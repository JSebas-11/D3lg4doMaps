using D3lg4doMaps.Core.Public.Abstractions;
using D3lg4doMaps.Places.Public.Abstractions;
using D3lg4doMaps.Places.Public.Models;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Internal.Services;

internal class AutocompleteService : IAutocompleteService {
    // -------------------- INIT --------------------
    private readonly IMapsApiClient _apiClient;

    public AutocompleteService(IMapsApiClient apiClient)
        => _apiClient = apiClient;

    // -------------------- METHS --------------------
    public Task<IReadOnlyList<PlaceSuggestion>> SuggestAsync(AutocompleteRequest request) {
        throw new NotImplementedException();
    }
}