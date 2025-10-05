using Henshi.Flashcards.Flashcards.Models;
using Henshi.Shared.Dtos;
using Henshi.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Henshi.Flashcards.Flashcards.Features.DeleteFlashcard.V1;

public class DeleteFlashcardEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapDelete(
            "/v1/card-collections/{collectionId:guid}/flashcards/{id:guid}",
            async(Guid id, [FromServices] DeleteFlashcardCommandHandler handler) =>
        {
            await handler.Handle(new DeleteFlashcardCommand(
                id
            ));

            return Results.Ok(ApiResponse<Flashcard>.Success("Flashcard deleted"));
        });
    }
}
