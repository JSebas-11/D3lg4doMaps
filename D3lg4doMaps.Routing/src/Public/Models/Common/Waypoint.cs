using D3lg4doMaps.Routing.Public.Models.Utilities;

namespace D3lg4doMaps.Routing.Public.Models.Common;

public sealed class Waypoint {
    public bool Via { get; internal set; }
    public bool VehicleStopover { get; internal set; }
    public bool SideOfRoad { get; internal set; }

    public string? PlaceId { get; }
    public Location? Location { get; }

    internal Waypoint(string? placeId, Location? location) {
        PlaceId = placeId;
        Location = location;
    }
}