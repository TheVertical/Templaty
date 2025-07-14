using Microsoft.EntityFrameworkCore;
using Templaty.Abstractions;

namespace Templaty.Postgres.Stores;

internal sealed class PostgresTemplateContentStoreFactory<TDbContext>(TDbContext dbContext) : ITemplateContentStoreFactory
    where TDbContext : DbContext
{
    public Template.StoreType Type => Template.StoreType.Postgres;

    public string Name => "default";

    public ITemplateContentStore Create() => new PostgresTemplateContentStore<TDbContext>(dbContext);
}