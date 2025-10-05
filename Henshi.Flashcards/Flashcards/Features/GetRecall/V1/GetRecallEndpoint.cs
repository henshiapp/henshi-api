using Henshi.Flashcards.Flashcards.Models;
using Henshi.Shared.Dtos;
using Henshi.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Henshi.Flashcards.Flashcards.Features.GetFlashcard.V1;

public class GetRecallEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(
            "/v1/card-collections/{collectionId:guid}/flashcards/recall",
            async(Guid collectionId, [FromServices] GetRecallQueryHandler handler) =>
        {
            var avaliableForRecall = await handler.Handle(new GetRecallQuery(
                collectionId
            ));

            return Results.Ok(ApiResponse<List<Flashcard>>.Success(avaliableForRecall, null));
        });
    }
}
