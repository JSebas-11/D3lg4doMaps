using DelgadoMaps.Routes.Enums;

namespace DelgadoMaps.Routes.Internal.Mapping.Converters;

internal static class TransitRoutingPreferenceConverter {
    public static TransitRoutingPreference FromApi(string? preference)
        => preference?.ToUpperInvariant() switch {
            "LESS_WALKING" => TransitRoutingPreference.LessWalking,
            "FEWER_TRANSFERS" => TransitRoutingPreference.FewerTransfers,
            _ => TransitRoutingPreference.Unknown
        };
        
    public static string ToApi(TransitRoutingPreference preference)
        => preference switch {
            TransitRoutingPreference.LessWalking => "LESS_WALKING",
            TransitRoutingPreference.FewerTransfers => "FEWER_TRANSFERS",
            TransitRoutingPreference.Unknown => throw new ArgumentException("Cannot convert Unknown TransitRoutingPreference to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(preference), preference, null)
        };
}

internal static class TransitTravelModeConverter {
    public static TransitTravelMode FromApi(string? mode)
        => mode?.ToUpperInvariant() switch {
            "BUS" => TransitTravelMode.Bus,
            "SUBWAY" => TransitTravelMode.Subway,
            "TRAIN" => TransitTravelMode.Train,
            "LIGHT_RAIL" => TransitTravelMode.LightRail,
            "RAIL" => TransitTravelMode.Rail,
            _ => TransitTravelMode.Unknown
        };
        
    public static string ToApi(TransitTravelMode mode)
        => mode switch {
            TransitTravelMode.Bus => "BUS",
            TransitTravelMode.Subway => "SUBWAY",
            TransitTravelMode.Train => "TRAIN",
            TransitTravelMode.LightRail => "LIGHT_RAIL",
            TransitTravelMode.Rail => "RAIL",
            TransitTravelMode.Unknown => throw new ArgumentException("Cannot convert Unknown TransitTravelMode to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
}