namespace DelgadoMaps.Places.Models.Details.Reviews;

/// <summary>
/// Represents a single user review for a place.
/// </summary>
public sealed class Review {
    /// <summary>
    /// Gets the review text.
    /// </summary>
    public string Text { get; internal set; } = null!;

    /// <summary>
    /// Gets the rating provided by the user.
    /// </summary>
    public float? Rating { get; internal set; }

    /// <summary>
    /// Gets the name of the review author.
    /// </summary>
    public string? AuthorName { get; internal set; }

    /// <summary>
    /// Gets the publication time of the review.
    /// </summary>
    public DateTimeOffset? PublishTime { get; internal set; }

    /// <summary>
    /// Gets the language code of the review.
    /// </summary>
    public string LanguageCode { get; internal set; } = null!;
}