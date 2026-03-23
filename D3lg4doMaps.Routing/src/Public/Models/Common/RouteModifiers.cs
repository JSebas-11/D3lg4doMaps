using D3lg4doMaps.Routing.Public.Models.Utilities;

namespace D3lg4doMaps.Routing.Public.Models.Common;

public sealed class RouteModifiers {
    public bool AvoidTolls { get; private set; } = false;
    public bool AvoidHighways { get; private set; } = false;
    public bool AvoidFerries { get; private set; } = false;
    public bool AvoidIndoor { get; private set; } = false;
    public VehicleInfo? VehicleInfo { get; private set; }

    private RouteModifiers() { }

    public static RouteModifiers Create() => new ();

    public RouteModifiers WithVehicleInfo(VehicleInfo vehicleInfo) {
        VehicleInfo = vehicleInfo;
        return this;
    }
    public RouteModifiers NoTolls() {
        AvoidTolls = true;
        return this;
    }
    public RouteModifiers NoHighways() {
        AvoidHighways = true;
        return this;
    }
    public RouteModifiers NoFerries() {
        AvoidFerries = true;
        return this;
    }
    public RouteModifiers NoIndoor() {
        AvoidIndoor = true;
        return this;
    }
}