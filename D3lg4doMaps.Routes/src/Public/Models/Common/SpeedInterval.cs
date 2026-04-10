using D3lg4doMaps.Routes.Public.Enums;

namespace D3lg4doMaps.Routes.Public.Models.Common;

public sealed class SpeedInterval {
    public int StartPolylinePointIndex { get; internal set; }
    public int EndPolylinePointIndex { get; internal set; }
    public Speed Speed { get; internal set; }
}