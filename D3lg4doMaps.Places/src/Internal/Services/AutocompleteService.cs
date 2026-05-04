using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Models;
using DelgadoMaps.Places.Internal.Constants;
using DelgadoMaps.Places.Internal.Mappers;
using DelgadoMaps.Places.Internal.Models.Responses;
using DelgadoMaps.Places.Abstractions;
using DelgadoMaps.Places.Models;
using DelgadoMaps.Places.Models.Requests;

namespace DelgadoMaps.Places.Internal.Services;

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
        var response = await _apiClient.SendAsync<AutocompleteResponse>(request).ConfigureAwait(false);

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