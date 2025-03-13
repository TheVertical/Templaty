using Templaty.Abstractions;

namespace Templaty.Tests.Services;

internal sealed class AnotherTestLocalizationTemplateContentStore : ITemplateContentStore
{
    public Template.StoreType Type => Template.StoreType.Localizations;

    /// <inheritdoc />
    public Task<string> GetContent(string path, CancellationToken cancellationToken = default)
    {
        return Task.FromResult("store: localilzations - another. {{string}} {{number}} {{double_number}} {{decimal_number}}");
    }
}