using D3lg4doMaps.Core.Public.Abstractions;
using D3lg4doMaps.Core.Public.Models;
using D3lg4doMaps.Places.Internal.Constants;
using D3lg4doMaps.Places.Internal.Mappers;
using D3lg4doMaps.Places.Internal.Models.Responses;
using D3lg4doMaps.Places.Public.Abstractions;
using D3lg4doMaps.Places.Public.Models;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Internal.Services;

internal class AutocompleteService : IAutocompleteService {
    // -------------------- INIT --------------------
    private static readonly Dictionary<string, string> _defHeaders = new() {
        {"Content-Type", "application/json"},
        {"X-Goog-FieldMask", "suggestions.placePrediction.placeId,suggestions.placePrediction.text,suggestions.placePrediction.types"}
    };
    
    private readonly IMapsApiClient _apiClient;

    public AutocompleteService(IMapsApiClient apiClient)
        => _apiClient = apiClient;

    // -------------------- METHS --------------------
    public async Task<IReadOnlyList<PlaceSuggestion>> SuggestPlacesAsync(AutocompleteRequest autocompleteRequest) {
        var request = CreateRequest(autocompleteRequest);
        var response = await _apiClient.SendAsync<AutocompleteResponse>(request);

        return [.. response.Suggestions
            .Where(s => s.PlacePrediction is not null)
            .Select(s => AutocompleteMapper.ToPlaceSuggestion(s.PlacePrediction!))];
    }

    // -------------------- INNER METHS --------------------
    private static MapsApiRequest CreateRequest(AutocompleteRequest payload)
        => new () {
            Method = HttpMethod.Post,
            BaseUrl = PlacesEndpoints.BaseUrl,
            Endpoint = PlacesEndpoints.Autocomplete,
            Headers = _defHeaders,
            Payload = payload
        };
}