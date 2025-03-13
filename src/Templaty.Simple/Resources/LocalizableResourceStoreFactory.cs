using Microsoft.Extensions.Localization;
using Templaty.Abstractions;

namespace Templaty.Simple.Resources;

internal sealed class LocalizableResourceStoreFactory(IStringLocalizer<Templates> stringLocalizer) : ITemplateContentStoreFactory
{
    public Template.StoreType Type => Template.StoreType.Localizations;

    public string Name => "default";

    public ITemplateContentStore Create() => new LocalizableResourceStore(stringLocalizer);
}