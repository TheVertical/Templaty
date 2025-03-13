using System.Reflection;
using Templaty.Abstractions;

namespace Templaty.Stores;

internal sealed class ResourceTemplateContentStoreFactory(params Assembly[] assemblies) : ITemplateContentStoreFactory
{
    public Template.StoreType Type => Template.StoreType.Resources;

    public string Name => "default";

    public ITemplateContentStore Create()
    {
        return new ResourceTemplateContentStore(assemblies);
    }
}