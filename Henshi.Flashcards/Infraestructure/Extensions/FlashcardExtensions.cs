using System;
using Henshi.Flashcards.Infraestructure;
using Henshi.Flashcards.Infraestructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Henshi.Flashcards.Application.Extensions;

public static class FlashcardServiceExtensions
{
    public static IServiceCollection AddFlashcardServices(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        string? connectionString = configurationManager.GetConnectionString("FlashcardsConnectionString");

        services.AddDbContext<FlashcardDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IFlashcardCollectionRepository, EfFlashcardCollectionRepository>();
        services.AddScoped<IFlashcardCollectionsService, FlashcardCollectionsService>();

        return services;
    }
}
