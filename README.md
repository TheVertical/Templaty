# Templaty - .net library for templating


Block force pushes
Prevent users with push access from force pushing to refs.

# 1. Introduction

**Templaty** is a lightweight .net library based on [Scriban library](https://github.com/scriban/scriban) providing
developers easy way to realize flexible templates feature into projects.

## 1.1 Use cases

1. E-mailing message templates;
2. Localization message templates;
3. Text messages templates;

## 1.2 Concepts

**Templaty** is simple. It operates a few several concepts:

1. **Content** - template content / message content;
2. **Template object** - a `class` or `record` that has `[Template.Source(...)]` attribute and implements `ITemplate`
   interface;
3. **Store** - service implements `ITemplateContentStore` where **Content** is storing;
4. **Loader** - service that realizes **Content** loading;
5. **Builder** - main service builds result by **Template object** and **Content**;

# 2. Quick start
First of all, you need to import `Templaty` as package;

``` shell
dotnet add package Templaty
```

## 2.1 ASP.NET

To use **Templaty** into ASP.NET projects just using into `Startup` or `Program`:

```csharp
builder.Services.UseTemplaty(configator => configator.AddResourceStoreAssembly(typeof(Program).Assembly));
```

By default **Templaty** provides only `ResourceTemplateContentStore` that registering while `UseTemplate` executing.
It allows you to use embedded resource files as templates and use it around project;

## 2.2 Make your own template

Let's look at an example into project `Templaty.Simple` (see `Samples`).

1. Create template content file (example: `WheaterDistribution.txt`);

```
Date: {{date}}
Temperature: {{temperature}} °C
Summary: {{summary}}
```

2. Mark `WheaterDistribution.txt` as `Embedded Resource` (see: [Build actions](https://learn.microsoft.com/en-us/visualstudio/ide/build-actions?view=vs-2022));

3. Create a model with data `WheaterDistributionTemplate`;

```csharp
[Template.Source("Templaty.Simple.Wheater.Notifications.WheaterDistribution.txt", Template.StoreType.Resources)]
internal sealed record WheaterDistributionTemplate(DateOnly Date, int Temperature, string? Summary) : ITemplate;
```

4. Use `ITemplateBuilder` to get end-message from template;

```csharp
var template = new WheaterDistributionTemplate(
    DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
    Random.Shared.Next(-20, 55),
    Summaries[Random.Shared.Next(Summaries.Length)]
 );

var result = await _templateBuilder.LoadAndBuild(template);
```

Result will be something like that:

```
Date: 16.03.2025
Temperature: 8 °C
Summary: Balmy
```


# 3. Own store

**Templaty** allows you to make your own store that will contains template content whenever you want.

1. Create new class that implements `ITemplateContentStore`;
```csharp
internal sealed class LocalizableResourceStore : ITemplateContentStore
{
    private readonly IStringLocalizer<Templates> _stringLocalizer;

    public LocalizableResourceStore(IStringLocalizer<Templates> stringLocalizer)
    {
        _stringLocalizer = stringLocalizer;
    }

    public Template.StoreType Type => Template.StoreType.Localizations;

    public Task<string> GetContent(string path, CancellationToken cancellationToken = default)
    {
        var localizedString = _stringLocalizer.GetString(path);

        if (localizedString.ResourceNotFound)
        {
            throw new TemplateContentMissedException(path, Type, $"Localization '{path}' was not found.");
        }
        else if (string.IsNullOrWhiteSpace(localizedString))
        {
            return Task.FromResult(path);
        }

        return Task.FromResult(localizedString.Value);
    }
}
```
2. Create new class that implements `ITemplateContentStoreFactory`;
```csharp
internal sealed class LocalizableResourceStoreFactory(IStringLocalizer<Templates> stringLocalizer) : ITemplateContentStoreFactory
{
    public Template.StoreType Type => Template.StoreType.Localizations;

    public string Name => "default";

    public ITemplateContentStore Create() => new LocalizableResourceStore(stringLocalizer);
}
```
3. Register created service-factory as `ITemplateContentStoreFactory` or configurator method `.AddStore()`;
```csharp
builder.Services.AddSingleton<ITemplateContentStoreFactory, LocalizableResourceStoreFactory>();
```

```csharp
builder.Services.UseTemplaty(
    configator => configator
        .AddResourceStoreAssembly(typeof(Program).Assembly)
        .AddStore(x => x.GetRequiredService<LocalizableResourceStoreFactory>())
);
```
