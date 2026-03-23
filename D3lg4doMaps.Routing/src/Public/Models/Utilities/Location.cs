using D3lg4doMaps.Core.Public.Models.Geometry;

namespace D3lg4doMaps.Routing.Public.Models.Utilities;

public sealed class Location {
    public LatLng LatLng { get; internal set; } = null!;
    public int? Heading { get; internal set; }
}