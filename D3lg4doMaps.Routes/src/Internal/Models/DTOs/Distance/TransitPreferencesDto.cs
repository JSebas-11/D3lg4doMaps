namespace D3lg4doMaps.Routes.Internal.Models.DTOs;

internal sealed class TransitPreferencesDto {
    public IReadOnlyList<string> AllowedTravelModes { get; internal set; } = [];
    public string? RoutingPreference { get; internal set; }
}