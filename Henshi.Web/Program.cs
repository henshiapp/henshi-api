using System.Text.Json.Serialization;
using Henshi.Flashcards.Database;
using Henshi.Flashcards.Extensions;
using Henshi.Shared.Extensions;
using Henshi.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = $"https://{builder.Configuration["AUTH0_DOMAIN"]}";
            options.Audience = builder.Configuration["AUTH0_AUDIENCE"];
        });
    builder.Services.AddAuthorization(options =>
    {
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
    });
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

    builder.Services.AddOpenApi();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

    builder.Services.Configure<JsonOptions>(options =>
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    builder.AddFlashcardModules();

    builder.Services.AddFlashcardServices(builder.Configuration);
}

var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<FlashcardDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        var pendingMigrations = dbContext.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
        {
            logger.LogInformation("Applying pending migrations...");
            dbContext.Database.Migrate();
            logger.LogInformation("Migrations applied successfully.");
        }
        else
        {
            logger.LogInformation("No pending migrations found.");
        }
    }

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseCors("AllowAll");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapMinimalEndpoints();
}

app.Run();