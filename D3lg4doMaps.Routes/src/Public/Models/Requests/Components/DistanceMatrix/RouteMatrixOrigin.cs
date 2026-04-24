using DelgadoMaps.Routes.Models.Requests.Common;

namespace DelgadoMaps.Routes.Models.Requests.Components;

/// <summary>
/// Represents an origin element in a distance matrix request.
/// </summary>
/// <remarks>
/// Combines a <see cref="Waypoint"/> with optional <see cref="RouteModifiers"/>
/// that influence routing behavior for this specific origin.
/// </remarks>
public sealed class RouteMatrixOrigin {
    /// <summary>
    /// Gets the waypoint representing the origin location.
    /// </summary>
    public Waypoint Waypoint { get; } = null!;

    /// <summary>
    /// Gets optional route modifiers applied to this origin.
    /// </summary>    
    public RouteModifiers? RouteModifiers { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RouteMatrixOrigin"/> class.
    /// </summary>
    /// <param name="waypoint">
    /// The waypoint representing the origin location.
    /// </param>
    /// <param name="routeModifiers">
    /// Optional routing modifiers applied to this origin.
    /// </param>
    public RouteMatrixOrigin(
        Waypoint waypoint, 
        RouteModifiers? routeModifiers = null
    ) {
        Waypoint = waypoint;
        RouteModifiers = routeModifiers;
    }

    internal void ValidateForRequest() 
        => RouteModifiers?.ValidateForRequest();
}