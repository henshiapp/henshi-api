using Microsoft.AspNetCore.Routing;

namespace Henshi.Shared.Endpoints;

public interface IMinimalEndpoint
{
    void MapRoutes(IEndpointRouteBuilder routeBuilder);
}
