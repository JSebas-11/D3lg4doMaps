namespace DelgadoMaps.Core.Models.Geometry;

/// <summary>
/// Represents a geographic coordinate defined by a latitude and longitude pair.
/// </summary>
/// <remarks>
/// This model is used throughout the SDK to represent locations returned by
/// Google Maps APIs or provided as input for geographic queries.
///
/// Instances are created internally by the SDK to ensure coordinate validity
/// and consistency across modules.
/// </remarks>
public sealed class LatLng {
    /// <summary>
    /// Gets the latitude component of the coordinate in decimal degrees.
    /// </summary>
    public double Latitude { get; }
    
    /// <summary>
    /// Gets the longitude component of the coordinate in decimal degrees.
    /// </summary>
    public double Longitude { get; }

    internal LatLng(double latitude, double longitude) {
        Latitude = latitude;
        Longitude = longitude;
    }
}