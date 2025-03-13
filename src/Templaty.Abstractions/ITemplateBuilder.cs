namespace Templaty.Abstractions;

public interface ITemplateBuilder
{
    /// <summary>
    /// builds end message from template
    /// </summary>
    /// <param name="cancellationToken">cancellation token></param>
    /// <param name="templateContent">template content</param>
    /// <param name="templateObject">template class</param>
    /// <returns>template content as string</returns>
    /// <exception cref="ArgumentNullException"><paramref name="templateContent"/> is null or empty</exception>
    ValueTask<string> Build<TTemplate>(string templateContent, TTemplate templateObject, CancellationToken cancellationToken) where TTemplate : ITemplate;

    /// <summary>
    /// loads and builds end message from template
    /// </summary>
    /// <param name="cancellationToken">cancellation token></param>
    /// <param name="templateObject">template class</param>
    /// <returns>template content as string</returns>
    /// <exception cref="ArgumentNullException">if loaded content is null or empty</exception>
    ValueTask<string> LoadAndBuild<TTemplate>(TTemplate templateObject, CancellationToken cancellationToken = default) where TTemplate : ITemplate;

    /// <summary>
    /// loads and builds message by default behavior
    /// </summary>
    /// <remarks>
    /// default building behavior: no variables are applied
    /// </remarks>
    /// <param name="cancellationToken">cancellation token></param>
    /// <returns>template content as string</returns>
    /// <exception cref="ArgumentNullException">if loaded content is null or empty</exception>
    ValueTask<string> LoadAndBuild<TTemplate>(CancellationToken cancellationToken = default) where TTemplate : ITemplate;
}