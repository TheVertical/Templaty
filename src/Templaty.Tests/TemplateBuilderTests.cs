using System.Globalization;
using FluentAssertions;
using NSubstitute;
using Templaty.Abstractions;
using Templaty.Tests.Models;

namespace Templaty.Tests;

public sealed class TemplateBuilderTests
{
    private readonly ITemplateLoader _templateLoader;
    private readonly ITemplateBuilder _sut;

    public TemplateBuilderTests()
    {
        _templateLoader = Substitute.For<ITemplateLoader>();
        _sut = new TemplateBuilder(_templateLoader);
    }

    private const string TestTemplateContent = "{{string}} {{number}} {{double_number}} {{decimal_number}}";

    [Fact]
    public async Task BuilderShouldBuildsMessageFromTemplateByVariables()
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-US");
        // arrange
        _templateLoader.LoadContent<TestTemplate>(Arg.Any<CancellationToken>()).Returns(TestTemplateContent);
        var testTemplate = new TestTemplate
        {
            String = "test",
            Number = 10,
            DoubleNumber = 10.5d,
            DecimalNumber = 25.5m
        };

        // act
        var builtMessage = await _sut.Build(TestTemplateContent, testTemplate, CancellationToken.None);

        // assert
        builtMessage.Should()
            .Be($"{testTemplate.String} {testTemplate.Number.ToString()} {testTemplate.DoubleNumber} {testTemplate.DecimalNumber}");
    }

    [Fact]
    public async Task BuilderShouldbThrowExceptionIfTemplateContentIsNullOrEmpty()
    {
        // arrange
        _templateLoader.LoadContent<TestTemplate>(Arg.Any<CancellationToken>()).Returns(TestTemplateContent);
        var testTemplate = new TestTemplate();

        // act & assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.Build(string.Empty, testTemplate, CancellationToken.None));
    }
}