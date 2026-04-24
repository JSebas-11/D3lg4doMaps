namespace DelgadoMaps.Routes.Models.Common;

/// <summary>
/// Represents travel advisory information for a route.
/// </summary>
/// <remarks>
/// Includes traffic conditions, restrictions, and transit-related data.
/// </remarks>
public sealed class RouteTravelAdvisory {
    /// <summary>
    /// Gets the list of speed intervals along the route.
    /// </summary>
    public IReadOnlyList<SpeedInterval> SpeedReadingIntervals { get; internal set; } = [];

    /// <summary>
    /// Gets whether route restrictions were partially ignored.
    /// </summary>
    public bool? RouteRestrictionsPartiallyIgnored { get; internal set; }
    
    /// <summary>
    /// Gets the transit fare for the route, if applicable.
    /// </summary>
    public Money? TransitFare { get; internal set; }
}