using Templaty.Abstractions;

namespace Templaty.PostgresStoreSample.Resources;

internal class Templates
{
    // template without any variables
    [Template.Source("Templates_HelloWorld", Template.StoreType.Postgres)]
    public sealed record HelloWorld : ITemplate;

    // template with variables
    [Template.Source("Templates_Hello", Template.StoreType.Postgres)]
    public sealed record Hello(string Name, string SecondName) : ITemplate;
}