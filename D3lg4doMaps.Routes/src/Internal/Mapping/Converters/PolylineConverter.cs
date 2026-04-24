using DelgadoMaps.Routes.Enums;

namespace DelgadoMaps.Routes.Internal.Mapping.Converters;

internal static class PolylineConverter {
    public static PolylineQuality QualityFromApi(string? quality)
        => quality?.ToUpperInvariant() switch {
            "HIGH_QUALITY" => PolylineQuality.HighQuality,
            "OVERVIEW" => PolylineQuality.Overview,
            _ => PolylineQuality.Unknown
        };
    public static string QualityToApi(PolylineQuality quality)
        => quality switch {
            PolylineQuality.HighQuality => "HIGH_QUALITY",
            PolylineQuality.Overview => "OVERVIEW",
            PolylineQuality.Unknown => throw new ArgumentException("Cannot convert Unknown PolylineQuality to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(quality), quality, null)
        };

    public static PolylineEncoding EncodingFromApi(string? encoding)
        => encoding?.ToUpperInvariant() switch {
            "ENCODED_POLYLINE" => PolylineEncoding.EncodedPolyline,
            //"GEO_JSON_LINESTRING" => PolylineEncoding.GeoJsonLinestring,
            _ => PolylineEncoding.Unknown
        };
    public static string EncodingToApi(PolylineEncoding encoding)
        => encoding switch {
            PolylineEncoding.EncodedPolyline => "ENCODED_POLYLINE",
            //PolylineEncoding.GeoJsonLinestring => "GEO_JSON_LINESTRING",
            PolylineEncoding.Unknown => throw new ArgumentException("Cannot convert Unknown PolylineEncoding to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(encoding), encoding, null)
        };
}