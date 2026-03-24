using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Routing.Public.Enums;
using D3lg4doMaps.Routing.Public.Models.Common;
using D3lg4doMaps.Routing.Public.Models.Requests;

namespace D3lg4doMaps.Routing.Public.Builders;

public sealed class RouteRequestBuilder {
    // -------------------- INIT --------------------
    // REQUIRED
    private Waypoint? _origin;
    private Waypoint? _destination;
    
    // OPTIONAL
    private List<Waypoint> _intermediates = [];
    private TravelMode? _travelMode;
    private RoutingPreference? _routingPreference;
    private PolylineQuality? _polylineQuality;
    private PolylineEncoding? _polylineEncoding;
    private DateTimeOffset? _departureTime;
    private DateTimeOffset? _arrivalTime;
    private bool? _computeAlternativeRoutes;
    private bool? _optimizeWaypointOrder;
    private RouteModifiers? _routeModifiers;
    private Units? _units;
    
    // -------------------- BUILD --------------------
    public RouteRequest Build() {
        if (_origin is null || _destination is null)
            throw new MapsInvalidRequestException("Both Origin and Destination must be provided.");

        if (_arrivalTime is not null && _departureTime is not null)
            throw new MapsInvalidRequestException("Only one of ArrivalTime or DepartureTime can be set.");
        
        if (_optimizeWaypointOrder == true && _intermediates.Count == 0)
            throw new MapsInvalidRequestException("OptimizeWaypointOrder requires providing Intermediates.");
        
        if (_intermediates.Count > 25)
            throw new MapsInvalidRequestException("Maximum 25 Intermediates are allowed.");

        if (WaypointsContainsHeading()) 
            if (_travelMode != TravelMode.Drive && _travelMode != TravelMode.TwoWheeler)
                throw new MapsInvalidRequestException("Waypoint heading is only supported for Drive and TwoWheeler travel modes.");

        return new RouteRequest() {
            Origin = _origin, Destination = _destination, Intermediates = _intermediates.AsReadOnly(),
            TravelMode = _travelMode, RoutingPreference = _routingPreference,
            PolylineQuality = _polylineQuality, PolylineEncoding = _polylineEncoding,
            DepartureTime = _departureTime, ArrivalTime = _arrivalTime,
            ComputeAlternativeRoutes = _computeAlternativeRoutes, OptimizeWaypointOrder = _optimizeWaypointOrder,
            RouteModifiers = _routeModifiers, Units = _units
        };
    }

    // -------------------- CONFIG --------------------
    // WAYPOINTS
    public RouteRequestBuilder From(Waypoint origin) {
        _origin = origin;
        return this;
    }
    public RouteRequestBuilder To(Waypoint destination) {
        _destination = destination;
        return this;
    }
    public RouteRequestBuilder WithIntermediates(IEnumerable<Waypoint> intermediates) {
        _intermediates = [.. intermediates];
        return this;
    }
    public RouteRequestBuilder AddIntermediate(Waypoint intermediate) {
        _intermediates.Add(intermediate);
        return this;
    }
    
    // TIME
    public RouteRequestBuilder WithDepartureTime(DateTimeOffset departureTime) {
        _departureTime = departureTime;
        return this;
    }
    public RouteRequestBuilder WithArrivalTime(DateTimeOffset arrivalTime) {
        _arrivalTime = arrivalTime;
        return this;
    }

    // POLYLINE
    public RouteRequestBuilder WithPolyline(PolylineQuality polyQuality, PolylineEncoding polyEncoding) {
        _polylineQuality = polyQuality;
        _polylineEncoding = polyEncoding;
        return this;
    }

    // ROUTING
    public RouteRequestBuilder WithTravelMode(TravelMode travelMode) {
        _travelMode = travelMode;
        return this;
    }
    public RouteRequestBuilder WithRoutingPreference(RoutingPreference routingPreference) {
        _routingPreference = routingPreference;
        return this;
    }
    public RouteRequestBuilder WithRouteModifiers(RouteModifiers routeModifiers) {
        _routeModifiers = routeModifiers;
        return this;
    }

    // MEASURES
    public RouteRequestBuilder WithUnits(Units units) {
        _units = units;
        return this;
    }

    // BOOLEANS
    public RouteRequestBuilder WithAlternativeRoutes() {
        _computeAlternativeRoutes = true;
        return this;
    }
    public RouteRequestBuilder OptimizeWaypointOrder() {
        _optimizeWaypointOrder = true;
        return this;
    }

    // -------------------- INNER METHS --------------------
    private bool WaypointsContainsHeading() {
        if (_origin?.Location?.Heading is not null) return true;
        if (_destination?.Location?.Heading is not null) return true;

        foreach (Waypoint item in _intermediates) {
            if (item.Location?.Heading is not null) return true;
        }

        return false;
    }
}