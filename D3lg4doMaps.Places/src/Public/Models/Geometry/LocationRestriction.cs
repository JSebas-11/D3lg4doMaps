namespace DelgadoMaps.Places.Models.Geometry;

/// <summary>
/// Represents a geographic restriction applied to a Places request.
/// </summary>
/// <remarks>
/// A location restriction strictly limits results to the specified area,
/// ensuring that returned places fall within the defined boundary.
/// </remarks>
public sealed class LocationRestriction {
    /// <summary>
    /// Gets the circular area used to restrict results.
    /// </summary>
    public GeoCircle? Circle { get; internal set; }
}