namespace Templaty.Abstractions;

/// <remarks>Inheritance should be signleton because <see cref="ITemplateLoader"/> caches instatces of <see cref="ITemplateContentStoreFactory"/></remarks>
public interface ITemplateContentStoreFactory
{
    /// <summary>
    /// type of template content store
    /// </summary>
    Template.StoreType Type { get; }

    /// <summary>
    /// name of template content store
    /// </summary>
    string Name { get; }

    ITemplateContentStore Create();
}