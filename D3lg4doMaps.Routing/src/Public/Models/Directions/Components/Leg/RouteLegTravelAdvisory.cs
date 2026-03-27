using D3lg4doMaps.Routing.Public.Models.Components;

namespace D3lg4doMaps.Routing.Public.Models.Directions.Components;

public sealed class RouteLegTravelAdvisory {
    public IReadOnlyList<SpeedInterval> SpeedReadingIntervals { get; internal set; } = [];
}