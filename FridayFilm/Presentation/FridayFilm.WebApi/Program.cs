using FridayFilm.Application.Settings;
using FridayFilm.Infrastructure;
using FridayFilm.Persistence;
using FridayFilm.Persistence.Contexts;
using FridayFilm.WebApi.ExceptionHandlers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; // Enum konvertasiyası üçün mütləqdir

var builder = WebApplication.CreateBuilder(args);

// DƏYİŞİKLİK BURADADIR: Enum-ları mətn kimi oxumaq üçün konfiqurasiya
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services
    .AddOptions<CloudinarySettings>()
    .Bind(builder.Configuration.GetSection(
        CloudinarySettings.SectionName))
    .Validate(
        settings => !string.IsNullOrWhiteSpace(settings.CloudName),
        "Cloudinary CloudName tələb olunur.")
    .Validate(
        settings => !string.IsNullOrWhiteSpace(settings.ApiKey),
        "Cloudinary ApiKey tələb olunur.")
    .Validate(
        settings => !string.IsNullOrWhiteSpace(settings.ApiSecret),
        "Cloudinary ApiSecret tələb olunur.")
    .ValidateOnStart();

builder.Services.AddInfrastructure();
builder.Services.AddPersistence();
// Swagger üçün lazımlı konfiqurasiyalar
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FridayFilmDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Swagger açılanda birbaşa endpointləri görmək üçün
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FridayFilm API v1");
        c.RoutePrefix = string.Empty; // localhost:port yazan kimi birbaşa Swagger açılsın
    });
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();