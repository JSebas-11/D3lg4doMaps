namespace D3lg4doMaps.Places.Public.Models.Details.Photos;

public sealed class PlacePhoto {
    public Uri Uri { get; internal set; } = null!;
    public string Name { get; internal set; } = null!;
    public string? AuthorName { get; internal set; }
    public int? HeightPx { get; internal set; }
    public int? WidthPx { get; internal set; }
}