using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.DistanceMatrix;
using D3lg4doMaps.Routes.Public.Models.Requests;

namespace D3lg4doMaps.Routes.Public.Abstractions;

/// <summary>
/// Provides functionality for computing distance and travel time
/// between multiple origins and destinations.
/// </summary>
/// <remarks>
/// This service wraps the Google Routes API (Compute Route Matrix) and returns
/// pairwise route information between sets of locations.
/// </remarks>
public interface IDistanceMatrixService {
    /// <summary>
    /// Calculates distances, durations and other details between origins and destinations.
    /// </summary>
    /// <param name="distanceRequest">
    /// The request containing origin and destination locations,
    /// along with optional routing parameters.
    /// </param>
    /// <param name="detailLevel">
    /// The level of detail to include in the response.
    /// </param>
    /// <returns>
    /// A read-only list of <see cref="RouteMatrixElement"/> representing
    /// distances and travel durations between location pairs.
    /// </returns>
    /// <remarks>
    /// Useful for:
    /// - Logistics and delivery systems
    /// - Travel time estimation
    /// - Route comparison scenarios
    /// </remarks>
    Task<IReadOnlyList<RouteMatrixElement>> GetDistancesAsync(
        DistanceRequest distanceRequest, RouteDetailLevel detailLevel = RouteDetailLevel.Standard
    );
}