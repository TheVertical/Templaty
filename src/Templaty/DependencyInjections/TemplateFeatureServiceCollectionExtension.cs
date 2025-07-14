using Microsoft.Extensions.DependencyInjection;
using Templaty.Abstractions;

namespace Templaty.DependencyInjections;

public static class TemplateFeatureServiceCollectionExtension
{
    public static IServiceCollection UseTemplaty(this IServiceCollection serviceCollection, Action<ITemplatesConfigurator>? configureAction = null)
    {
        // Templaty services register
        serviceCollection.AddScoped<ITemplateBuilder, TemplateBuilder>();
        serviceCollection.AddScoped<ITemplateLoader, TemplateLoader>();

        var configurator = new TemplatesConfigurator();

        if (configureAction is not null)
        {
            configureAction(configurator);
        }

        return configurator.Configure(serviceCollection);
    }
}