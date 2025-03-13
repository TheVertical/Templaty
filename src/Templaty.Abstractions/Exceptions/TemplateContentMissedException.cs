namespace Templaty.Abstractions.Exceptions;

public sealed class TemplateContentMissedException : ApplicationException
{
    public readonly string TemplatePath;

    public readonly Template.StoreType StoreType;

    public TemplateContentMissedException(string templatePath, Template.StoreType storeType, string? message = null) : base(message)
    {
        TemplatePath = templatePath;
        StoreType = storeType;
    }

    public TemplateContentMissedException(string templatePath, Template.StoreType storeType) : base(
        $"Template content was not found by path '{templatePath}' arround store '{Enum.GetName(storeType)}'."
    )
    {
        TemplatePath = templatePath;
        StoreType = storeType;
    }
}