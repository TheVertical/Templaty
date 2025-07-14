using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Templaty.Abstractions;
using Templaty.Postgres.Services;
using Templaty.Postgres.Services.Filling;
using Templaty.Postgres.Services.Getting;
using Templaty.Postgres.Stores;

namespace Templaty.Postgres.DependencyInjections;

public static class TemplateFeatureServiceCollectionExtension
{
    public static IServiceCollection AddTemplatyPostgresStore<TDbContext>(this IServiceCollection serviceCollection)
        where TDbContext : DbContext
    {
        // Templaty services register
        serviceCollection.AddScoped<IPostgresContentStoreFiller>(
            provider =>
            {
                var dbContext = provider.CreateScope().ServiceProvider.GetRequiredService<TDbContext>();
                return new PostgresContentStoreFiller<TDbContext>(dbContext);
            }
        );
        serviceCollection.AddScoped<IPostgresContentStoreGetter>(
            provider =>
            {
                var dbContext = provider.CreateScope().ServiceProvider.GetRequiredService<TDbContext>();
                return new PostgresContentStoreGetter<TDbContext>(dbContext);
            }
        );
        serviceCollection.AddScoped<ITemplateContentStoreFactory>(
            provider =>
            {
                var dbContext = provider.CreateScope().ServiceProvider.GetRequiredService<TDbContext>();
                return new PostgresTemplateContentStoreFactory<TDbContext>(dbContext);
            }
        );

        return serviceCollection;
    }
}