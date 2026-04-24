using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Core.Models.Geometry;
using DelgadoMaps.Routes.Models.Components;
using DelgadoMaps.Routes.Models.Requests.Common;

namespace DelgadoMaps.Routes.Builders;

/// <summary>
/// Provides a fluent API for constructing <see cref="Waypoint"/> instances.
/// </summary>
/// <remarks>
/// A waypoint represents a location used in routing operations such as directions
/// and distance matrix calculations.
///
/// A waypoint can be defined using either a <c>PlaceId</c> or geographic coordinates,
/// but never both at the same time.
/// </remarks>
public sealed class WaypointBuilder {
    #region INIT
    // REQUIRED
    private Location? _location;
    private string? _placeId;

    // OPTIONAL
    private bool _via = false;
    private bool _vehicleStopover = false;
    private bool _sideOfRoad = false;
    #endregion

    #region BUILD
    /// <summary>
    /// Builds a new <see cref="Waypoint"/> instance.
    /// </summary>
    /// <returns>
    /// A fully configured <see cref="Waypoint"/>.
    /// </returns>
    /// <exception cref="MapsInvalidRequestException">
    /// Thrown when both or neither of <c>PlaceId</c> and <c>Location</c> are provided.
    /// </exception>
    public Waypoint Build() {
        if ((string.IsNullOrWhiteSpace(_placeId) && _location is null) || 
            (!string.IsNullOrWhiteSpace(_placeId) && _location is not null))
            throw new MapsInvalidRequestException("Only one of PlaceId or Location must be provided.");
        
        return new Waypoint(_placeId, _location) {
            Via = _via, VehicleStopover = _vehicleStopover, SideOfRoad = _sideOfRoad
        };
    }
    #endregion

    #region CONFIG
    /// <summary>
    /// Defines the waypoint using a Google Place ID.
    /// </summary>
    /// <param name="placeId">
    /// The unique identifier of the place.
    /// </param>
    /// <returns>
    /// The current <see cref="WaypointBuilder"/> instance.
    /// </returns>
    /// <exception cref="MapsInvalidRequestException">
    /// Thrown when the place ID is null, empty, or whitespace.
    /// </exception>
    public WaypointBuilder FromPlaceId(string placeId) {
        if (string.IsNullOrWhiteSpace(placeId)) 
            throw new MapsInvalidRequestException("PlaceId cannot be empty.");

        _placeId = placeId;
        return this;
    }

    /// <summary>
    /// Defines the waypoint using geographic coordinates.
    /// </summary>
    /// <param name="latitude">
    /// The latitude of the location (between -90 and 90).
    /// </param>
    /// <param name="longitude">
    /// The longitude of the location (between -180 and 180).
    /// </param>
    /// <param name="heading">
    /// Optional heading in degrees (0–360) used for routing direction.
    /// </param>
    /// <returns>
    /// The current <see cref="WaypointBuilder"/> instance.
    /// </returns>
    /// <exception cref="MapsInvalidRequestException">
    /// Thrown when latitude, longitude, or heading values are out of range.
    /// </exception>
    public WaypointBuilder FromLocation(double latitude, double longitude, int? heading = null) {
        // MEASURES VALIDATION
        if (latitude is < -90 or > 90) 
            throw new MapsInvalidRequestException("Latitude must be between -90 and 90.");
        if (longitude is < -180 or > 180) 
            throw new MapsInvalidRequestException("Longitude must be between -180 and 180.");  
        
        _location = new Location() { LatLng = new LatLng(latitude, longitude) };

        if (heading is not null) {
            if (heading is < 0 or > 360)
                throw new MapsInvalidRequestException("Heading must be between 0 and 360.");

            _location.Heading = heading;
        } 

        return this;
    }

    /// <summary>
    /// Marks the waypoint as a <c>via</c> point.
    /// </summary>
    /// <returns>
    /// The current <see cref="WaypointBuilder"/> instance.
    /// </returns>
    /// <remarks>
    /// A via point influences the route path but does not create a stopover.
    /// </remarks>
    public WaypointBuilder WithVia() {
        _via = true;
        return this;
    }

    /// <summary>
    /// Indicates that the waypoint should be matched to the nearest road segment.
    /// </summary>
    /// <returns>
    /// The current <see cref="WaypointBuilder"/> instance.
    /// </returns>
    public WaypointBuilder WithSideOfRoad() {
        _sideOfRoad = true;
        return this;
    }

    /// <summary>
    /// Marks the waypoint as a stopover for vehicles.
    /// </summary>
    /// <returns>
    /// The current <see cref="WaypointBuilder"/> instance.
    /// </returns>
    /// <remarks>
    /// Stopovers represent explicit stops along the route.
    /// </remarks>
    public WaypointBuilder WithStopover() {
        _vehicleStopover = true;
        return this;
    }
    #endregion
}