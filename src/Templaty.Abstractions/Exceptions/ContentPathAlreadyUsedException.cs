namespace Templaty.Abstractions.Exceptions;

public sealed class ContentPathAlreadyUsedException : ApplicationException
{
    public readonly string TemplatePath;

    public ContentPathAlreadyUsedException(string templatePath, string? message = null) : base(message)
    {
        TemplatePath = templatePath;
    }

    public ContentPathAlreadyUsedException(string templatePath) : base($"Path {templatePath} alredy used by another content")
    {
        TemplatePath = templatePath;
    }
}