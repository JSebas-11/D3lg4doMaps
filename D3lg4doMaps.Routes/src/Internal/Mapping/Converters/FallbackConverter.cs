using DelgadoMaps.Routes.Enums;

namespace DelgadoMaps.Routes.Internal.Mapping.Converters;

internal static class RoutingModeConverter {
    public static RoutingMode FromApi(string? mode)
        => mode?.ToUpperInvariant() switch {
            "FALLBACK_TRAFFIC_AWARE" => RoutingMode.FallbackTrafficAware,
            "FALLBACK_TRAFFIC_UNAWARE" => RoutingMode.FallbackTrafficUnaware,
            _ => RoutingMode.Unknown
        };
        
    public static string ToApi(RoutingMode mode)
        => mode switch {
            RoutingMode.FallbackTrafficAware => "FALLBACK_TRAFFIC_AWARE",
            RoutingMode.FallbackTrafficUnaware => "FALLBACK_TRAFFIC_UNAWARE",
            RoutingMode.Unknown => throw new ArgumentException("Cannot convert Unknown RoutingMode to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
}

internal static class ReasonConverter {
    public static Reason FromApi(string? reason)
        => reason?.ToUpperInvariant() switch {
            "SERVER_ERROR" => Reason.ServerError,
            "LATENCY_EXCEEDED" => Reason.LatencyExceeded,
            _ => Reason.Unknown
        };
        
    public static string ToApi(Reason reason)
        => reason switch {
            Reason.ServerError => "SERVER_ERROR",
            Reason.LatencyExceeded => "LATENCY_EXCEEDED",
            Reason.Unknown => throw new ArgumentException("Cannot convert Unknown Reason to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(reason), reason, null)
        };
}