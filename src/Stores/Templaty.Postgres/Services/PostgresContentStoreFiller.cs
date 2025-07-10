using Microsoft.EntityFrameworkCore;
using Templaty.Abstractions.Exceptions;
using Templaty.Postgres.Services.Filling;

namespace Templaty.Postgres.Services;

internal sealed class PostgresContentStoreFiller<TDbContext> : IPostgresContentStoreFiller
    where TDbContext : DbContext
{
    private readonly DbContext _dbContext;

    public PostgresContentStoreFiller(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task AddOne(string path, string body, ContentStoreFillOptions options, CancellationToken cancellationToken = default)
    {
        var content = await _dbContext.Set<Content>().FirstOrDefaultAsync(x => x.Path == path, cancellationToken: cancellationToken);

        if (content is not null)
        {
            switch (options.FillMode)
            {
                case ContentFillMode.Exception:
                    throw new ContentPathAlreadyUsedException(path);
                case ContentFillMode.Skip:
                    return;
                case ContentFillMode.Replace:
                    await ReplaceContent(content, body, cancellationToken);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(options.FillMode), $"Unknown enum value {options.FillMode}");
            }
        }

        content = new Content(path, body);
        _dbContext.Set<Content>().Add(content);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task AddRange(
        KeyValuePair<string, string>[] contentItems,
        ContentStoreFillOptions options,
        CancellationToken cancellationToken = default
    )
    {
        var paths = contentItems.Select(x => x.Key).ToArray();
        var contents = await _dbContext.Set<Content>()
            .AsTracking()
            .Where(x => paths.Contains(x.Path))
            .ToDictionaryAsync(x => x.Path, cancellationToken);

        var toAddList = new List<Content>(contentItems.Length);
        foreach (var item in contentItems)
        {
            if (contents.TryGetValue(item.Key, out var content))
            {
                switch (options.FillMode)
                {
                    case ContentFillMode.Exception:
                        throw new ContentPathAlreadyUsedException(item.Key);
                    case ContentFillMode.Skip:
                        continue;
                    case ContentFillMode.Replace:
                        content.Body = item.Value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(options.FillMode), $"Unknown enum value {options.FillMode}");
                }
            }

            content = new Content(item.Key, item.Value);
            toAddList.Add(content);
        }

        _dbContext.Set<Content>().AddRange(toAddList);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task ReplaceContent(string path, string newBody, CancellationToken cancellationToken = default)
    {
        var content = await _dbContext.Set<Content>().FirstOrDefaultAsync(x => x.Path == path, cancellationToken: cancellationToken);
        if (content is null)
        {
            throw new TemplateContentNotFoundException(path);
        }

        await ReplaceContent(content, newBody, cancellationToken);
    }

    private async Task ReplaceContent(Content content, string newBody, CancellationToken cancellationToken = default)
    {
        content.Body = newBody;
        _dbContext.Attach(content);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteContent(string path, CancellationToken cancellationToken = default)
    {
        var content = await _dbContext.Set<Content>().AsTracking().FirstOrDefaultAsync(x => x.Path == path, cancellationToken: cancellationToken);

        if (content is null)
        {
            throw new TemplateContentNotFoundException(path);
        }

        _dbContext.Set<Content>().Remove(content);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}