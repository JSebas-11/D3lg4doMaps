using D3lg4doMaps.Routes.Public.Models.Requests.Common;

namespace D3lg4doMaps.Routes.Internal.Models.DTOs;

internal sealed class RouteMatrixOriginDto {
    public Waypoint Waypoint { get; internal set; } = null!;
    public RouteModifiersDto? RouteModifiers { get; internal set; }
}