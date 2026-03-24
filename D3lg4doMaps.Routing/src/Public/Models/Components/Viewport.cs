using D3lg4doMaps.Core.Public.Models.Geometry;

namespace D3lg4doMaps.Routing.Public.Models.Components;

public sealed class Viewport {
    public LatLng Low { get; internal set; } = null!;
    public LatLng High { get; internal set; } = null!;
}