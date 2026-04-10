using D3lg4doMaps.Routes.Public.Models.Requests.Common;

namespace D3lg4doMaps.Routes.Public.Models.Requests.Components;

public sealed class RouteMatrixDestination {
    public Waypoint Waypoint { get; } = null!;

    public RouteMatrixDestination(Waypoint waypoint)
        => Waypoint = waypoint;
}