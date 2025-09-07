using Henshi.Flashcards.Application.Services;
using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.Infraestructure.Database;
using Henshi.Flashcards.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Henshi.Flashcards.Infraestructure.Extensions;

public static class FlashcardServiceExtensions
{
    public static IServiceCollection AddFlashcardServices(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        string? connectionString = configurationManager.GetConnectionString("FlashcardsConnectionString");

        services.AddDbContext<FlashcardDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IFlashcardCollectionRepository, EfFlashcardCollectionRepository>();
        services.AddScoped<IFlashcardCollectionService, FlashcardCollectionService>();

        services.AddScoped<IFlashcardRepository, EfFlashcardRepository>();
        services.AddScoped<IFlashcardService, FlashcardService>();

        return services;
    }
}
