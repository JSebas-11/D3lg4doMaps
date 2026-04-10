using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Common;
using D3lg4doMaps.Routes.Public.Models.DistanceMatrix.Components;

namespace D3lg4doMaps.Routes.Public.Models.DistanceMatrix;

public sealed class RouteMatrixElement {
    // INFORMATION
    public Status? Status { get; internal set; }
    public RouteElementCondition Condition { get; internal set; }
    
    // METRICS
    public int? DistanceMeters { get; internal set; }
    public string? Duration { get; internal set; }
    public string? StaticDuration { get; internal set; }
    
    // INDEXING
    public int? OriginIndex { get; internal set; }
    public int? DestinationIndex { get; internal set; }

    // EXTRAS
    public RouteTravelAdvisory? TravelAdvisory { get; internal set; }
    public FallbackInfo? FallbackInfo { get; internal set; }
    public RouteLocalizedValues? LocalizedValues { get; internal set; }
}