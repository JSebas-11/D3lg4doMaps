namespace D3lg4doMaps.Routes.Public.Models.Common;

public sealed class RouteTravelAdvisory {
    public IReadOnlyList<SpeedInterval> SpeedReadingIntervals { get; internal set; } = [];
    public bool? RouteRestrictionsPartiallyIgnored { get; internal set; }
    public Money? TransitFare { get; internal set; }
}