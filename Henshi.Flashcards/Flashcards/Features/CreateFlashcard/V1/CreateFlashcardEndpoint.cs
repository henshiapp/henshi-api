using Henshi.Shared.Dtos;
using Henshi.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Henshi.Flashcards.Flashcards.Features.CreateFlashcard.V1;

public class CreateFlashcardEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost(
            "/v1/card-collections/{collectionId:guid}/flashcards",
            async(Guid collectionId, [FromBody] CreateFlashcardRequest request, [FromServices] CreateFlashcardCommandHandler handler) =>
        {
            await handler.Handle(new CreateFlashcardCommand(
                collectionId,
                request.Question,
                request.Answer
            ));

            return Results.Ok(ApiResponse<Task>.Success("Flashcard created successfully!"));
        });
    }
}
