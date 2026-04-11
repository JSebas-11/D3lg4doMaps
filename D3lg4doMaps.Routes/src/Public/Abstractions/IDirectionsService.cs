using System.Text.Json;
using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Directions;
using D3lg4doMaps.Routes.Public.Models.Requests;

namespace D3lg4doMaps.Routes.Public.Abstractions;

/// <summary>
/// Provides functionality for calculating routes between locations.
/// </summary>
/// <remarks>
/// This service wraps the Google Routes API (Compute Routes) and returns
/// optimized routes with varying levels of detail.
/// 
/// It supports both strongly-typed responses and raw JSON access
/// for advanced scenarios.
/// </remarks>
public interface IDirectionsService {
    /// <summary>
    /// Calculates routes based on the provided request.
    /// </summary>
    /// <param name="routeRequest">
    /// The request containing origin, destination, intermediates, travel mode,
    /// and optional routing parameters.
    /// </param>
    /// <param name="detailLevel">
    /// The level of detail to include in the response.
    /// </param>
    /// <returns>
    /// A <see cref="RouteResult"/> containing the computed routes
    /// and associated metadata.
    /// </returns>
    /// <remarks>
    /// Use <see cref="RouteDetailLevel.Summary"/> for lightweight responses,
    /// or <see cref="RouteDetailLevel.Full"/> for complete route information.
    /// </remarks>
    Task<RouteResult> GetRoutesAsync(
        RouteRequest routeRequest, RouteDetailLevel detailLevel = RouteDetailLevel.Standard
    );

    /// <summary>
    /// Retrieves raw JSON route data from the API.
    /// </summary>
    /// <param name="routeRequest">
    /// The request containing origin, destination, intermediates and routing parameters.
    /// </param>
    /// <param name="fields">
    /// Optional list of fields to include in the response.
    /// </param>
    /// <returns>
    /// A <see cref="JsonDocument"/> containing the raw API response.
    /// </returns>
    /// <remarks>
    /// This method is useful for:
    /// - Accessing fields not yet mapped by the SDK
    /// - Debugging API responses
    /// - Advanced/custom integrations
    ///
    /// ⚠ The returned <see cref="JsonDocument"/> must be disposed by the caller.
    /// </remarks>
    Task<JsonDocument> GetRoutesRawAsync(RouteRequest routeRequest, params string[] fields);
}