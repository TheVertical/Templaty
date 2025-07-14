using Microsoft.EntityFrameworkCore;
using Templaty.Postgres.Services.Getting;

namespace Templaty.Postgres.Services;

internal sealed class PostgresContentStoreGetter<TDbContext> : IPostgresContentStoreGetter where TDbContext : DbContext
{
    private readonly DbContext _dbContext;

    public PostgresContentStoreGetter(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public Task<string[]> GetAllPaths(CancellationToken cancellationToken)
    {
        return _dbContext.Set<Content>()
            .AsNoTracking()
            .Select(x => x.Path)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<string?> GetContent(string path, CancellationToken cancellationToken)
    {
        var content = await _dbContext.Set<Content>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Path == path, cancellationToken: cancellationToken);

        return content?.Body;
    }
}