using Templaty.Abstractions;

namespace Templaty.Tests.Services;

internal sealed class AnotherTestLocalizationTemplateContentStoreFactory : ITemplateContentStoreFactory
{
    public Template.StoreType Type => Template.StoreType.Localizations;

    public string Name => "another";

    public ITemplateContentStore Create()
    {
        return new AnotherTestLocalizationTemplateContentStore();
    }
}