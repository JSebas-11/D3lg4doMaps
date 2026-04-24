using DelgadoMaps.Routes.Enums;

namespace DelgadoMaps.Routes.Internal.Mapping.Converters;

internal static class UnitsConverter {
    public static Units FromApi(string? units)
        => units?.ToUpperInvariant() switch {
            "METRIC" => Units.Metric,
            "IMPERIAL" => Units.Imperial,
            _ => Units.Unknown
        };
    public static string ToApi(Units units)
        => units switch {
            Units.Metric => "METRIC",
            Units.Imperial => "IMPERIAL",
            Units.Unknown => throw new ArgumentException("Cannot convert Unknown Units to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(units), units, null)
        };
}