using D3lg4doMaps.Routes.Public.Enums;

namespace D3lg4doMaps.Routes.Public.Models.DistanceMatrix.Components;

/// <summary>
/// Represents fallback routing information for a matrix element.
/// </summary>
/// <remarks>
/// Indicates that the result was computed using an alternative strategy
/// due to constraints such as latency or server limitations.
/// </remarks>
public sealed class FallbackInfo {
    /// <summary>
    /// Gets the routing mode used for fallback computation.
    /// </summary>
    public RoutingMode RoutingMode { get; internal set; }

    /// <summary>
    /// Gets the reason why fallback was applied.
    /// </summary>
    public Reason Reason { get; internal set; }
}