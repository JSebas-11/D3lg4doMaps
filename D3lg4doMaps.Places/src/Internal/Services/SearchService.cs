using D3lg4doMaps.Core.Public.Abstractions;
using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Core.Public.Models;
using D3lg4doMaps.Places.Internal.Constants;
using D3lg4doMaps.Places.Internal.Mappers;
using D3lg4doMaps.Places.Internal.Models.Responses;
using D3lg4doMaps.Places.Public.Abstractions;
using D3lg4doMaps.Places.Public.Models;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Internal.Services;

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

        return [.. response.Places.Select(PlaceMapper.ToSearchResult)];
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