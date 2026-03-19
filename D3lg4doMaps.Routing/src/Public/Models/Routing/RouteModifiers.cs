using D3lg4doMaps.Routing.Public.Models.Travel;

namespace D3lg4doMaps.Routing.Public.Models.Routing;

public sealed class RouteModifiers {
    public bool AvoidTolls { get; internal set; }
    public bool AvoidHighways { get; internal set; }
    public bool AvoidFerries { get; internal set; }
    public bool AvoidIndoor { get; internal set; }
    public VehicleInfo? VehicleInfo { get; internal set; }
}