using D3lg4doMaps.Routes.Public.Enums;

namespace D3lg4doMaps.Routes.Internal.Mapping.Converters;

internal static class RoutingPreferenceConverter {
    public static RoutingPreference FromApi(string? preference)
        => preference?.ToUpperInvariant() switch {
            "TRAFFIC_UNAWARE" => RoutingPreference.TrafficUnaware,
            "TRAFFIC_AWARE" => RoutingPreference.TrafficAware,
            "TRAFFIC_AWARE_OPTIMAL" => RoutingPreference.TrafficAwareOptimal,
            _ => RoutingPreference.Unknown
        };
        
    public static string ToApi(RoutingPreference preference)
        => preference switch {
            RoutingPreference.TrafficUnaware => "TRAFFIC_UNAWARE",
            RoutingPreference.TrafficAware => "TRAFFIC_AWARE",
            RoutingPreference.TrafficAwareOptimal => "TRAFFIC_AWARE_OPTIMAL",
            RoutingPreference.Unknown => throw new ArgumentException("Cannot convert Unknown RoutingPreference to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(preference), preference, null)
        };
}