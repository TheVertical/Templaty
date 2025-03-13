using Microsoft.AspNetCore.Mvc;
using Templaty.Abstractions;
using Templaty.Simple.Resources;
using Templaty.Simple.Wheater.Dtos;
using Templaty.Simple.Wheater.Notifications;

namespace Templaty.Simple.Wheater;

[Route("weatherforecast")]
public sealed class WheaterController : Controller
{
    private readonly ITemplateBuilder _templateBuilder;

    public WheaterController(ITemplateBuilder templateBuilder)
    {
        _templateBuilder = templateBuilder;
    }

    private static readonly string[] Summaries =
    [
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    ];

    [HttpGet]
    public WeatherForecastDto[] Get()
    {
        var forecast = Enumerable.Range(1, 5)
            .Select(
                index =>
                    new WeatherForecastDto(
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        Summaries[Random.Shared.Next(Summaries.Length)]
                    )
            )
            .ToArray();

        return forecast;
    }

    [HttpGet("summary")]
    public async Task<string> GetSummary()
    {
        var forecast = new WeatherForecastDto(
            DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
            Random.Shared.Next(-20, 55),
            Summaries[Random.Shared.Next(Summaries.Length)]
        );

        var template = new WheaterDistributionTemplate(forecast.Date, forecast.TemperatureC, forecast.Summary);
        var @string = await _templateBuilder.LoadAndBuild(template);
        return @string;
    }

    [HttpGet("hellowolrd")]
    public async Task<string> GetHelloWorld(CancellationToken cancellationToken = default)
    {
        // loads and builds a string from store
        var @string = await _templateBuilder.LoadAndBuild<Templates.HelloWorld>(cancellationToken);
        return @string;
    }

    [HttpGet("hello")]
    public async Task<string> GetHello(string fName, string sName, CancellationToken cancellationToken = default)
    {
        var template = new Templates.Hello(fName, sName);
        var @string = await _templateBuilder.LoadAndBuild(template, cancellationToken);
        return @string;
    }
}