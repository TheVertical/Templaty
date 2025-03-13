using Templaty.Abstractions;

namespace Templaty.Tests.Models;

[Template.Source("Templaty.Tests.Resources.TestTemplate.sbn", Template.StoreType.Resources)]
internal sealed class TestTemplate : ITemplate
{
    public string? String { get; set; }
    public int? Number { get; set; }
    public double? DoubleNumber { get; set; }
    public decimal? DecimalNumber { get; set; }
}

[Template.Source("DefaultTestTemplate.sbn", Template.StoreType.Localizations, "default")]
internal sealed class DefaultTestTemplate : ITemplate
{
    public string? String { get; set; }
    public int? Number { get; set; }
    public double? DoubleNumber { get; set; }
    public decimal? DecimalNumber { get; set; }
}

[Template.Source("AnotherTestTemlpate", Template.StoreType.Localizations, "another")]
internal sealed class AnotherTestTemlpate : ITemplate
{
    public string? String { get; set; }
    public int? Number { get; set; }
    public double? DoubleNumber { get; set; }
    public decimal? DecimalNumber { get; set; }
}