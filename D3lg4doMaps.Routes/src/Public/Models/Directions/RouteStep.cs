using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Components;
using D3lg4doMaps.Routes.Public.Models.Directions.Components;

namespace D3lg4doMaps.Routes.Public.Models.Directions;

/// <summary>
/// Represents a single navigation step within a route leg.
/// </summary>
/// <remarks>
/// Steps provide fine-grained navigation instructions such as turns,
/// distances, and travel modes.
/// </remarks>
public sealed class RouteStep {
    #region METRICS
    /// <summary>
    /// Gets the distance of the step in meters.
    /// </summary>
    public int DistanceMeters { get; internal set; }

    /// <summary>
    /// Gets the static (traffic-unaware) duration of the step, if available.
    /// </summary>
    public string? StaticDuration { get; internal set; }
    #endregion

    #region LOCATION
    /// <summary>
    /// Gets the starting location of the step.
    /// </summary>
    public Location? StartLocation { get; internal set; }

    /// <summary>
    /// Gets the ending location of the step.
    /// </summary>
    public Location? EndLocation { get; internal set; }
    #endregion

    #region GEOMETRY
     /// <summary>
    /// Gets the encoded polyline representing the step geometry.
    /// </summary>
    public Polyline? Polyline { get; internal set; }
    #endregion

    #region NAVIGATION
    /// <summary>
    /// Gets the navigation instruction for this step.
    /// </summary>
    public NavigationInstruction? NavigationInstruction { get; internal set; }
    #endregion

    #region EXTRAS    
    /// <summary>
    /// Gets travel advisory information specific to this step.
    /// </summary>
    public RouteLegTravelAdvisory? TravelAdvisory { get; internal set; }

    /// <summary>
    /// Gets localized values such as formatted distance and static duration.
    /// </summary>
    public RouteLegStepLocalizedValues? LocalizedValues { get; internal set; }

    /// <summary>
    /// Gets the travel mode used in this step.
    /// </summary>
    public TravelMode? TravelMode { get; internal set; }
    #endregion
}