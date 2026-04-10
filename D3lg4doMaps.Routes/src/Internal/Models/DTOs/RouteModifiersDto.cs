namespace D3lg4doMaps.Routes.Internal.Models.DTOs;

internal sealed class RouteModifiersDto {
    public bool AvoidTolls { get; internal set; } = false;
    public bool AvoidHighways { get; internal set; } = false;
    public bool AvoidFerries { get; internal set; } = false;
    public bool AvoidIndoor { get; internal set; } = false;
    public VehicleInfoDto? VehicleInfo { get; internal set; }
}