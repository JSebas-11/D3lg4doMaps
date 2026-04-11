namespace D3lg4doMaps.Routes.Public.Models.Directions;

/// <summary>
/// Represents the result of a directions request.
/// </summary>
/// <remarks>
/// Contains all computed routes between the origin and destination,
/// along with an optional best route defined by the API.
/// </remarks>
public sealed class RouteResult {
    /// <summary>
    /// Gets the list of computed routes.
    /// </summary>
    public IReadOnlyList<ComputeRoute> Routes { get; internal set; } = [];

    /// <summary>
    /// Gets the best route suggested by the API, if available.
    /// </summary>
    /// <remarks>
    /// This is typically the most optimal route based on factors such as
    /// duration, distance, and traffic conditions.
    /// </remarks>
    public ComputeRoute? BestRoute { get; internal set; }
}