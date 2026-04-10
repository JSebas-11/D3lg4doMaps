using D3lg4doMaps.Routes.Public.Enums;

namespace D3lg4doMaps.Routes.Internal.Mapping.Converters;

internal static class VehicleConverter {
    public static VehicleEmissionType EmissionTypeFromApi(string? emissionType)
        => emissionType?.ToUpperInvariant() switch {
            "GASOLINE" => VehicleEmissionType.Gasoline,
            "ELECTRIC" => VehicleEmissionType.Electric,
            "HYBRID" => VehicleEmissionType.Hybrid,
            "DIESEL" => VehicleEmissionType.Diesel,
            _ => VehicleEmissionType.Unknown
        };
    public static string EmissionTypeToApi(VehicleEmissionType emissionType)
        => emissionType switch {
            VehicleEmissionType.Gasoline => "GASOLINE",
            VehicleEmissionType.Electric => "ELECTRIC",
            VehicleEmissionType.Hybrid => "HYBRID",
            VehicleEmissionType.Diesel => "DIESEL",
            VehicleEmissionType.Unknown => throw new ArgumentException("Cannot convert Unknown VehicleEmissionType to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(emissionType), emissionType, null)
        };
}