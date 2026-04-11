using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Common;
using D3lg4doMaps.Routes.Public.Models.DistanceMatrix.Components;

namespace D3lg4doMaps.Routes.Public.Models.DistanceMatrix;

/// <summary>
/// Represents a single element in a distance matrix response.
/// </summary>
/// <remarks>
/// Each element corresponds to a specific origin–destination pair,
/// containing distance, duration, and additional routing metadata.
/// </remarks>
public sealed class RouteMatrixElement {
    #region INFORMATION
    /// <summary>
    /// Gets the status of the computation for this element.
    /// </summary>
    public Status? Status { get; internal set; }

    /// <summary>
    /// Gets the condition of the route element.
    /// </summary>
    public RouteElementCondition Condition { get; internal set; }
    #endregion

    #region METRICS
    /// <summary>
    /// Gets the distance between origin and destination in meters.
    /// </summary>
    public int? DistanceMeters { get; internal set; }

    /// <summary>
    /// Gets the estimated travel duration.
    /// </summary>
    public string? Duration { get; internal set; }

    /// <summary>
    /// Gets the static (traffic-unaware) duration, if available.
    /// </summary>
    public string? StaticDuration { get; internal set; }
    #endregion

    #region INDEXING
    /// <summary>
    /// Gets the index of the origin in the request.
    /// </summary>
    public int? OriginIndex { get; internal set; }

    /// <summary>
    /// Gets the index of the destination in the request.
    /// </summary>
    public int? DestinationIndex { get; internal set; }
    #endregion

    #region EXTRAS
    /// <summary>
    /// Gets travel advisory information for this route.
    /// </summary>
    public RouteTravelAdvisory? TravelAdvisory { get; internal set; }

    /// <summary>
    /// Gets information related to how and why a fallback result was used.
    /// </summary>
    public FallbackInfo? FallbackInfo { get; internal set; }

    /// <summary>
    /// Gets localized (human-readable) values for this element.
    /// </summary>
    public RouteLocalizedValues? LocalizedValues { get; internal set; }
    #endregion
}