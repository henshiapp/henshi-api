using Henshi.Flashcards.FlashcardCollections.Models;
using Henshi.Shared.Dtos;
using Henshi.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Henshi.Flashcards.FlashcardCollections.Features.GetFlashcardCollection.V1;

public class GetFlashcardCollectionEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(
            "/v1/card-collections/{id:guid}",
            async (Guid id, [FromServices] GetFlashcardCollectionQueryHandler handler) =>
        {
            var collection = await handler.Handle(new GetFlashcardCollectionQuery(
                id
            ));

            return Results.Ok(ApiResponse<FlashcardCollection>.Success(collection!));
        });
    }
}
