using Henshi.Shared.Dtos;
using Henshi.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Henshi.Flashcards.FlashcardCollections.Features.UpdateFlashcardCollection.V1;

public class UpdateFlashcardCollectionEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPatch(
            "/v1/card-collections/{collectionId:guid}",
            async(Guid collectionId, [FromBody] UpdateFlashcardCollectionRequest request, [FromServices] UpdateFlashcardCollectionCommandHandler handler) =>
        {
            await handler.Handle(new UpdateFlashcardCollectionCommand(
                collectionId,
                request.Title,
                request.Description,
                request.Icon
            ));

            return Results.Ok(ApiResponse<Task>.Success("Flashcard collection updated successfully!"));
        });
    }
}
