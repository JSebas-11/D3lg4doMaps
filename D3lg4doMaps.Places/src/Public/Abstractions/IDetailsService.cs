using D3lg4doMaps.Places.Public.Models.Details;
using D3lg4doMaps.Places.Public.Models.Details.Reviews;

namespace D3lg4doMaps.Places.Public.Abstractions;

public interface IDetailsService {
    Task<PlaceDetails> GetDetailsAsync(string placeId, PlaceDetailsOptions options);
    Task<IReadOnlyList<PlacePhotos>> GetPhotosAsync(string placeId);
    Task<PlaceReviews> GetReviewsAsync(string placeId);
}