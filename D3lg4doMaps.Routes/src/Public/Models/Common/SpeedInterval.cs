using DelgadoMaps.Routes.Enums;

namespace DelgadoMaps.Routes.Models.Common;

/// <summary>
/// Represents a segment of the route with a specific traffic speed condition.
/// </summary>
/// <remarks>
/// Defined by polyline point indices, allowing mapping to route geometry.
/// </remarks>
public sealed class SpeedInterval {
    /// <summary>
    /// Gets the starting index within the route polyline.
    /// </summary>
    public int StartPolylinePointIndex { get; internal set; }

    /// <summary>
    /// Gets the ending index within the route polyline.
    /// </summary>
    public int EndPolylinePointIndex { get; internal set; }

    /// <summary>
    /// Gets the traffic speed condition for this interval.
    /// </summary>
    public Speed Speed { get; internal set; }
}