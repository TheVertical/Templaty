using Microsoft.EntityFrameworkCore;
using Templaty.Abstractions;
using Templaty.Abstractions.Exceptions;

namespace Templaty.Postgres.Stores;

internal sealed class PostgresTemplateContentStore<TDbContext> : ITemplateContentStore
    where TDbContext : DbContext
{
    private readonly DbContext _dbContext;

    public PostgresTemplateContentStore(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public Template.StoreType Type => Template.StoreType.Postgres;

    /// <inheritdoc />
    public async Task<string> GetContent(string path, CancellationToken cancellationToken = default)
    {
        var content = await _dbContext.Set<Content>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Path == path, cancellationToken);

        if (content == null)
        {
            throw new TemplateContentMissedException(path, Type, "Template content was not found into postgres store.");
        }

        return content.Body;
    }
}