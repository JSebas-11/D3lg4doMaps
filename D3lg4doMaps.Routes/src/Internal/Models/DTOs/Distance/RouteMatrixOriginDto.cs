using DelgadoMaps.Routes.Models.Requests.Common;

namespace DelgadoMaps.Routes.Internal.Models.DTOs;

internal sealed class RouteMatrixOriginDto {
    public Waypoint Waypoint { get; internal set; } = null!;
    public RouteModifiersDto? RouteModifiers { get; internal set; }
}