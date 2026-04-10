using D3lg4doMaps.Routes.Public.Enums;

namespace D3lg4doMaps.Routes.Internal.Mapping.Converters;

internal static class TrafficModelConverter {
    public static TrafficModel FromApi(string? model)
        => model?.ToUpperInvariant() switch {
            "BEST_GUESS" => TrafficModel.BestGuess,
            "PESSIMISTIC" => TrafficModel.Pessimistic,
            "OPTIMISTIC" => TrafficModel.Optimistic,
            _ => TrafficModel.Unknown
        };
        
    public static string ToApi(TrafficModel model)
        => model switch {
            TrafficModel.BestGuess => "BEST_GUESS",
            TrafficModel.Pessimistic => "PESSIMISTIC",
            TrafficModel.Optimistic => "OPTIMISTIC",
            TrafficModel.Unknown => throw new ArgumentException("Cannot convert Unknown TrafficModel to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
        };
}