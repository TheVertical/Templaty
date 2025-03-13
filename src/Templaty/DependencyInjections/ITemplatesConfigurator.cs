using System.Reflection;
using Templaty.Abstractions;

namespace Templaty.DependencyInjections;

public interface ITemplatesConfigurator
{
    /// <summary>
    /// adds resource assembly to resource <see cref="ITemplateContentStore"/>
    /// </summary>
    /// <param name="resourceAssembly">resource assembly</param>
    ITemplatesConfigurator AddResourceStoreAssembly(Assembly resourceAssembly);

    /// <summary>
    /// adds custom store for template content
    /// </summary>
    /// <param name="implementationFunc">implemenattion function</param>
    ITemplatesConfigurator AddStore(Func<IServiceProvider, ITemplateContentStoreFactory> implementationFunc);
}