using D3lg4doMaps.Core.Public.Models.Geometry;

namespace D3lg4doMaps.Routes.Public.Models.Components;

/// <summary>
/// Represents a geographic location.
/// </summary>
/// <remarks>
/// Combines latitude/longitude coordinates with optional directional metadata.
/// </remarks>
public sealed class Location {
    /// <summary>
    /// Gets the geographic coordinates.
    /// </summary>    
    public LatLng LatLng { get; internal set; } = null!;

    /// <summary>
    /// Gets the heading (direction) in degrees.
    /// </summary>
    /// <remarks>
    /// Value ranges from 0 to 360, where 0 represents north.
    /// </remarks>
    public int? Heading { get; internal set; }
}