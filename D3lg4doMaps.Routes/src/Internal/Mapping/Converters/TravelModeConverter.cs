using D3lg4doMaps.Routes.Public.Enums;

namespace D3lg4doMaps.Routes.Internal.Mapping.Converters;

internal static class TravelModeConverter {
    public static TravelMode FromApi(string? mode)
        => mode?.ToUpperInvariant() switch {
            "DRIVE" => TravelMode.Drive,
            "BICYCLE" => TravelMode.Bicycle,
            "WALK" => TravelMode.Walk,
            "TWO_WHEELER" => TravelMode.TwoWheeler,
            "TRANSIT" => TravelMode.Transit,
            _ => TravelMode.Unknown
        };
        
    public static string ToApi(TravelMode mode)
        => mode switch {
            TravelMode.Drive => "DRIVE",
            TravelMode.Bicycle => "BICYCLE",
            TravelMode.Walk => "WALK",
            TravelMode.TwoWheeler => "TWO_WHEELER",
            TravelMode.Transit => "TRANSIT",
            TravelMode.Unknown => throw new ArgumentException("Cannot convert Unknown TravelMode to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
}