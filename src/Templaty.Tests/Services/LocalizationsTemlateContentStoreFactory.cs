using Templaty.Abstractions;

namespace Templaty.Tests.Services;

internal sealed class LocalizationsTemlateContentStoreFactory : ITemplateContentStoreFactory
{
    public Template.StoreType Type => Template.StoreType.Localizations;
    public string Name => "default";

    public ITemplateContentStore Create()
    {
        return new LocalizationsTemlateContentStore();
    }
}