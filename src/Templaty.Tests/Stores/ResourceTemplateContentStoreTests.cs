using FluentAssertions;
using Templaty.Abstractions;
using Templaty.Abstractions.Exceptions;
using Templaty.Stores;

namespace Templaty.Tests.Stores;

public sealed class ResourceTemplateContentStoreTests
{
    private readonly ITemplateContentStore _sut;

    public ResourceTemplateContentStoreTests()
    {
        _sut = new ResourceTemplateContentStore(typeof(ResourceTemplateContentStoreTests).Assembly);
    }

    [Fact]
    public async Task HandleGetContent_WhenAssemblyHasNoTemplateContentResource_ThenTemplateContentMissedExceptionOccurs()
    {
        // arrange
        var templatePath = "Templaty.Tests.Resources.MissedTemplate.sbn";

        // act & assert
        await Assert.ThrowsAsync<TemplateContentMissedException>(() => _sut.GetContent(templatePath, CancellationToken.None));
    }

    [Fact]
    public async Task HandleGetContent_ThenTemplateContentIsReturned()
    {
        // arrange
        var templatePath = "Templaty.Tests.Resources.TestTemplate.sbn";

        // act
        var content = await _sut.GetContent(templatePath, CancellationToken.None);

        // assert
        content.Should().NotBeNullOrWhiteSpace();
    }
}