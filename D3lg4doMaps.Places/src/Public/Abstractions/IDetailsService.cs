namespace D3lg4doMaps.Places.Public.Abstractions;

public interface IDetailsService {
    Task<PlaceDetails> GetDetailsAsync(string placeId, PlaceDetailsOptions? options = null);
    Task<IReadOnlyList<PlacePhoto>> GetPhotosAsync(string placeId);
    Task<IReadOnlyList<PlaceReview>> GetReviewsAsync(string placeId);
}