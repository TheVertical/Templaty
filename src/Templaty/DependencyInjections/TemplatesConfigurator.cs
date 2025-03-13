using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Templaty.Abstractions;
using Templaty.Stores;

namespace Templaty.DependencyInjections;

public sealed class TemplatesConfigurator : ITemplatesConfigurator
{
    private readonly List<Assembly> _resourceAssemblies = new();
    private readonly List<Func<IServiceProvider, ITemplateContentStoreFactory>> _factoryImplementationFuncs = new();

    public ITemplatesConfigurator AddResourceStoreAssembly(Assembly resourceAssembly)
    {
        _resourceAssemblies.Add(resourceAssembly);
        return this;
    }

    public ITemplatesConfigurator AddStore(Func<IServiceProvider, ITemplateContentStoreFactory> implementationFunc)
    {
        _factoryImplementationFuncs.Add(implementationFunc);
        return this;
    }

    public IServiceCollection Configure(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITemplateContentStoreFactory>(new ResourceTemplateContentStoreFactory(_resourceAssemblies.ToArray()));
        foreach (var func in _factoryImplementationFuncs)
        {
            serviceCollection.AddSingleton(func);
        }

        return serviceCollection;
    }
}