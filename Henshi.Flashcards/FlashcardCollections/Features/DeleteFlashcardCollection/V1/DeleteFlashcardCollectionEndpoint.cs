using Henshi.Shared.Dtos;
using Henshi.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Henshi.Flashcards.FlashcardCollections.Features.DeleteFlashcardCollection.V1;

public class DeleteFlashcardCollectionEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapDelete(
            "/v1/card-collections/{collectionId:guid}",
            async(Guid collectionId, [FromServices] DeleteFlashcardCollectionCommandHandler handler) =>
        {
            await handler.Handle(new DeleteFlashcardCollectionCommand(
                collectionId
            ));

            return Results.Ok(ApiResponse<Task>.Success("Collection deleted"));
        });
    }
}
