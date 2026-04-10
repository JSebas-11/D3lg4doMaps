using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Requests;
using D3lg4doMaps.Routes.Public.Models.Requests.Common;

namespace D3lg4doMaps.Routes.Public.Builders;

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
        ValidateNotUnknown(polyQuality, nameof(polyQuality));
        ValidateNotUnknown(polyEncoding, nameof(polyEncoding));

        _polylineQuality = polyQuality;
        _polylineEncoding = polyEncoding;
        return this;
    }

    // ROUTING
    public RouteRequestBuilder WithTravelMode(TravelMode travelMode) {
        ValidateNotUnknown(travelMode, nameof(travelMode));

        _travelMode = travelMode;
        return this;
    }
    public RouteRequestBuilder WithRoutingPreference(RoutingPreference routingPreference) {
        ValidateNotUnknown(routingPreference, nameof(routingPreference));

        _routingPreference = routingPreference;
        return this;
    }
    public RouteRequestBuilder WithRouteModifiers(RouteModifiers routeModifiers) {
        routeModifiers.ValidateForRequest();

        _routeModifiers = routeModifiers;
        return this;
    }

    // MEASURES
    public RouteRequestBuilder WithUnits(Units units) {
        ValidateNotUnknown(units, nameof(units));

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

    private static void ValidateNotUnknown<TEnum>(TEnum value, string paramName)
        where TEnum : struct, Enum {
        if (value.ToString().Equals("Unknown", StringComparison.Ordinal))
            throw new MapsInvalidRequestException(
                $"Unknown {paramName} value is for internal use only and cannot be sent to the API."
            );
    }
}