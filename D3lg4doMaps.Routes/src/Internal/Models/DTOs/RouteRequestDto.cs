using DelgadoMaps.Routes.Models.Requests.Common;

namespace DelgadoMaps.Routes.Internal.Models.DTOs;

internal sealed class RouteRequestDto {
    public Waypoint Origin { get; internal set; } = null!;
    public Waypoint Destination { get; internal set; } = null!;
    public IReadOnlyList<Waypoint> Intermediates { get; internal set; } = [];
    public string? TravelMode { get; internal set; }
    public string? RoutingPreference { get; internal set; }
    public string? PolylineQuality { get; internal set; }
    public string? PolylineEncoding { get; internal set; }
    public DateTimeOffset? DepartureTime { get; internal set; }
    public DateTimeOffset? ArrivalTime { get; internal set; }
    public bool? ComputeAlternativeRoutes { get; internal set; }
    public bool? OptimizeWaypointOrder { get; internal set; }
    public RouteModifiersDto? RouteModifiers { get; internal set; }
    public string? Units { get; internal set; }
}