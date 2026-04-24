using System.Text.Json;
using DelgadoMaps.Places.Models.Details;
using DelgadoMaps.Places.Models.Details.Photos;
using DelgadoMaps.Places.Models.Details.Reviews;
using DelgadoMaps.Places.Models.Requests;

namespace DelgadoMaps.Places.Abstractions;

/// <summary>
/// Provides access to detailed information about places, including metadata,
/// photos, and reviews.
/// </summary>
/// <remarks>
/// This service wraps the Google Places Details API and exposes both
/// strongly typed responses and raw JSON access for advanced scenarios.
/// </remarks>
public interface IDetailsService {
    /// <summary>
    /// Retrieves detailed information for a specific place.
    /// </summary>
    /// <param name="placeId">
    /// The unique identifier of the place.
    /// </param>
    /// <returns>
    /// A <see cref="PlaceDetails"/> object containing structured information about the place.
    /// </returns>
    Task<PlaceDetails> GetDetailsAsync(string placeId);

    /// <summary>
    /// Retrieves raw JSON details for a specific place.
    /// </summary>
    /// <param name="placeId">
    /// The unique identifier of the place.
    /// </param>
    /// <param name="fields">
    /// Optional list of fields to include in the response.
    /// </param>
    /// <returns>
    /// A <see cref="JsonDocument"/> containing the raw API response.
    /// </returns>
    /// <remarks>
    /// This method is useful for accessing fields not yet mapped by the SDK
    /// or for debugging API responses.
    ///
    /// ⚠ <b>Important:</b> The returned <see cref="JsonDocument"/> is <see cref="IDisposable"/>.
    /// Consumers are responsible for properly disposing it to avoid memory leaks.
    /// 
    /// <example>
    /// <code>
    /// using var json = await detailsService.GetDetailsRawAsync(placeId);
    /// var root = json.RootElement;
    /// </code>
    /// </example>
    /// </remarks>
    Task<JsonDocument> GetDetailsRawAsync(string placeId, params string[] fields);

    /// <summary>
    /// Retrieves photos associated with a specific place.
    /// </summary>
    /// <param name="placeId">
    /// The unique identifier of the place.
    /// </param>
    /// <param name="photoRequest">
    /// Optional configuration for photo retrieval such as size constraints, etc.
    /// </param>
    /// <returns>
    /// A read-only list of <see cref="PlacePhoto"/> objects.
    /// </returns>
    Task<IReadOnlyList<PlacePhoto>> GetPhotosAsync(string placeId, PhotoRequest? photoRequest = null);

    /// <summary>
    /// Retrieves reviews information for a specific place.
    /// </summary>
    /// <param name="placeId">
    /// The unique identifier of the place.
    /// </param>
    /// <returns>
    /// A <see cref="PlaceReviews"/> object containing user reviews, AI summarize and other information.
    /// </returns>
    Task<PlaceReviews> GetReviewsAsync(string placeId);
}