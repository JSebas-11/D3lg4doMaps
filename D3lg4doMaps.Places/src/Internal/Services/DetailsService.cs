using System.Text.Json;
using D3lg4doMaps.Core.Public.Abstractions;
using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Core.Public.Extensions;
using D3lg4doMaps.Core.Public.Models;
using D3lg4doMaps.Places.Internal.Constants;
using D3lg4doMaps.Places.Internal.Mappers;
using D3lg4doMaps.Places.Public.Abstractions;
using D3lg4doMaps.Places.Public.Models.Details;
using D3lg4doMaps.Places.Public.Models.Details.Photos;
using D3lg4doMaps.Places.Public.Models.Details.Reviews;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Internal.Services;

internal class DetailsService : IDetailsService {
    // -------------------- INIT --------------------
    private static readonly string[] _detailsFields = [ 
        "displayName", "types", // General Info
        "formattedAddress", "plusCode", "location", "postalAddress", "timeZone", // Location-like Info
        "nationalPhoneNumber", "internationalPhoneNumber", // Communication Info
        "priceLevel", "rating", "userRatingCount", // Calification Info
        "googleMapsUri", "websiteUri", "googleMapsLinks", // Links Info
        "paymentOptions", "parkingOptions", // Options Info
        "regularOpeningHours" // Bussines Info
    ];
    private static readonly string[] _reviewsFields = [ 
        "displayName", "reviewSummary", "reviews", "googleMapsLinks"
    ];

    private readonly IMapsApiClient _apiClient;
    public DetailsService(IMapsApiClient apiClient) => _apiClient = apiClient;

    // -------------------- METHS --------------------
    public async Task<PlaceDetails> GetDetailsAsync(string placeId) {
        using var jsonDocument = await GetDetailsRawAsync(placeId, _detailsFields);
        return DetailsMapper.ToPlaceDetails(placeId, jsonDocument);
    }

    public async Task<JsonDocument> GetDetailsRawAsync(string placeId, params string[] fields) {
        if (string.IsNullOrWhiteSpace(placeId))
            throw new MapsInvalidRequestException("Place Id was not provided.");
        
        if (fields.Length == 0)
            throw new MapsInvalidRequestException("Fields must not be empty (FieldMask is a required parameter).");

        var headers = new Dictionary<string, string>() {
            { "X-Goog-FieldMask", string.Join(',', fields) }
        };
        var request = CreateRequest($"{PlacesEndpoints.Details}/{placeId}", headers: headers);

        return await _apiClient.SendAsync<JsonDocument>(request);
    }

    public async Task<IReadOnlyList<PlacePhoto>> GetPhotosAsync(string placeId, PhotoRequest? photoRequest = null) {
        using var jsonDocument = await GetDetailsRawAsync(placeId, "photos"); // Get Photo info from Details

        photoRequest ??= new PhotoRequest();
        
        var rawPhotos = jsonDocument.RootElement
            .GetArray("photos")
            .Take(photoRequest.MaxPhotos)
            .ToList();

        if (rawPhotos.Count == 0) return [];
        
        // COMMON QUERY PARAMS
        var query = new Dictionary<string, string>() {
            { "skipHttpRedirect", "true" },
            { "maxHeightPx", photoRequest.MaxHeightPx.ToString() },
            { "maxWidthPx", photoRequest.MaxWidthPx.ToString() }
        };

        // PARALLEL EXECUTION
        var tasks = new List<Task<PlacePhoto>>();
        foreach (var photoDet in rawPhotos)
            tasks.Add(GetPhotoAndMapAsync(photoDet, query));

        return [.. await Task.WhenAll(tasks)];
    }

    public async Task<PlaceReviews> GetReviewsAsync(string placeId) {
        using var jsonDocument = await GetDetailsRawAsync(placeId, _reviewsFields);
        return DetailsMapper.ToPlaceReviews(placeId, jsonDocument);
    }

    // -------------------- INNER METHS --------------------
    private static MapsApiRequest CreateRequest(
        string endpoint, IDictionary<string, string>? headers = null, IDictionary<string, string>? query = null
    )
        => new () {
            Method = HttpMethod.Get,
            BaseUrl = PlacesEndpoints.BaseUrl,
            Endpoint = endpoint,
            Headers = headers,
            Query = query
        };

    private async Task<PlacePhoto> GetPhotoAndMapAsync(JsonElement photoDets, IDictionary<string, string> reqQuery) {
        var json = await _apiClient.SendAsync<JsonDocument>(
            CreateRequest($"{photoDets.GetStringValue("name")!}/media", reqQuery)
        );
        
        return DetailsMapper.ToPlacePhoto(json, photoDets);
    }
}