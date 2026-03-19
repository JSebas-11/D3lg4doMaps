using D3lg4doMaps.Routing.Public.Models.Requests;

namespace D3lg4doMaps.Routing.Public.Abstractions;

public interface IDirectionsService {
    Task<RouteCollection> GetRouteAsync(RouteRequest routeRequest);
}