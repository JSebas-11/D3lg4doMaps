using DelgadoMaps.Routes.Models.Requests.Common;

namespace DelgadoMaps.Routes.Models.Requests.Components;

/// <summary>
/// Represents a destination element in a distance matrix request.
/// </summary>
/// <remarks>
/// Wraps a <see cref="Waypoint"/> used as a destination in matrix calculations.
/// </remarks>
public sealed class RouteMatrixDestination {
    /// <summary>
    /// Gets the waypoint representing the destination location.
    /// </summary>
    public Waypoint Waypoint { get; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="RouteMatrixDestination"/> class.
    /// </summary>
    /// <param name="waypoint">
    /// The waypoint representing the destination location.
    /// </param>
    public RouteMatrixDestination(Waypoint waypoint)
        => Waypoint = waypoint;
}