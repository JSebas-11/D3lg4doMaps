using System.Text.Json;
using D3lg4doMaps.Routing.Public.Enums;
using D3lg4doMaps.Routing.Public.Models.Directions;
using D3lg4doMaps.Routing.Public.Models.Requests;

namespace D3lg4doMaps.Routing.Public.Abstractions;

public interface IDirectionsService {
    Task<RouteResult> GetRoutesAsync(
        RouteRequest routeRequest, RouteDetailLevel detailLevel = RouteDetailLevel.Standard
    );
    Task<JsonDocument> GetRoutesRawAsync(RouteRequest routeRequest, params string[] fields);
}