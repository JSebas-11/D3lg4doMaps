using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Components;

namespace D3lg4doMaps.Routes.Public.Models.Directions.Components;

public sealed class Segment {
    public NavigationInstruction? NavigationInstruction { get; internal set; }
    public TravelMode? TravelMode { get; internal set; }
    public int StartIndex { get; internal set; }
    public int EndIndex { get; internal set; }
}