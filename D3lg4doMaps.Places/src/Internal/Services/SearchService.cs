using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Core.Models;
using DelgadoMaps.Places.Internal.Constants;
using DelgadoMaps.Places.Internal.Mappers;
using DelgadoMaps.Places.Internal.Models.Responses;
using DelgadoMaps.Places.Abstractions;
using DelgadoMaps.Places.Models;
using DelgadoMaps.Places.Models.Requests;

namespace DelgadoMaps.Places.Internal.Services;

internal class SearchService : ISearchService {
    // -------------------- INIT --------------------
    private static readonly Dictionary<string, string> _defHeaders = new() {
        {"Content-Type", "application/json"},
        {"X-Goog-FieldMask", "places.id,places.displayName,places.types"}
    };
    
    private readonly IMapsApiClient _apiClient;
    public SearchService(IMapsApiClient apiClient) => _apiClient = apiClient;

    // -------------------- METHS --------------------
    public async Task<IReadOnlyList<PlaceSearchResult>> SearchByTextAsync(string textQuery) {
        if (string.IsNullOrWhiteSpace(textQuery))
            throw new MapsInvalidRequestException("TextQuery must be neither empty nor null.");
        
        var request = CreateRequest(PlacesEndpoints.SearchText, new { textQuery });
        return await SendAndMapAsync(request);
    }

    public async Task<IReadOnlyList<PlaceSearchResult>> SearchByNearbyAsync(NearbyRequest nearbyRequest) {
        var request = CreateRequest(PlacesEndpoints.SearchNearby, nearbyRequest);
        return await SendAndMapAsync(request);
    }

    // -------------------- INNER METHS --------------------
    private async Task<IReadOnlyList<PlaceSearchResult>> SendAndMapAsync(MapsApiRequest request) {
        var response = await _apiClient.SendAsync<PlacesSearchResponse>(request);

        return [.. 
            response.Places
            .Where(p => p is not null)
            .Select(PlaceMapper.ToSearchResult)
        ];
    }

    private static MapsApiRequest CreateRequest(string endpoint, object? payload = null)
        => new () {
            Method = HttpMethod.Post,
            BaseUrl = PlacesEndpoints.BaseUrl,
            Endpoint = endpoint,
            Headers = _defHeaders,
            Payload = payload
        };
}