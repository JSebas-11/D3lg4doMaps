using D3lg4doMaps.Places.Public.Models;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Public.Abstractions;

public interface IAutocompleteService {
    Task<IReadOnlyList<PlaceSuggestion>> SuggestAsync(AutocompleteRequest request);
}