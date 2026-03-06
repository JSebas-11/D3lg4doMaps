namespace D3lg4doMaps.Places.Public.Models;

public sealed class PlaceSearchResult {
    public string? PlaceId { get; internal set; }
    public string? DisplayName { get; internal set; }
    public IReadOnlyList<string> Types { get; internal set; } = [];
}