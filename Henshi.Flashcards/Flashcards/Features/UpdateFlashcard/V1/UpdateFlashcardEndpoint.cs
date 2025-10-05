using Henshi.Flashcards.Flashcards.Features.UpdateFlashcard.V1;
using Henshi.Shared.Dtos;
using Henshi.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Henshi.Flashcards.Flashcards.Features.CreateFlashcard.V1;

public class UpdateFlashcardEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPatch(
            "/v1/card-collections/{collectionId:guid}/flashcards/{id:guid}",
            async(Guid id, [FromBody] UpdateFlashcardRequest request, [FromServices] UpdateFlashcardCommandHandler handler) =>
        {
            await handler.Handle(new UpdateFlashcardCommand(
                id,
                request.Question,
                request.Answer
            ));

            return Results.Ok(ApiResponse<Task>.Success("Flashcard updated successfully!"));
        });
    }
}
