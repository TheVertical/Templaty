using Templaty.Abstractions;

namespace Templaty.PostgresStoreSample.Wheater.Notifications;

[Template.Source("Notifications_WheaterDistributionTemplate", Template.StoreType.Postgres)]
internal sealed record WheaterDistributionTemplate(DateOnly Date, int Temperature, string? Summary) : ITemplate;