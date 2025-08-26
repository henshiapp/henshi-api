using System.Text.Json.Serialization;
using Auth0.AspNetCore.Authentication;
using Henshi.Flashcards.Infraestructure.Database;
using Henshi.Flashcards.Infraestructure.Extensions;
using Henshi.Flashcards.Presentation.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            options.Authority = builder.Configuration["Auth0:Domain"];
            options.Audience = builder.Configuration["Auth0:Audience"];
        });
    builder.Services.AddAuthorization();

    builder.Services.AddOpenApi();

    builder.Services.AddControllers()
        .AddApplicationPart(typeof(FlashcardCollectionsController).Assembly)
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

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

    // app.UseHttpsRedirection();
    app.UseCors(option =>
        option
            .WithOrigins("http://localhost:3000")
            .AllowCredentials()
            .AllowAnyHeader()
    );

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}

app.Run();