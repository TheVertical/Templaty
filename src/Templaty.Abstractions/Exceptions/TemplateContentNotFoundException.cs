namespace Templaty.Abstractions.Exceptions;

public sealed class TemplateContentNotFoundException : ApplicationException
{
    public readonly string TemplatePath;

    public TemplateContentNotFoundException(string templatePath, string? message = null) : base(message)
    {
        TemplatePath = templatePath;
    }

    public TemplateContentNotFoundException(string templatePath) : base($"Content by path='{templatePath}' was not found")
    {
        TemplatePath = templatePath;
    }
}