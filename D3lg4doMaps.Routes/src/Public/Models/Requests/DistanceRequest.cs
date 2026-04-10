using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Requests.Components;

namespace D3lg4doMaps.Routes.Public.Models.Requests;

public sealed class DistanceRequest {
    public IReadOnlyList<RouteMatrixOrigin> Origins { get; internal set; } = [];
    public IReadOnlyList<RouteMatrixDestination> Destinations { get; internal set; } = [];
    public TravelMode? TravelMode { get; internal set; } 
    public RoutingPreference? RoutingPreference { get; internal set; } 
    public DateTimeOffset? DepartureTime { get; internal set; }
    public DateTimeOffset? ArrivalTime { get; internal set; }
    public Units? Units { get; internal set; }
    public TrafficModel? TrafficModel { get; internal set; }
    public TransitPreferences? TransitPreferences { get; internal set; }
}