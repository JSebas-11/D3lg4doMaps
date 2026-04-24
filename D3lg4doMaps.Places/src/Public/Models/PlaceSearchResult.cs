namespace DelgadoMaps.Places.Models;

/// <summary>
/// Represents a place returned from a search query.
/// </summary>
/// <remarks>
/// This model contains basic information about a place such as PlaceId.
/// For more detailed data, use <c>IDetailsService</c>.
/// </remarks>
public sealed class PlaceSearchResult {
    /// <summary>
    /// Gets the unique identifier of the place.
    /// </summary>
    public string? PlaceId { get; internal set; }

    /// <summary>
    /// Gets the name of the place.
    /// </summary>
    public string? DisplayName { get; internal set; }
    
    /// <summary>
    /// Gets the list of place types associated with the place.
    /// </summary>
    public IReadOnlyList<string> Types { get; internal set; } = [];
}