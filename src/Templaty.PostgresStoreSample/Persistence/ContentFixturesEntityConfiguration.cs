using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Templaty.Postgres;

namespace Templaty.PostgresStoreSample.Persistence;

internal sealed class ContentFixturesEntityConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.HasData(
            [
                new Content("Templates_HelloWorld", "Hello world!"),
                new Content("Templates_Hello", "Hello {{name}} {{second_name}}!"),
                new Content("Notifications_WheaterDistributionTemplate", "Date: {{date}}\nTemperature: {{temperature}} \u00b0C\nSummary: {{summary}}")
            ]
        );
    }
}