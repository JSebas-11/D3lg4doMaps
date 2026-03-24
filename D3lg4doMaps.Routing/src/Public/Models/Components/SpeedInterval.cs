using D3lg4doMaps.Routing.Public.Enums;

namespace D3lg4doMaps.Routing.Public.Models.Components;

public sealed class SpeedInterval {
    public int StartPolylinePointIndex { get; internal set; }
    public int EndPolylinePointIndex { get; internal set; }
    public Speed Speed { get; internal set; }
}