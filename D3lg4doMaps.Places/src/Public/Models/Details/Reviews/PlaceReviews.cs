namespace D3lg4doMaps.Places.Public.Models.Details.Reviews;

public sealed class PlaceReviews {
    public string PlaceId { get; internal set; } = null!;
    public string DisplayName { get; internal set; } = null!;
    public IReadOnlyList<Review> Reviews { get; internal set; } = [];
    public ReviewSummary? ReviewSummary { get; internal set; }
    public string? ReviewsUri { get; internal set; }
}