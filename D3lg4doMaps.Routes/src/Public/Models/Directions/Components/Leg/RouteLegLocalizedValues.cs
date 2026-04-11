namespace D3lg4doMaps.Routes.Public.Models.Directions.Components;

/// <summary>
/// Represents localized (human-readable) values for a route leg.
/// </summary>
/// <remarks>
/// These values are formatted according to locale preferences,
/// making them suitable for direct display in UI.
/// </remarks>
public sealed class RouteLegLocalizedValues {
    /// <summary>
    /// Gets the formatted distance (e.g., "5 km", "3 mi").
    /// </summary>
    public string? Distance { get; internal set; }

    /// <summary>
    /// Gets the formatted duration (e.g., "10 min").
    /// </summary>
    public string? Duration { get; internal set; }
}