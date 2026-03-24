using D3lg4doMaps.Routing.Public.Models.Components;
using D3lg4doMaps.Routing.Public.Models.Directions.Components;

namespace D3lg4doMaps.Routing.Public.Models.Directions;

public sealed class ComputeRoute {
    // METRICS
    public int DistanceMeters { get; internal set; }
    public string Duration { get; internal set; } = null!;
    public string? StaticDuration { get; internal set; }

    // DESCRIPTION
    public string? Description { get; internal set; }
    public IReadOnlyList<string> RouteLabels { get; internal set; } = [];

    // GEOMETRY
    public Viewport? Viewport { get; internal set; }
    public Polyline? Polyline { get; internal set; }

    // NAVIGATION
    public IReadOnlyList<RouteLeg> Legs { get; internal set; } = [];

    // EXTRAS    
    public string? RouteToken { get; internal set; }
    public IReadOnlyList<string> Warnings { get; internal set; } = [];
    public RouteTravelAdvisory? TravelAdvisory { get; internal set; }
    public RouteLocalizedValues? LocalizedValues { get; internal set; }
    public IReadOnlyList<int> OptimizedWaypointOrder { get; internal set; } = [];
}