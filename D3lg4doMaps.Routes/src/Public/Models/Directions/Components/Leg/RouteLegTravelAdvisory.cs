using DelgadoMaps.Routes.Models.Common;

namespace DelgadoMaps.Routes.Models.Directions.Components;

/// <summary>
/// Represents traffic and travel-related advisory information for a route leg.
/// </summary>
/// <remarks>
/// Includes traffic conditions along segments of the route.
/// </remarks>
public sealed class RouteLegTravelAdvisory {
    /// <summary>
    /// Gets the list of speed intervals along the route.
    /// </summary>
    public IReadOnlyList<SpeedInterval> SpeedReadingIntervals { get; internal set; } = [];
}