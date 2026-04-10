using D3lg4doMaps.Routes.Public.Models.Requests.Common;

namespace D3lg4doMaps.Routes.Public.Models.Requests.Components;

public sealed class RouteMatrixOrigin {
    public Waypoint Waypoint { get; } = null!;
    public RouteModifiers? RouteModifiers { get; }

    public RouteMatrixOrigin(
        Waypoint waypoint, 
        RouteModifiers? routeModifiers = null
    ) {
        Waypoint = waypoint;
        RouteModifiers = routeModifiers;
    }

    internal void ValidateForRequest() 
        => RouteModifiers?.ValidateForRequest();
}