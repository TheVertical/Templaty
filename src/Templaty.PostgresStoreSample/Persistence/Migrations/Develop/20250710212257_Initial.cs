using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Templaty.PostgresStoreSample.Persistence.Migrations.Develop
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contents",
                columns: table => new
                {
                    Path = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contents", x => x.Path);
                });

            migrationBuilder.InsertData(
                table: "contents",
                columns: new[] { "Path", "Body" },
                values: new object[,]
                {
                    { "Notifications_WheaterDistributionTemplate", "Date: {{date}}\nTemperature: {{temperature}} °C\nSummary: {{summary}}" },
                    { "Templates_Hello", "Hello {{name}} {{second_name}}!" },
                    { "Templates_HelloWorld", "Hello world!" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_contents_Path",
                table: "contents",
                column: "Path",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contents");
        }
    }
}
