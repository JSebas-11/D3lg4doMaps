using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Components;
using D3lg4doMaps.Routes.Public.Models.Directions.Components;

namespace D3lg4doMaps.Routes.Public.Models.Directions;

public sealed class RouteStep {
    // METRICS
    public int DistanceMeters { get; internal set; }
    public string? StaticDuration { get; internal set; }
    
    // LOCATION
    public Location? StartLocation { get; internal set; }
    public Location? EndLocation { get; internal set; }

    // GEOMETRY
    public Polyline? Polyline { get; internal set; }
    
    // NAVIGATION
    public NavigationInstruction? NavigationInstruction { get; internal set; }

    // EXTRAS    
    public RouteLegTravelAdvisory? TravelAdvisory { get; internal set; }
    public RouteLegStepLocalizedValues? LocalizedValues { get; internal set; }
    public TravelMode? TravelMode { get; internal set; }
}