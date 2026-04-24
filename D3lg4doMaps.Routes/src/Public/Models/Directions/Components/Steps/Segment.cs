using DelgadoMaps.Routes.Enums;
using DelgadoMaps.Routes.Models.Components;

namespace DelgadoMaps.Routes.Models.Directions.Components;

/// <summary>
/// Represents a summarized navigation segment within a route.
/// </summary>
/// <remarks>
/// Segments group multiple steps into higher-level navigation instructions.
/// </remarks>
public sealed class Segment {
    /// <summary>
    /// Gets the navigation instruction for the segment.
    /// </summary>
    public NavigationInstruction? NavigationInstruction { get; internal set; }

    /// <summary>
    /// Gets the travel mode used in this segment.
    /// </summary>
    public TravelMode? TravelMode { get; internal set; }
    
    /// <summary>
    /// Gets the starting index within the route polyline.
    /// </summary>
    public int StartIndex { get; internal set; }

    /// <summary>
    /// Gets the ending index within the route polyline.
    /// </summary>
    public int EndIndex { get; internal set; }
}