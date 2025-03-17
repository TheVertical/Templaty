using Microsoft.Extensions.Localization;
using Templaty.Abstractions;
using Templaty.Abstractions.Exceptions;

namespace Templaty.Simple.Resources;

internal sealed class LocalizableResourceStore : ITemplateContentStore
{
    private readonly IStringLocalizer<Templates> _stringLocalizer;

    public LocalizableResourceStore(IStringLocalizer<Templates> stringLocalizer)
    {
        _stringLocalizer = stringLocalizer;
    }

    public Template.StoreType Type => Template.StoreType.Localizations;

    public Task<string> GetContent(string path, CancellationToken cancellationToken = default)
    {
        var localizedString = _stringLocalizer.GetString(path);

        if (localizedString.ResourceNotFound)
        {
            throw new TemplateContentMissedException(path, Type, $"Localization '{path}' was not found.");
        }
        else if (string.IsNullOrWhiteSpace(localizedString))
        {
            return Task.FromResult(path);
        }

        return Task.FromResult(localizedString.Value);
    }
}