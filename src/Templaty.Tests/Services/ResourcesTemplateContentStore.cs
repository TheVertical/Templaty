using Templaty.Abstractions;

namespace Templaty.Tests.Services;

internal sealed class ResourcesTemplateContentStore : ITemplateContentStore
{
    public Template.StoreType Type => Template.StoreType.Resources;

    /// <inheritdoc />
    public Task<string> GetContent(string path, CancellationToken cancellationToken = default)
    {
        return new TestResourcesHelper("Templaty.Tests.Resources", typeof(ResourcesTemplateContentStore).Assembly)
            .GetTestResouceAsStringByAbsolutePath(path);
    }
}