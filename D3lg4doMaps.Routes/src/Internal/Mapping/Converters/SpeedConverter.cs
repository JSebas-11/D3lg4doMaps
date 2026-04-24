using DelgadoMaps.Routes.Enums;

namespace DelgadoMaps.Routes.Internal.Mapping.Converters;

internal static class SpeedConverter {
    public static Speed FromApi(string? speed)
        => speed?.ToUpperInvariant() switch {
            "NORMAL" => Speed.Normal,
            "SLOW" => Speed.Slow,
            "TRAFFIC_JAM" => Speed.TrafficJam,
            _ => Speed.Unknown
        };
        
    public static string ToApi(Speed speed)
        => speed switch {
            Speed.Normal => "NORMAL",
            Speed.Slow => "SLOW",
            Speed.TrafficJam => "TRAFFIC_JAM",
            Speed.Unknown => throw new ArgumentException("Cannot convert Unknown Speed to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(speed), speed, null)
        };
}