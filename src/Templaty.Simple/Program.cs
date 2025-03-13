using System.Globalization;
using Templaty.Abstractions;
using Templaty.DependencyInjections;
using Templaty.Simple.Resources;

var currentCulture = new CultureInfo("ru-RU");
CultureInfo.CurrentCulture = currentCulture;
CultureInfo.CurrentUICulture = currentCulture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add localizations and store for it
builder.Services.AddLocalization();
builder.Services.AddRequestLocalization(
    options =>
    {
        options.SupportedCultures = new List<CultureInfo> {new("en"), new("ru")};
        options.SetDefaultCulture("ru");
    }
);
builder.Services.AddSingleton<ITemplateContentStoreFactory, LocalizableResourceStoreFactory>();

builder.Services.UseTemplaty(configator => configator.AddResourceStoreAssembly(typeof(Program).Assembly));

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