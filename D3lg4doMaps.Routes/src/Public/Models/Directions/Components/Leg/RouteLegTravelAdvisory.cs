using D3lg4doMaps.Routes.Public.Models.Common;

namespace D3lg4doMaps.Routes.Public.Models.Directions.Components;

public sealed class RouteLegTravelAdvisory {
    public IReadOnlyList<SpeedInterval> SpeedReadingIntervals { get; internal set; } = [];
}