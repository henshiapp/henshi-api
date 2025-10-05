using Henshi.Flashcards.Flashcards.Models;
using Henshi.Shared.Dtos;
using Henshi.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Henshi.Flashcards.Flashcards.Features.GetFlashcard.V1;

public class GetFlashcardEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(
            "/v1/card-collections/{collectionId:guid}/flashcards/{id:guid}",
            async(Guid id, [FromServices] GetFlashcardQueryHandler handler) =>
        {
            var flashcard = await handler.Handle(new GetFlashcardQuery(
                id
            ));

            return Results.Ok(ApiResponse<Flashcard>.Success(flashcard!));
        });
    }
}
