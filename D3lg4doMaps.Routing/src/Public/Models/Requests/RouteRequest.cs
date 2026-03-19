using D3lg4doMaps.Routing.Public.Enums;
using D3lg4doMaps.Routing.Public.Models.Common;
using D3lg4doMaps.Routing.Public.Models.Routing;

namespace D3lg4doMaps.Routing.Public.Models.Requests;

public sealed class RouteRequest {
    public Waypoint Origin { get; internal set; } = null!;
    public Waypoint Destination { get; internal set; } = null!;
    public IReadOnlyList<Waypoint> Intermediates { get; internal set; } = [];
    public TravelMode? TravelMode { get; internal set; }
    public RoutingPreference? RoutingPreference { get; internal set; }
    public PolylineQuality? PolylineQuality { get; internal set; }
    public PolylineEncoding? PolylineEncoding { get; internal set; }
    public DateTimeOffset? DepartureTime { get; internal set; }
    public DateTimeOffset? ArrivalTime { get; internal set; }
    public bool? ComputeAlternativeRoutes { get; internal set; }
    public bool? OptimizeWaypointOrder { get; internal set; }
    public RouteModifiers? RouteModifiers { get; internal set; }
    public Units? Units { get; internal set; }
}