using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Templaty.DependencyInjections;
using Templaty.Postgres.DependencyInjections;
using Templaty.PostgresStoreSample.Persistence;

var currentCulture = new CultureInfo("ru-RU");
CultureInfo.CurrentCulture = currentCulture;
CultureInfo.CurrentUICulture = currentCulture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    (provider, optionsBuilder) =>
    {
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Templaty"));

        var logger = provider.GetRequiredService<ILogger<DbContext>>();
        optionsBuilder.LogTo(message => logger.LogInformation(message), LogLevel.Information);
        optionsBuilder.EnableSensitiveDataLogging();
    }
);

builder.Services.UseTemplaty(configator => configator.AddResourceStoreAssembly(typeof(Program).Assembly));
builder.Services.AddTemplatyPostgresStore<AppDbContext>();

builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRequestLocalization();

app.MapControllers();

app.Run();