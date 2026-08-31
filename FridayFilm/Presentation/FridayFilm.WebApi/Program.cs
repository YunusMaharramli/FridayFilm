using FridayFilm.Persistence;
using FridayFilm.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence();
builder.Services.AddControllers();

// Swagger üçün lazımlı konfiqurasiyalar
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FridayFilmDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();