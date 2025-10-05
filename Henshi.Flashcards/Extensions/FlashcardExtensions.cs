using Henshi.Flashcards.Database;
using Henshi.Flashcards.Domain.Repositories;
using Henshi.Flashcards.FlashcardCollections.Features.CreateFlashcardCollection.V1;
using Henshi.Flashcards.FlashcardCollections.Features.DeleteFlashcardCollection.V1;
using Henshi.Flashcards.FlashcardCollections.Features.GetFlashcardCollection.V1;
using Henshi.Flashcards.FlashcardCollections.Features.ListFlashcardCollections.V1;
using Henshi.Flashcards.FlashcardCollections.Features.UpdateFlashcardCollection.V1;
using Henshi.Flashcards.FlashcardCollections.Repositories;
using Henshi.Flashcards.Flashcards.Features.CreateFlashcard.V1;
using Henshi.Flashcards.Flashcards.Features.DeleteFlashcard.V1;
using Henshi.Flashcards.Flashcards.Features.GetFlashcard.V1;
using Henshi.Flashcards.Flashcards.Features.ListFlashcards.V1;
using Henshi.Flashcards.Flashcards.Features.SaveRecall.V1;
using Henshi.Flashcards.Infraestructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Henshi.Shared.Extensions;
using Henshi.Flashcards.Stats.Features.V1.GetStats;

namespace Henshi.Flashcards.Extensions;

public static class FlashcardServiceExtensions
{
    public static IServiceCollection AddFlashcardServices(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        string? connectionString = configurationManager.GetConnectionString("FlashcardsConnectionString");

        services.AddDbContext<FlashcardDbContext>(options => options.UseNpgsql(connectionString));

        // Collections
        services.AddScoped<IFlashcardCollectionRepository, EfFlashcardCollectionRepository>();
        services.AddScoped<CreateFlashcardCollectionCommandHandler>();
        services.AddScoped<GetFlashcardCollectionQueryHandler>();
        services.AddScoped<DeleteFlashcardCollectionCommandHandler>();
        services.AddScoped<UpdateFlashcardCollectionCommandHandler>();
        services.AddScoped<ListFlashcardCollectionsQueryHandler>();

        // Flashcards
        services.AddScoped<IFlashcardRepository, EfFlashcardRepository>();
        services.AddScoped<CreateFlashcardCommandHandler>();
        services.AddScoped<DeleteFlashcardCommandHandler>();
        services.AddScoped<GetFlashcardQueryHandler>();
        services.AddScoped<GetRecallQueryHandler>();
        services.AddScoped<ListFlashcardsQueryHandler>();
        services.AddScoped<SaveRecallCommandHandler>();
        services.AddScoped<UpdateFlashcardCommandHandler>();

        // Stats
        services.AddScoped<GetStatsQueryHandler>();

        return services;
    }

    public static WebApplicationBuilder AddFlashcardModules(this WebApplicationBuilder builder)
    {
        builder.AddMinimalEndpoints(assemblies: typeof(FlashcardRoot).Assembly);

        return builder;
    }
}
