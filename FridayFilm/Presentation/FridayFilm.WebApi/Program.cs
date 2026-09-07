using FluentValidation.AspNetCore; // YENİ: AutoValidation üçün mütləqdir
using FridayFilm.Application;
using FridayFilm.Application.Settings;
using FridayFilm.Persistence;
using FridayFilm.Persistence.Contexts;
using FridayFilm.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using FridayFilm.WebApi.ExceptionHandlers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using FridayFilm.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // Yaratdığımız ValidationFilter-i bütün Controller-lərə qlobal tətbiq edirik
    options.Filters.Add<ValidationFilter>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// YENİ ƏLAVƏ EDİLƏN KOD: Validatorların avtomatik işə düşməsi üçün
builder.Services.AddFluentValidationAutoValidation();

// .NET-in öz avtomatik xəta qaytarma mexanizmini bağlayırıq ki, 
// yalnız bizim yazdığımız səliqəli ValidationFilter işləsin
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
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

builder.Services
    .AddOptions<JwtOptions>()
    .Bind(builder.Configuration.GetRequiredSection(
        JwtOptions.SectionName))
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.Issuer),
        "Requires JWT Issuer .")
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.Audience),
        "Requires JWT Audience .")
    .Validate(
        options => options.RefreshTokenExpirationDays > 0,
        "JWT RefreshTokenExpirationDays must be greater than zero.")
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.SecretKey)
                   && options.SecretKey.Length >= 32,
        "JWT SecretKey minimum 32 characters required.")
    .Validate(
        options => options.AccessTokenExpirationMinutes > 0,
        "JWT AccessTokenExpirationMinutes must be greater than zero.")
    .ValidateOnStart();
var jwtOptions = builder.Configuration
    .GetRequiredSection(JwtOptions.SectionName)
    .Get<JwtOptions>()
    ?? throw new InvalidOperationException(
        "JWT configuration was not found.");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        jwtOptions.SecretKey)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

                NameClaimType = "name",
                RoleClaimType = "role"
            };
    });

builder.Services.AddAuthorizationBuilder()
    .SetFallbackPolicy(new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build());

builder.Services.AddInfrastructure();
builder.Services.AddPersistence();

builder.Services.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    const string bearerScheme = "Bearer";

    options.AddSecurityDefinition(bearerScheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT access token daxil edin. 'Bearer' yazmağa ehtiyac yoxdur."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference(bearerScheme, document)] = []
    });
});

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
