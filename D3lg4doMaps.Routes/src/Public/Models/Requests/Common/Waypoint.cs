using D3lg4doMaps.Routes.Public.Builders;
using D3lg4doMaps.Routes.Public.Models.Components;

namespace D3lg4doMaps.Routes.Public.Models.Requests.Common;

/// <summary>
/// Represents a location used in routing operations.
/// </summary>
/// <remarks>
/// A waypoint can be defined either by a <c>PlaceId</c> or geographic coordinates.
/// It may also include additional routing hints such as stopovers or road alignment.
///
/// Instances are typically created using <see cref="WaypointBuilder"/>.
/// </remarks>
public sealed class Waypoint {
    /// <summary>
    /// Gets whether the waypoint is treated as a via point.
    /// </summary>
    /// <remarks>
    /// Via points influence the route path but do not create a stopover.
    /// </remarks>
    public bool Via { get; internal set; }

    /// <summary>
    /// Gets whether the waypoint represents a vehicle stopover.
    /// </summary>
    public bool VehicleStopover { get; internal set; }

    /// <summary>
    /// Gets whether the waypoint should be matched to the nearest road segment.
    /// </summary>
    public bool SideOfRoad { get; internal set; }

    /// <summary>
    /// Gets the place identifier of the waypoint, if defined.
    /// </summary>
    public string? PlaceId { get; }

    /// <summary>
    /// Gets the geographic location of the waypoint, if defined.
    /// </summary>
    public Location? Location { get; }

    internal Waypoint(string? placeId, Location? location) {
        PlaceId = placeId;
        Location = location;
    }
}