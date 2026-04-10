using D3lg4doMaps.Routes.Public.Models.Requests.Components;

namespace D3lg4doMaps.Routes.Internal.Models.DTOs;

internal sealed class DistanceRequestDto {
    public IReadOnlyList<RouteMatrixOriginDto> Origins { get; internal set; } = [];
    public IReadOnlyList<RouteMatrixDestination> Destinations { get; internal set; } = [];
    public string? TravelMode { get; internal set; } 
    public string? RoutingPreference { get; internal set; } 
    public DateTimeOffset? DepartureTime { get; internal set; }
    public DateTimeOffset? ArrivalTime { get; internal set; }
    public string? Units { get; internal set; }
    public string? TrafficModel { get; internal set; }
    public TransitPreferencesDto? TransitPreferences { get; internal set; }
}