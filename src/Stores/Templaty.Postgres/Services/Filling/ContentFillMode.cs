namespace Templaty.Postgres.Services.Filling;

/// <summary>
/// Defines fill behavior on add content into store
/// </summary>
public enum ContentFillMode : byte
{
    /// <summary>
    /// Throws exception on used path
    /// </summary>
    Exception = 0,

    /// <summary>
    /// Skip filling if path is already in use
    /// </summary>
    Skip = 1,

    /// <summary>
    /// Replace body of existing path
    /// </summary>
    Replace = 2
}