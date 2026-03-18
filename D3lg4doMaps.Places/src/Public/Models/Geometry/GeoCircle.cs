using D3lg4doMaps.Core.Public.Models.Geometry;

namespace D3lg4doMaps.Places.Public.Models.Geometry;

public sealed class GeoCircle {
    public LatLng Center { get; internal set; } = null!;
    public double Radius { get; internal set; }
}