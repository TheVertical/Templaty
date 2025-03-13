using FluentAssertions;
using Templaty.Abstractions;
using Templaty.Tests.Models;
using Templaty.Tests.Services;

namespace Templaty.Tests;

public sealed class TemplateLoaderTests
{
    private readonly ITemplateLoader _sut = new TemplateLoader(
        [
            new LocalizationsTemlateContentStoreFactory(),
            new AnotherTestLocalizationTemplateContentStoreFactory(),
            new ResourcesTemplateContentStoreFactory()
        ]
    );

    private const string TestTemplateContent = "{{string}} {{number}} {{double_number}} {{decimal_number}}";

    private const string LocalizationsTemlateContentStoreContent =
        "store: localilzations - default. {{string}} {{number}} {{double_number}} {{decimal_number}}";

    private const string AnotherTestLocalizationTemplateContentStoreContent =
        "store: localilzations - another. {{string}} {{number}} {{double_number}} {{decimal_number}}";

    [Fact]
    public async Task HandleLoad_WhenTemplateHasNoAttribute_ThenNullReferenceExceptionOccurs()
    {
        // act & assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.LoadContent<WithoutAttributeTestTemplate>(CancellationToken.None));
    }

    [Fact]
    public async Task HandleLoad_WhenNoTemplateContentStoreFactoryPassedForGivenStoreType_ThenInvalidOperationExceptionOccures()
    {
        // act & assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _sut.LoadContent<LocalizationTestTemplate>(CancellationToken.None));
    }

    [Fact]
    public async Task HandleLoadContent_ThenTemplateIsLoaded()
    {
        // act
        var templateContent = await _sut.LoadContent<TestTemplate>(CancellationToken.None);

        // assert
        templateContent.Should().Be(TestTemplateContent);
    }

    [Fact]
    public async Task HandleLoadContent_WhenSeveralLocalizationsStoresLinked_ThenTemplateIsLoaded()
    {
        // act
        var templateContent = await _sut.LoadContent<DefaultTestTemplate>(CancellationToken.None);
        var anotherTempateContent = await _sut.LoadContent<AnotherTestTemlpate>(CancellationToken.None);

        // assert
        templateContent.Should().Be(LocalizationsTemlateContentStoreContent);
        anotherTempateContent.Should().Be(AnotherTestLocalizationTemplateContentStoreContent);
    }
}