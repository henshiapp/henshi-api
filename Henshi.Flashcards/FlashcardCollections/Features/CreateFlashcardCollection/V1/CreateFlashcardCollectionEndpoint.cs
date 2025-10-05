using System.Security.Claims;
using Henshi.Shared.Dtos;
using Henshi.Shared.Endpoints;
using Henshi.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Henshi.Flashcards.FlashcardCollections.Features.CreateFlashcardCollection.V1;

public class CreateFlashcardCollectionEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost(
            "/v1/card-collections",
            async(
                [FromBody] CreateFlashcardCollectionRequest request,
                [FromServices] CreateFlashcardCollectionCommandHandler handler,
                ClaimsPrincipal user
            ) =>
        {
            await handler.Handle(new CreateFlashcardCollectionCommand(
                request.Title,
                request.Description,
                request.Icon,
                user.Id()!
            ));

            return Results.Ok(ApiResponse<Task>.Success("Flashcard collection created successfully!"));
        });
    }
}
