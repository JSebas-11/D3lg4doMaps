using D3lg4doMaps.Routing.Public.Models.Components;
using D3lg4doMaps.Routing.Public.Models.Directions.Components;

namespace D3lg4doMaps.Routing.Public.Models.Directions;

public sealed class RouteLeg {
    // METRICS
    public int DistanceMeters { get; internal set; }
    public string Duration { get; internal set; } = null!;
    
    // LOCATION
    public Location? StartLocation { get; internal set; }
    public Location? EndLocation { get; internal set; }

    // GEOMETRY
    public Polyline? Polyline { get; internal set; }
    
    // STEPS
    public IReadOnlyList<RouteStep> Steps { get; internal set; } = [];

    // EXTRAS    
    public RouteLegTravelAdvisory? TravelAdvisory { get; internal set; }
    public RouteLegLocalizedValues? LocalizedValues { get; internal set; }
    public StepsOverview? Overview { get; internal set; }
}