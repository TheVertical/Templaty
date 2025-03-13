using Templaty.Abstractions;

namespace Templaty.Tests.Models;

internal sealed record WithoutAttributeTestTemplate : ITemplate;

[Template.Source(path: "localization.test.template", storeType: Template.StoreType.Localizations, storeName: "unknown")]
internal sealed class LocalizationTestTemplate : ITemplate;