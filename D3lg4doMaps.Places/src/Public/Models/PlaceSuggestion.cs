namespace D3lg4doMaps.Places.Public.Models;

public sealed class PlaceSuggestion {
    public string PlaceId { get; internal set; } = null!;
    public string Text { get; internal set; } = null!;
    public int EndOffset { get; internal set; } = -1;
    public IReadOnlyList<string> Types { get; internal set; } = [];
}