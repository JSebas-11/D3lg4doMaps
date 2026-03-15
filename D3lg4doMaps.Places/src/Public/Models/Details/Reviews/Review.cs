namespace D3lg4doMaps.Places.Public.Models.Details.Reviews;

public sealed class Review {
    public string Text { get; internal set; } = null!;
    public float? Rating { get; internal set; }
    public string? AuthorName { get; internal set; }
    public DateTimeOffset? PublishTime { get; internal set; }
    public string LanguageCode { get; internal set; } = null!;
}