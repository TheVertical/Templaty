using System.Collections.Concurrent;
using System.Reflection;
using Templaty.Abstractions;

namespace Templaty;

internal sealed class TemplateLoader : ITemplateLoader
{
    private sealed record StoreDescriptor(Template.StoreType Type, string Name);

    private sealed record TemplateTypeDescriptor(string Path, Template.StoreType StoreType, string StoreName);

    private readonly IReadOnlyDictionary<StoreDescriptor, ITemplateContentStoreFactory> _contentStoreFactories;
    private readonly ConcurrentDictionary<Type, TemplateTypeDescriptor> _templatePathCache;

    public TemplateLoader(IEnumerable<ITemplateContentStoreFactory> contentStoreFactories)
    {
        _contentStoreFactories = contentStoreFactories.ToDictionary(x => new StoreDescriptor(x.Type, x.Name));
        _templatePathCache = new ConcurrentDictionary<Type, TemplateTypeDescriptor>();
    }

    /// <inheritdoc />
    public Task<string> LoadContent<TTemplate>(CancellationToken cancellationToken) where TTemplate : ITemplate
    {
        var type = typeof(TTemplate);
        return LoadContent(type, cancellationToken);
    }

    /// <inheritdoc />
    public Task<string> LoadContent(Type type, CancellationToken cancellationToken)
    {
        if (!typeof(ITemplate).IsAssignableFrom(type))
        {
            throw new InvalidCastException($"{type.FullName} should implement {nameof(ITemplate)} interface");
        }

        if (_templatePathCache.TryGetValue(type, out var templateTypeDescriptor))
        {
            return GetContent(templateTypeDescriptor, cancellationToken);
        }

        var pathAttribute = type.GetCustomAttribute<Template.SourceAttribute>();

        if (pathAttribute is null || string.IsNullOrWhiteSpace(pathAttribute.Path))
        {
            throw new NullReferenceException($"Type {type.FullName} must have {nameof(Template.SourceAttribute)}.");
        }

        templateTypeDescriptor = new TemplateTypeDescriptor(pathAttribute.Path, pathAttribute.StoreType, pathAttribute.StoreName);

        if (!_templatePathCache.TryAdd(type, templateTypeDescriptor) && !_templatePathCache.TryGetValue(type, out templateTypeDescriptor))
        {
            throw new InvalidOperationException($"Unable to add or get template type descriptor {type.FullName}.");
        }

        return GetContent(templateTypeDescriptor, cancellationToken);
    }

    private async Task<string> GetContent(TemplateTypeDescriptor templateTypeDescriptor, CancellationToken cancellationToken = default)
    {
        if (!_contentStoreFactories.TryGetValue(new StoreDescriptor(templateTypeDescriptor.StoreType, templateTypeDescriptor.StoreName), out var factory))
        {
            throw new InvalidOperationException($"Template content store factory is not registered for {Enum.GetName(templateTypeDescriptor.StoreType)}.");
        }

        var store = factory.Create();
        var content = await store.GetContent(templateTypeDescriptor.Path, cancellationToken);
        return content.Trim('\n', '\r');
    }
}