using Henshi.Flashcards.Flashcards.Models;
using Henshi.Shared.Dtos;
using Henshi.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Henshi.Flashcards.Flashcards.Features.ListFlashcards.V1;

public class ListFlashcardsEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(
            "/v1/card-collections/{collectionId:guid}/flashcards",
            async(Guid collectionId,
                [FromServices] ListFlashcardsQueryHandler handler,
                [FromQuery] string? search,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10
        ) =>
        {
            var (flashcards, metadata) = await handler.Handle(new ListFlashcardsQuery(
                collectionId,
                search,
                page,
                pageSize
            ));

            return Results.Ok(
                ApiResponse<List<Flashcard>>.Success(flashcards, metadata)
            );
        });
    }
}
