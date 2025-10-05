using Henshi.Flashcards.FlashcardCollections.Models;
using Henshi.Shared.Dtos;
using Henshi.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Henshi.Flashcards.FlashcardCollections.Features.ListFlashcardCollections.V1;

public class ListFlashcardCollectionsEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(
            "/v1/card-collections",
        async ([FromServices] ListFlashcardCollectionsQueryHandler handler,
                [FromQuery] string? search,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10
        ) =>
        {
            var (collections, metadata) = await handler.Handle(new ListFlashcardCollectionsQuery(
                search,
                page,
                pageSize
            ));

            return Results.Ok(
                ApiResponse<List<FlashcardCollection>>.Success(collections, metadata)
            );
        });
    }
}
