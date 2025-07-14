namespace Templaty.Postgres.Services.Filling;

public interface IPostgresContentStoreFiller
{
    /// <summary>
    /// Adds content to store
    /// </summary>
    /// <param name="path">template path</param>
    /// <param name="body">content body</param>
    /// <param name="options">fill options</param>
    /// <param name="cancellationToken">cancellation tokens</param>
    /// <returns>Task</returns>
    /// <exception cref="Templaty.Abstractions.Exceptions.ContentPathAlreadyUsedException">when <see cref="ContentStoreFillOptions"/> is set as <see cref="ContentFillMode"/></exception>
    Task AddOne(string path, string body, ContentStoreFillOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds range of content to store
    /// </summary>
    /// <param name="contentItems">content items where Key = Path and Value = Content body</param>
    /// <param name="options">fill options</param>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>Task</returns>
    /// <exception cref="Templaty.Abstractions.Exceptions.ContentPathAlreadyUsedException">when <see cref="ContentStoreFillOptions.FillMode"/> is set as <see cref="ContentFillMode.Exception"/></exception>
    Task AddRange(
        KeyValuePair<string, string>[] contentItems,
        ContentStoreFillOptions options,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Replace existing template content body
    /// </summary>
    /// <param name="path">template path</param>
    /// <param name="newBody">new content body</param>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>Task</returns>
    /// <exception cref="Templaty.Abstractions.Exceptions.TemplateContentNotFoundException">when content was not found into store</exception>
    Task ReplaceContent(string path, string newBody, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes existing template content from store
    /// </summary>
    /// <param name="path">template path</param>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>Task</returns>
    /// <exception cref="Templaty.Abstractions.Exceptions.TemplateContentNotFoundException">when content was not found into store</exception>
    Task DeleteContent(string path, CancellationToken cancellationToken = default);
}