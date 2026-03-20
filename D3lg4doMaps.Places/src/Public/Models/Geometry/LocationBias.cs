namespace D3lg4doMaps.Places.Public.Models.Geometry;

/// <summary>
/// Represents a geographic bias applied to a Places request.
/// </summary>
/// <remarks>
/// A location bias influences the ranking of results toward a specific area
/// but does not strictly restrict results to that area.
/// </remarks>
public sealed class LocationBias {
    /// <summary>
    /// Gets the circular area used to bias results.
    /// </summary>
    public GeoCircle? Circle { get; internal set; }
}