using Templaty.Abstractions;

namespace Templaty.Tests.Services;

internal sealed class ResourcesTemplateContentStoreFactory : ITemplateContentStoreFactory
{
    public Template.StoreType Type => Template.StoreType.Resources;

    public string Name => "default";

    public ITemplateContentStore Create()
    {
        return new ResourcesTemplateContentStore();
    }
}