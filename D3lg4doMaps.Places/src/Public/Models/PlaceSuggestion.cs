namespace DelgadoMaps.Places.Models;

/// <summary>
/// Represents a predicted place suggestion returned from an autocomplete query.
/// </summary>
/// <remarks>
/// Suggestions are typically used to guide user input before performing a full search.
/// </remarks>
public sealed class PlaceSuggestion {
    /// <summary>
    /// Gets the unique identifier of the suggested place.
    /// </summary>
    public string PlaceId { get; internal set; } = null!;

    /// <summary>
    /// Gets the full suggestion text.
    /// </summary>
    public string Text { get; internal set; } = null!;

    /// <summary>
    /// Gets the index where the matched input ends within the suggestion text.
    /// </summary>
    /// <remarks>
    /// This can be used for highlighting matched substrings in UI scenarios.
    /// </remarks>
    public int EndOffset { get; internal set; } = -1;
    
    /// <summary>
    /// Gets the list of place types associated with the suggestion.
    /// </summary>
    public IReadOnlyList<string> Types { get; internal set; } = [];
}