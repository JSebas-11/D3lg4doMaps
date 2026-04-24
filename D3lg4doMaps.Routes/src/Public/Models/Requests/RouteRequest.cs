using DelgadoMaps.Routes.Builders;
using DelgadoMaps.Routes.Enums;
using DelgadoMaps.Routes.Models.Requests.Common;

namespace DelgadoMaps.Routes.Models.Requests;

/// <summary>
/// Represents a request for calculating routes between an origin and a destination.
/// </summary>
/// <remarks>
/// This model is constructed using <see cref="RouteRequestBuilder"/> to ensure
/// all required fields and constraints are properly validated.
///
/// Supports advanced routing configuration such as intermediates, travel mode, 
/// routing preferences, polyline options, timing constraints, and waypoint optimization.
/// </remarks>
public sealed class RouteRequest {
    /// <summary>
    /// Gets the origin waypoint of the route.
    /// </summary>
    public Waypoint Origin { get; internal set; } = null!;

    /// <summary>
    /// Gets the destination waypoint of the route.
    /// </summary>
    public Waypoint Destination { get; internal set; } = null!;

    /// <summary>
    /// Gets the list of intermediate waypoints between origin and destination.
    /// </summary>
    public IReadOnlyList<Waypoint> Intermediates { get; internal set; } = [];

    /// <summary>
    /// Gets the travel mode used for routing.
    /// </summary>
    public TravelMode? TravelMode { get; internal set; }

    /// <summary>
    /// Gets the routing preference object.
    /// </summary>
    public RoutingPreference? RoutingPreference { get; internal set; }

    /// <summary>
    /// Gets the polyline quality for the returned route geometry.
    /// </summary>
    public PolylineQuality? PolylineQuality { get; internal set; }

    /// <summary>
    /// Gets the polyline encoding format.
    /// </summary>
    public PolylineEncoding? PolylineEncoding { get; internal set; }

    /// <summary>
    /// Gets the departure time for the route.
    /// </summary>
    public DateTimeOffset? DepartureTime { get; internal set; }

    /// <summary>
    /// Gets the arrival time for the route.
    /// </summary>
    public DateTimeOffset? ArrivalTime { get; internal set; }

    /// <summary>
    /// Gets whether alternative routes should be computed.
    /// </summary>
    public bool? ComputeAlternativeRoutes { get; internal set; }

    /// <summary>
    /// Gets whether intermediate waypoints should be optimized for efficiency.
    /// </summary>
    public bool? OptimizeWaypointOrder { get; internal set; }

    /// <summary>
    /// Gets route modifiers objects with properties such as avoiding tolls or highways.
    /// </summary>
    public RouteModifiers? RouteModifiers { get; internal set; }

    /// <summary>
    /// Gets the unit system used in the response.
    /// </summary>
    public Units? Units { get; internal set; }
}