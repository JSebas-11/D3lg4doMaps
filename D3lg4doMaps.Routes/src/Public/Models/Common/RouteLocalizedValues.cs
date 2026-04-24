namespace DelgadoMaps.Routes.Models.Common;

/// <summary>
/// Represents localized (human-readable) values for a route.
/// </summary>
/// <remarks>
/// These values are formatted for display purposes and may include units
/// and locale-specific formatting.
/// </remarks>
public sealed class RouteLocalizedValues {
    /// <summary>
    /// Gets the formatted distance.
    /// </summary>
    public string Distance { get; internal set; } = null!;

    /// <summary>
    /// Gets the formatted duration.
    /// </summary>
    public string Duration { get; internal set; } = null!;

    /// <summary>
    /// Gets the formatted static duration, if available.
    /// </summary>
    public string? StaticDuration { get; internal set; }

    /// <summary>
    /// Gets the formatted transit fare, if available.
    /// </summary>
    public string? TransitFare { get; internal set; }
}