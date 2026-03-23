using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Core.Public.Models.Geometry;
using D3lg4doMaps.Routing.Public.Models.Common;
using D3lg4doMaps.Routing.Public.Models.Utilities;

namespace D3lg4doMaps.Routing.Public.Builders;

public sealed class WaypointBuilder {
    // -------------------- INIT --------------------
    // REQUIRED
    private Location? _location;
    private string? _placeId;

    // OPTIONAL
    private bool _via = false;
    private bool _vehicleStopover = false;
    private bool _sideOfRoad = false;

    // -------------------- BUILD --------------------
    public Waypoint Build() {
        if ((string.IsNullOrWhiteSpace(_placeId) && _location is null) || 
            (!string.IsNullOrWhiteSpace(_placeId) && _location is not null))
            throw new MapsInvalidRequestException("Only one of PlaceId or Location must be provided.");
        
        return new Waypoint(_placeId, _location) {
            Via = _via, VehicleStopover = _vehicleStopover, SideOfRoad = _sideOfRoad
        };
    }

    // -------------------- CONFIG --------------------
    public WaypointBuilder FromPlaceId(string placeId) {
        if (string.IsNullOrWhiteSpace(placeId)) 
            throw new MapsInvalidRequestException("PlaceId cannot be empty.");

        _placeId = placeId;
        return this;
    }

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

    public WaypointBuilder AsVia() {
        _via = true;
        return this;
    }
    public WaypointBuilder WithSideOfRoad() {
        _sideOfRoad = true;
        return this;
    }
    public WaypointBuilder WithStopover() {
        _vehicleStopover = true;
        return this;
    }
}