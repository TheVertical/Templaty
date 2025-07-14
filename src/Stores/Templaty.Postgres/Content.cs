namespace Templaty.Postgres;

public sealed class Content
{
    private Content()
    {
    }

    public Content(string path, string body)
    {
        Path = path;
        Body = body;
    }

    public string Path { get; init; }

    public string Body { get; set; }

    public const int PathMaxLength = 128;
}