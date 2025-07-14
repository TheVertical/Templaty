namespace Templaty.Postgres.Services.Getting;

public interface IPostgresContentStoreGetter
{
    /// <summary>
    /// Get all used paths
    /// </summary>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>paths collection</returns>
    Task<string[]> GetAllPaths(CancellationToken cancellationToken);

    /// <summary>
    /// Get content body by path
    /// </summary>
    /// <param name="path">path</param>
    /// <param name="cancellationToken">cancellation token</param>
    /// <returns>content body if exists</returns>
    Task<string?> GetContent(string path, CancellationToken cancellationToken);
}