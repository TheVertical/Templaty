using System.Reflection;
using Templaty.Abstractions;
using Templaty.Abstractions.Exceptions;

namespace Templaty.Stores;

internal sealed class ResourceTemplateContentStore : ITemplateContentStore
{
    private readonly Dictionary<string, Assembly> _resourceNameToAssemblyMapping;

    public ResourceTemplateContentStore(params Assembly[] resourceAssemblyDescriptors)
    {
        _resourceNameToAssemblyMapping = resourceAssemblyDescriptors
            .SelectMany(assembly => assembly.GetManifestResourceNames().Select(r => new {ResourceName = r, Assembly = assembly}))
            .ToDictionary(x => x.ResourceName, x => x.Assembly);
    }

    /// <inheritdoc />
    public Template.StoreType Type => Template.StoreType.Resources;

    /// <inheritdoc />
    public async Task<string> GetContent(string path, CancellationToken cancellationToken = default)
    {
        if (!_resourceNameToAssemblyMapping.TryGetValue(path, out var assembly))
        {
            throw new TemplateContentMissedException(path, Type, "Template content was not registered into resource assebmlies.");
        }

        await using var stream = assembly.GetManifestResourceStream(path);

        if (stream == null)
        {
            throw new TemplateContentMissedException(path, Type);
        }

        using var streamReader = new StreamReader(stream);
        var content = await streamReader.ReadToEndAsync(cancellationToken);
        return content;
    }
}