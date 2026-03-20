namespace D3lg4doMaps.Places.Public.Models.Details.Reviews;

/// <summary>
/// Represents a summarized version of place reviews done by Gemini AI.
/// </summary>
/// <remarks>
/// Typically contains a short description of overall sentiment.
/// </remarks>
public sealed class ReviewSummary {
    /// <summary>
    /// Gets the summary text.
    /// </summary>
    public string Text { get; internal set; } = null!;

    /// <summary>
    /// Gets the language code of the summary.
    /// </summary>
    public string LanguageCode { get; internal set; } = null!;
}