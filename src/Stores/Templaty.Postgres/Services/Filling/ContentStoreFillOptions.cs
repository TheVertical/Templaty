namespace Templaty.Postgres.Services.Filling;

public sealed record ContentStoreFillOptions
{
    /// <summary>
    /// Fill mode (see: <see cref="ContentFillMode"/>)
    /// </summary>
    /// <remarks>default value: <see cref="ContentFillMode.Exception"/></remarks>
    public ContentFillMode FillMode { get; set; } = ContentFillMode.Exception;
}