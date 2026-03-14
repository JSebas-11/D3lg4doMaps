namespace D3lg4doMaps.Places.Public.Models.Requests;

public sealed class PhotoRequest {
    public int MaxPhotos { get; internal set; } = 10;
    public int MaxHeightPx { get; internal set; } = 480;
    public int MaxWidthPx { get; internal set; } = 480;
}