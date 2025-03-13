using System.Globalization;
using Scriban;
using Scriban.Parsing;
using Scriban.Runtime;
using Templaty.Abstractions;
using ITemplateLoader = Templaty.Abstractions.ITemplateLoader;
using Template = Scriban.Template;

namespace Templaty;

internal sealed class TemplateBuilder(ITemplateLoader loader) : ITemplateBuilder
{
    /// <inheritdoc />
    public ValueTask<string> Build<TTemplate>(string templateContent, TTemplate templateObject, CancellationToken cancellationToken) where TTemplate : ITemplate
    {
        if (string.IsNullOrWhiteSpace(templateContent))
        {
            throw new ArgumentNullException(nameof(templateContent));
        }

        var context = PrepareContext(templateObject);
        var template = Template.Parse(templateContent, parserOptions: new ParserOptions());
        return template.RenderAsync(context);
    }

    public async ValueTask<string> LoadAndBuild<TTemplate>(TTemplate templateObject, CancellationToken cancellationToken = default) where TTemplate : ITemplate
    {
        var templateContent = await loader.LoadContent<TTemplate>(cancellationToken);
        templateContent = templateContent.Trim('\n', '\r');

        if (string.IsNullOrWhiteSpace(templateContent))
        {
            throw new ArgumentNullException(nameof(templateContent));
        }

        var context = PrepareContext(templateObject);
        var template = Template.Parse(templateContent, parserOptions: new ParserOptions());

        var renderAsync = await template.RenderAsync(context);
        return renderAsync.Trim('\n', '\r');
    }

    /// <inheritdoc />
    public async ValueTask<string> LoadAndBuild<TTemplate>(CancellationToken cancellationToken = default) where TTemplate : ITemplate
    {
        return await loader.LoadContent<TTemplate>(cancellationToken);
    }

    private static TemplateContext PrepareContext<TTemplate>(TTemplate templateObject) where TTemplate : ITemplate
    {
        var scriptObject = new ScriptObject();
        scriptObject.Import(templateObject);

        var context = new TemplateContext();
        context.PushCulture(CultureInfo.CurrentCulture);
        context.PushGlobal(scriptObject);
        return context;
    }
}