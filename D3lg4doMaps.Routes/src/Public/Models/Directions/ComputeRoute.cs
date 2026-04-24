using DelgadoMaps.Routes.Models.Common;
using DelgadoMaps.Routes.Models.Components;

namespace DelgadoMaps.Routes.Models.Directions;

/// <summary>
/// Represents a fully computed route between an origin and a destination.
/// </summary>
/// <remarks>
/// A route contains aggregated information such as distance, duration,
/// geometry, navigation legs, and additional metadata.
/// </remarks>
public sealed class ComputeRoute {
    #region METRICS
     /// <summary>
    /// Gets the total distance of the route in meters.
    /// </summary>
    public int DistanceMeters { get; internal set; }

    /// <summary>
    /// Gets the estimated travel duration.
    /// </summary>
    public string Duration { get; internal set; } = null!;

    /// <summary>
    /// Gets the static (traffic-unaware) duration, if available.
    /// </summary>
    public string? StaticDuration { get; internal set; }
    #endregion

    #region DESCRIPTION
    /// <summary>
    /// Gets a textual description of the route.
    /// </summary>
    public string? Description { get; internal set; }

    /// <summary>
    /// Gets labels describing route characteristics (e.g., tolls, highways).
    /// </summary>
    public IReadOnlyList<string> RouteLabels { get; internal set; } = [];
    #endregion

    #region GEOMETRY
    /// <summary>
    /// Gets the viewport that bounds the entire route.
    /// </summary>
    public Viewport? Viewport { get; internal set; }

    /// <summary>
    /// Gets the encoded polyline representing the route geometry.
    /// </summary>
    public Polyline? Polyline { get; internal set; }
    #endregion

    #region  NAVIGATION
    /// <summary>
    /// Gets the list of route legs.
    /// </summary>
    /// <remarks>
    /// Each leg represents a segment of the route between waypoints.
    /// </remarks>
    public IReadOnlyList<RouteLeg> Legs { get; internal set; } = [];
    #endregion

    #region  EXTRAS    
    /// <summary>
    /// Gets a token representing this route.
    /// </summary>
    /// <remarks>
    /// Can be used for advanced scenarios such as route rehydration or tracking.
    /// </remarks>
    public string? RouteToken { get; internal set; }

    /// <summary>
    /// Gets warnings associated with the route.
    /// </summary>
    public IReadOnlyList<string> Warnings { get; internal set; } = [];

    /// <summary>
    /// Gets travel advisory information for the route.
    /// </summary>
    public RouteTravelAdvisory? TravelAdvisory { get; internal set; }

    /// <summary>
    /// Gets localized values such as formatted distance and duration.
    /// </summary>
    public RouteLocalizedValues? LocalizedValues { get; internal set; }

     /// <summary>
    /// Gets the optimized waypoint order, if waypoint optimization was requested.
    /// </summary>
    public IReadOnlyList<int> OptimizedWaypointOrder { get; internal set; } = [];
    #endregion
}