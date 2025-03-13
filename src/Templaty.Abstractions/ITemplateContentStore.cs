using Templaty.Abstractions.Exceptions;

namespace Templaty.Abstractions;

public interface ITemplateContentStore
{
    /// <summary>
    /// store type
    /// </summary>
    Template.StoreType Type { get; }

    /// <summary>
    /// returns content of template
    /// </summary>
    /// <param name="path">template path</param>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>template content</returns>
    /// <exception cref="TemplateContentMissedException">when template content was not found arround store.</exception>
    Task<string> GetContent(string path, CancellationToken cancellationToken = default);
}