# Templaty - PostgreSQL store

# 1. Introduction
**Templaty - Postgres store** - is a store based on PostgreSQL DB providing
developers easy way to store their content into Postgres DB.

# 1.1 Use cases

1. Template content should be stored into PostgreSQL DB;
2. Template content may change over time in live time;

# 2. Quick start

> To use `Templaty.Postgres` you have to enable `Templaty` as well.

First of all, you need to import `Templaty.Postgres` as package;

```shell
dotnet add package Templaty.Postgres
```

## 2.1 ASP.NET with EF Core

To use **Templaty.Postgres** into ASP.NET projects just using into `Startup` or `Program`:

```csharp
builder.Services.UseTemplaty(configator => configator.AddResourceStoreAssembly(typeof(Program).Assembly));
builder.Services.AddTemplatyPostgresStore<YourDbContext>();
```

**Templaty.Postgres** requires **EF Core** library to use Postgres as store.
Therefore you need to add `ContentConfiguration` into your DbContext or write it by your self.

```csharp
public sealed class YourDbContext : DbContext
{
    //..

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ContentConfiguration());
        // ...
    }

    // ...
}
```

After database will be updated you will be able to use Postgres as the store.

## 2.2 Make your own template

Let's look at an example into project `Templaty.Templaty.PostgresStoreSample` (see `Samples`).

1. Create template content and put it into table `contents`;

```
Date: {{date}}
Temperature: {{temperature}} °C
Summary: {{summary}}
```

2. Create a model with data `WheaterDistributionTemplate`;

```csharp
[Template.Source("Notifications_WheaterDistributionTemplate", Template.StoreType.Postgres)]
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
