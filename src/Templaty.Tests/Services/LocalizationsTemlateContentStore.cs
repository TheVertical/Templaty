using Templaty.Abstractions;

namespace Templaty.Tests.Services;

internal sealed class LocalizationsTemlateContentStore : ITemplateContentStore
{
    public Template.StoreType Type => Template.StoreType.Localizations;
    public Task<string> GetContent(string path, CancellationToken cancellationToken = default)
    {
        return Task.FromResult("store: localilzations - default. {{string}} {{number}} {{double_number}} {{decimal_number}}");
    }
}