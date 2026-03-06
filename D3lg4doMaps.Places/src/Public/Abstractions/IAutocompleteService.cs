using D3lg4doMaps.Places.Public.Models;

namespace D3lg4doMaps.Places.Public.Abstractions;

public interface IAutocompleteService {
    Task<IReadOnlyList<PlaceSuggestion>> SuggestAsync(string input);
}