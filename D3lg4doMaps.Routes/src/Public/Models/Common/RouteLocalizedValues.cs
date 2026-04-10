namespace D3lg4doMaps.Routes.Public.Models.Common;

public sealed class RouteLocalizedValues {
    public string Distance { get; internal set; } = null!;
    public string Duration { get; internal set; } = null!;
    public string? StaticDuration { get; internal set; }
    public string? TransitFare { get; internal set; }
}