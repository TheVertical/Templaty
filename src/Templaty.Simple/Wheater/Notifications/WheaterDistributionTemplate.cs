using Templaty.Abstractions;

namespace Templaty.Simple.Wheater.Notifications;

[Template.Source("Templaty.Simple.Wheater.Notifications.WheaterDistribution.txt", Template.StoreType.Resources)]
internal sealed record WheaterDistributionTemplate(DateOnly Date, int Temperature, string? Summary) : ITemplate;