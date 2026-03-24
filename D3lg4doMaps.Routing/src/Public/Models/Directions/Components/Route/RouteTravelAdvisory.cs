using D3lg4doMaps.Routing.Public.Models.Components;

namespace D3lg4doMaps.Routing.Public.Models.Directions.Components;

public sealed class RouteTravelAdvisory {
    public IReadOnlyList<SpeedInterval> SpeedReadingIntervals { get; internal set; } = [];
    public bool? RouteRestrictionsPartiallyIgnored { get; internal set; }
    public Money? TransitFare { get; internal set; }
}