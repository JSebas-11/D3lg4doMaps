using D3lg4doMaps.Places.Public.Models.Geometry;

namespace D3lg4doMaps.Places.Public.Models.Requests;

/// <summary>
/// Represents a request for retrieving place autocomplete suggestions.
/// </summary>
/// <remarks>
/// This model is constructed using <see cref="Builders.AutocompleteRequestBuilder"/>
/// to ensure all required fields are properly set.
/// </remarks>
public sealed class AutocompleteRequest {
    /// <summary>
    /// Gets the input text used to generate autocomplete suggestions.
    /// </summary>
    /// <remarks>
    /// This value represents partial user input (e.g., "coffee", "pizza near").
    /// </remarks>
    public string Input { get; internal set; } = null!;
    
    /// <summary>
    /// Gets the optional geographic bias applied to the autocomplete results.
    /// </summary>
    /// <remarks>
    /// When provided, results are biased toward the specified area but not strictly restricted.
    /// </remarks>
    public LocationBias? LocationBias { get; internal set; }
}