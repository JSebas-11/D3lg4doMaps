using D3lg4doMaps.Places.Public.Models;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Public.Abstractions;

/// <summary>
/// Provides functionality for searching places using text queries
/// or geographic constraints.
/// </summary>
/// <remarks>
/// This service wraps the Google Places Search APIs and returns
/// strongly typed results for common search scenarios.
/// </remarks>
public interface ISearchService {
    /// <summary>
    /// Searches for places using a text-based query.
    /// </summary>
    /// <param name="textQuery">
    /// The text query used to search for places (e.g., "restaurants near London").
    /// </param>
    /// <returns>
    /// A read-only list of <see cref="PlaceSearchResult"/> representing matching places.
    /// </returns>
    Task<IReadOnlyList<PlaceSearchResult>> SearchByTextAsync(string textQuery);

    /// <summary>
    /// Searches for places near a specific geographic location.
    /// </summary>
    /// <param name="nearbyRequest">
    /// The request containing location, radius, and optional filters.
    /// </param>
    /// <returns>
    /// A read-only list of <see cref="PlaceSearchResult"/> representing nearby places.
    /// </returns>
    Task<IReadOnlyList<PlaceSearchResult>> SearchByNearbyAsync(NearbyRequest nearbyRequest);
}