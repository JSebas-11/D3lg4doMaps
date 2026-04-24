using DelgadoMaps.Routes.Models.Components;
using DelgadoMaps.Routes.Models.Directions.Components;

namespace DelgadoMaps.Routes.Models.Directions;

/// <summary>
/// Represents a segment of a route between two waypoints.
/// </summary>
/// <remarks>
/// A route is composed of one or more legs, each containing detailed
/// navigation steps and segment-level metadata.
/// </remarks>
public sealed class RouteLeg {
    #region METRICS
    /// <summary>
    /// Gets the distance of the leg in meters.
    /// </summary>
    public int DistanceMeters { get; internal set; }

    /// <summary>
    /// Gets the estimated duration of the leg.
    /// </summary>
    public string Duration { get; internal set; } = null!;
    #endregion

    #region LOCATION
    /// <summary>
    /// Gets the starting location of the leg.
    /// </summary>
    public Location? StartLocation { get; internal set; }

    /// <summary>
    /// Gets the ending location of the leg.
    /// </summary>
    public Location? EndLocation { get; internal set; }
    #endregion

    #region GEOMETRY
    /// <summary>
    /// Gets the encoded polyline representing the leg geometry.
    /// </summary>
    public Polyline? Polyline { get; internal set; }
    #endregion

    #region STEPS
    /// <summary>
    /// Gets the list of detailed navigation steps within the leg.
    /// </summary>
    public IReadOnlyList<RouteStep> Steps { get; internal set; } = [];
    #endregion

    #region EXTRAS    
    /// <summary>
    /// Gets travel advisory information specific to this leg.
    /// </summary>
    public RouteLegTravelAdvisory? TravelAdvisory { get; internal set; }

    /// <summary>
    /// Gets localized values such as formatted distance and duration.
    /// </summary>
    public RouteLegLocalizedValues? LocalizedValues { get; internal set; }

    /// <summary>
    /// Gets a summarized overview of the steps in this leg.
    /// </summary>
    public StepsOverview? Overview { get; internal set; }
    #endregion
}