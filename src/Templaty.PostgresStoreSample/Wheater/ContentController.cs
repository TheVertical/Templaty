using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Templaty.Abstractions.Exceptions;
using Templaty.Postgres.Services.Filling;
using Templaty.Postgres.Services.Getting;

namespace Templaty.PostgresStoreSample.Wheater;

[Route("templates/contents")]
public sealed class ContentController : Controller
{
    private readonly IPostgresContentStoreGetter _contentStoreGetter;
    private readonly IPostgresContentStoreFiller _contentStoreFiller;

    public ContentController(IPostgresContentStoreGetter contentStoreGetter, IPostgresContentStoreFiller contentStoreFiller)
    {
        _contentStoreGetter = contentStoreGetter;
        _contentStoreFiller = contentStoreFiller;
    }

    [HttpGet("paths")]
    public async Task<string[]> GetAllPaths(CancellationToken cancellationToken)
    {
        return await _contentStoreGetter.GetAllPaths(cancellationToken);
    }

    [HttpGet("{path}")]
    public async Task<IActionResult> GetContentByPath([FromRoute] string path, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return BadRequest("Path is required");
        }

        var content = await _contentStoreGetter.GetContent(path, cancellationToken);

        if (string.IsNullOrWhiteSpace(content))
        {
            return NotFound();
        }

        return Ok(content);
    }

    public sealed class AddContentRequest
    {
        [Required]
        public string Path { get; init; }

        [Required]
        public string Body { get; init; }
    }

    [HttpPost]
    public async Task<IActionResult> AddOne([FromBody] AddContentRequest request, CancellationToken cancellationToken)
    {
        var options = new ContentStoreFillOptions {FillMode = ContentFillMode.Exception};
        try
        {
            await _contentStoreFiller.AddOne(request.Path, request.Body, options, cancellationToken);

            return Ok();
        }
        catch (ContentPathAlreadyUsedException e)
        {
            return BadRequest($"Path '{e.TemplatePath}' are already used");
        }
    }
    
    public sealed class AddContentRangeRequest
    {
        [Required]
        public AddContentRequest[] Contens { get; init; } = Array.Empty<AddContentRequest>();
    }

    [HttpPost("range")]
    public async Task<IActionResult> AddRange([FromBody] AddContentRangeRequest request, CancellationToken cancellationToken)
    {
        var options = new ContentStoreFillOptions {FillMode = ContentFillMode.Exception};
        try
        {
            var keyValuePairs = request.Contens.Select(x => KeyValuePair.Create(x.Path, x.Body)).ToArray();
            await _contentStoreFiller.AddRange(keyValuePairs, options, cancellationToken);

            return Ok();
        }
        catch (ContentPathAlreadyUsedException e)
        {
            return BadRequest($"Path '{e.TemplatePath}' are already used");
        }
    }

    [HttpDelete("{path}")]
    public async Task<IActionResult> Delete([FromRoute] string path, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return BadRequest("Path is required");
        }
        
        try
        {
            await _contentStoreFiller.DeleteContent(path, cancellationToken);

            return Ok();
        }
        catch (TemplateContentNotFoundException e)
        {
            return BadRequest($"Path '{e.TemplatePath}' not found");
        }
    }
}