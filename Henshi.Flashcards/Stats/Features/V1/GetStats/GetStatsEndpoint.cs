using Henshi.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace Henshi.Flashcards.Stats.Features.V1.GetStats;

public class GetStatsEndpoint : IMinimalEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(
            "/v1/card-collections/stats",
            async([FromServices] GetStatsQueryHandler handler) =>
        {
            var stats = await handler.Handle();

            return Results.Ok(stats);
        });
    }
}
