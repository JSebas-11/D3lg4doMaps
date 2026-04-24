using DelgadoMaps.Core.Models.Geometry;

namespace DelgadoMaps.Routes.Models.Components;

/// <summary>
/// Represents a rectangular geographic bounding box.
/// </summary>
/// <remarks>
/// Commonly used to define the visible region of a map.
/// </remarks>
public sealed class Viewport {
    /// <summary>
    /// Gets the south-west corner of the viewport.
    /// </summary>
    public LatLng Low { get; internal set; } = null!;

    /// <summary>
    /// Gets the north-east corner of the viewport.
    /// </summary>
    public LatLng High { get; internal set; } = null!;
}