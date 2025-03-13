namespace Templaty.Abstractions;

public interface ITemplateLoader
{
    /// <summary>
    /// loads template content as string from <see cref="ITemplateContentStore"/>
    /// </summary>
    /// <param name="cancellationToken">cancellation token</param>
    /// <typeparam name="TTemplate">template class</typeparam>
    /// <returns>template content as string</returns>
    /// <exception cref="NullReferenceException">when given type does not have <see cref="Template.SourceAttribute"/></exception>
    /// <exception cref="InvalidOperationException">when <see cref="ITemplateContentStoreFactory"/> was not registered for temlate content store</exception>
    Task<string> LoadContent<TTemplate>(CancellationToken cancellationToken) where TTemplate : ITemplate;

    /// <summary>
    /// loads template content as string from <see cref="ITemplateContentStore"/>
    /// </summary>
    /// <param name="type">template type (implements <see cref="ITemplate"/>)</param>
    /// <param name="cancellationToken">cancellation token></param>
    /// <returns>template content as string</returns>
    /// <exception cref="InvalidCastException">when given type does not inherit <see cref="ITemplate"/></exception>
    /// <exception cref="NullReferenceException">when given type does not have <see cref="Template.SourceAttribute"/></exception>
    /// <exception cref="InvalidOperationException">when <see cref="ITemplateContentStoreFactory"/> was not registered for temlate content store</exception>
    Task<string> LoadContent(Type type, CancellationToken cancellationToken);
}