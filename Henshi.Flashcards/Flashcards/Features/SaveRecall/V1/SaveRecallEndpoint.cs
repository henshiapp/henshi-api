using Henshi.Shared.Dtos;
using Henshi.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Henshi.Flashcards.Flashcards.Features.SaveRecall.V1;

public class SaveRecallEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost(
            "/v1/card-collections/{collectionId:guid}/flashcards/recall",
            async(Guid collectionId, [FromBody] SaveRecallFlashcardRequest request, [FromServices] SaveRecallCommandHandler handler) =>
        {
            await handler.Handle(new SaveRecallCommand(
                collectionId,
                request.Answers
            ));

            return Results.Ok(ApiResponse<Task>.Success("Recall saved successfully!"));
        });
    }
}
