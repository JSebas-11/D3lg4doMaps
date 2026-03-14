using System.Text.Json;
using D3lg4doMaps.Places.Public.Models.Details;
using D3lg4doMaps.Places.Public.Models.Details.Photos;
using D3lg4doMaps.Places.Public.Models.Details.Reviews;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Public.Abstractions;

public interface IDetailsService {
    Task<PlaceDetails> GetDetailsAsync(string placeId);
    Task<JsonDocument> GetDetailsRawAsync(string placeId, params string[] fields);
    Task<IReadOnlyList<PlacePhotos>> GetPhotosAsync(string placeId, PhotoRequest? photoRequest = null);
    Task<PlaceReviews> GetReviewsAsync(string placeId);
}