using FluentValidation.AspNetCore; // YENİ: AutoValidation üçün mütləqdir
using FridayFilm.Application;
using FridayFilm.Application.Authorization;
using FridayFilm.Application.Settings;
using FridayFilm.Infrastructure.Services;
using FridayFilm.Persistence;
using FridayFilm.Persistence.Contexts;
using FridayFilm.Persistence.Seed;
using FridayFilm.WebApi.ExceptionHandlers;
using FridayFilm.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Identity;
using FridayFilm.Persistence.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("Frontend", policy =>
{
    var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
    if (origins.Length > 0) policy.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
}));
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("auth", context => RateLimitPartition.GetFixedWindowLimiter(
        context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
        _ => new FixedWindowRateLimiterOptions { PermitLimit = 5, Window = TimeSpan.FromMinutes(1), QueueLimit = 0 }));
});

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
        options => !string.IsNullOrWhiteSpace(options.Issuer))
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.Audience))
    .Validate(
        options => options.RefreshTokenExpirationDays > 0)
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.SecretKey)
                   && options.SecretKey.Length >= 32)
    .Validate(
        options => options.AccessTokenExpirationMinutes > 0)
    .ValidateOnStart();
var jwtOptions = builder.Configuration
    .GetRequiredSection(JwtOptions.SectionName)
    .Get<JwtOptions>()
    ?? throw new InvalidOperationException();

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
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var manager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                var id = context.Principal?.FindFirst("sub")?.Value;
                var user = id is null ? null : await manager.FindByIdAsync(id);
                if (user is null || !user.EmailConfirmed || await manager.IsLockedOutAsync(user))
                {
                    context.Fail("Hesab təsdiqlənməyib və ya bloklanıb.");
                    return;
                }
                // Enforce current role permissions, including changes made after token issuance.
                var identity = (System.Security.Claims.ClaimsIdentity)context.Principal!.Identity!;
                foreach (var claim in identity.FindAll(CustomClaimTypes.Permission).ToArray())
                    identity.RemoveClaim(claim);
                var roleManager = context.HttpContext.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();
                var permissions = new HashSet<string>(StringComparer.Ordinal);
                foreach (var roleName in await manager.GetRolesAsync(user))
                {
                    var role = await roleManager.FindByNameAsync(roleName);
                    if (role is null) continue;
                    foreach (var claim in await roleManager.GetClaimsAsync(role))
                        if (claim.Type == CustomClaimTypes.Permission) permissions.Add(claim.Value);
                }
                foreach (var permission in permissions)
                    identity.AddClaim(new System.Security.Claims.Claim(CustomClaimTypes.Permission, permission));
            }
        };

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

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy =
        new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

    foreach (var permission in Permissions.All)
    {
        options.AddPolicy(
            permission,
            policy => policy.RequireClaim(
                CustomClaimTypes.Permission,
                permission));
    }
});

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

if (builder.Configuration.GetValue("Database:SeedRoles", true))
{
    await AdminRoleSeeder.SeedAsync(app.Services);
    await UserRoleSeeder.SeedAsync(app.Services);
}

app.UseMiddleware<GlobalExceptionHandler>();
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    var message = response.StatusCode switch
    {
        401 => "Davam etmək üçün hesabınıza daxil olun.",
        403 => "Bu əməliyyat üçün icazəniz yoxdur.",
        404 => "Resurs tapılmadı.",
        429 => "Çox sorğu göndərildi. Bir dəqiqə sonra yenidən sınayın.",
        _ => "Sorğu yerinə yetirilə bilmədi."
    };
    await response.WriteAsJsonAsync(new { StatusCode = response.StatusCode, Message = message,
        Path = context.HttpContext.Request.Path.Value, TraceId = context.HttpContext.TraceIdentifier });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Swagger açılanda birbaşa endpointləri görmək üçün
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FridayFilm API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("Frontend");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
