using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace Templaty.Tests;

/// <summary>
/// helper for resource
/// </summary>
/// <param name="ResourcePath">standard resources path</param>
/// <param name="ResourceAssembly">asseblies where resources are stored</param>
public sealed class TestResourcesHelper(string ResourcePath, Assembly ResourceAssembly)
{
    public Stream GetResourceFileAsStream(string fileName)
    {
        var resourcePath = string.Join('.', ResourcePath, fileName);

        var stream = ResourceAssembly.GetManifestResourceStream(resourcePath);

        if (stream == null)
        {
            throw new NullReferenceException($"Resource {fileName} was not found.");
        }

        return stream;
    }

    public IFormFile GetTestResourceAsFormFile(string fileName)
    {
        var resourcePath = string.Join('.', ResourcePath, fileName);
        var stream = ResourceAssembly.GetManifestResourceStream(resourcePath);

        if (stream == null)
        {
            throw new NullReferenceException($"Resource {fileName} was not found.");
        }

        return new FormFile(
            stream,
            0,
            stream.Length,
            Path.GetFileNameWithoutExtension(fileName),
            fileName
        );
    }

    public async Task<string> GetTetsResourceAsString(string fileName)
    {
        var resourcePath = string.Join('.', ResourcePath, fileName);
        using var stream = ResourceAssembly.GetManifestResourceStream(resourcePath);

        if (stream == null)
        {
            throw new NullReferenceException($"Resource {fileName} was not found.");
        }

        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }

    public async Task<string> GetTestResouceAsStringByAbsolutePath(string absoluteResourcePath)
    {
        using var stream = ResourceAssembly.GetManifestResourceStream(absoluteResourcePath);

        if (stream == null)
        {
            throw new NullReferenceException($"Resource {absoluteResourcePath} was not found.");
        }

        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
}