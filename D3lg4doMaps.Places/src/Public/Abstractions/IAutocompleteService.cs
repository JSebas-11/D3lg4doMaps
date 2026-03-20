using D3lg4doMaps.Places.Public.Models;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Public.Abstractions;

/// <summary>
/// Provides autocomplete functionality for retrieving place suggestions
/// based on partial user input.
/// </summary>
/// <remarks>
/// This service wraps the Google Places Autocomplete API and returns
/// strongly typed suggestions that can be used to guide user input
/// in search scenarios.
/// </remarks>
public interface IAutocompleteService {
    /// <summary>
    /// Retrieves a list of place suggestions based on the specified autocomplete request.
    /// </summary>
    /// <param cref="AutocompleteRequest">
    /// The request containing the input text and optional parameters.
    /// </param>
    /// <returns>
    /// A read-only list of <see cref="PlaceSuggestion"/> representing predicted places.
    /// </returns>
    Task<IReadOnlyList<PlaceSuggestion>> SuggestPlacesAsync(AutocompleteRequest autocompleteRequest);
}