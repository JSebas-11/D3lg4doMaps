using D3lg4doMaps.Routes.Public.Enums;

namespace D3lg4doMaps.Routes.Public.Models.DistanceMatrix.Components;

public sealed class FallbackInfo {
    public RoutingMode RoutingMode { get; internal set; }
    public Reason Reason { get; internal set; }
}