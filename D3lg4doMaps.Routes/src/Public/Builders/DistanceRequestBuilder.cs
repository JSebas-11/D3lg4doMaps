using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Requests;
using D3lg4doMaps.Routes.Public.Models.Requests.Common;
using D3lg4doMaps.Routes.Public.Models.Requests.Components;

namespace D3lg4doMaps.Routes.Public.Builders;

public sealed class DistanceRequestBuilder {
    // -------------------- INIT --------------------
    // REQUIRED
    private List<RouteMatrixOrigin> _origins = [];
    private List<RouteMatrixDestination> _destinations = [];

    // OPTIONAL
    private TravelMode? _travelMode;
    private RoutingPreference? _routingPreference;
    private DateTimeOffset? _departureTime;
    private DateTimeOffset? _arrivalTime;
    private Units? _units;
    private TrafficModel? _trafficModel;
    private TransitPreferences? _transitPreferences;

    // -------------------- BUILD --------------------
    public DistanceRequest Build() {
        if (_origins.Count == 0 || _destinations.Count == 0) 
            throw new MapsInvalidRequestException("Both Origin/s and Destination/s must be provided.");

        if (_origins.Count * _destinations.Count > 100)
            throw new MapsInvalidRequestException("Product origins-destinations reached, maximum 100 crossed elements are allowed.");

        if (_arrivalTime is not null && _departureTime is not null)
            throw new MapsInvalidRequestException("Only one of ArrivalTime or DepartureTime can be set.");

        if (_transitPreferences is not null && _travelMode is not TravelMode.Transit)
            throw new MapsInvalidRequestException("TransitPreferences can only be used when TravelMode is Transit.");

        return new() {
            Origins = _origins.AsReadOnly(), Destinations = _destinations.AsReadOnly(),
            TravelMode = _travelMode, RoutingPreference = _routingPreference,
            DepartureTime = _departureTime, ArrivalTime = _arrivalTime,
            Units = _units, 
            TrafficModel = _trafficModel, TransitPreferences = _transitPreferences
        };
    }

    // -------------------- CONFIG --------------------
    // WAYPOINTS
    public DistanceRequestBuilder WithOrigins(IEnumerable<RouteMatrixOrigin> origins) {
        foreach (var item in origins) item.ValidateForRequest();
        
        _origins = [.. origins];
        return this;
    }
    public DistanceRequestBuilder AddOrigin(RouteMatrixOrigin origin) {
        origin.ValidateForRequest();

        _origins.Add(origin);
        return this;
    }
    public DistanceRequestBuilder AddOrigin(Waypoint originWaypoint, RouteModifiers? modifiers = null) {
        var origin = new RouteMatrixOrigin(originWaypoint, modifiers);
        origin.ValidateForRequest();

        _origins.Add(origin);
        return this;
    }
    public DistanceRequestBuilder WithDestinations(IEnumerable<RouteMatrixDestination> destinations) {
        _destinations = [.. destinations];
        return this;
    }
    public DistanceRequestBuilder AddDestination(RouteMatrixDestination destination) {
        _destinations.Add(destination);
        return this;
    }
    public DistanceRequestBuilder AddDestination(Waypoint destinationWaypoint) {
        var destination = new RouteMatrixDestination(destinationWaypoint);

        _destinations.Add(destination);
        return this;
    }

    // ROUTING
    public DistanceRequestBuilder WithTravelMode(TravelMode travelMode) {
        ValidateNotUnknown(travelMode, nameof(travelMode));

        _travelMode = travelMode;
        return this;
    }
    public DistanceRequestBuilder WithRoutingPreference(RoutingPreference routingPreference) {
        ValidateNotUnknown(routingPreference, nameof(routingPreference));

        _routingPreference = routingPreference;
        return this;
    }

    // TIME
    public DistanceRequestBuilder WithDepartureTime(DateTimeOffset departureTime) {
        _departureTime = departureTime;
        return this;
    }
    public DistanceRequestBuilder WithArrivalTime(DateTimeOffset arrivalTime) {
        _arrivalTime = arrivalTime;
        return this;
    }

    // MEASURES
    public DistanceRequestBuilder WithUnits(Units units) {
        ValidateNotUnknown(units, nameof(units));

        _units = units;
        return this;
    }

    // TRAFFIC
    public DistanceRequestBuilder WithTrafficModel(TrafficModel trafficModel) {
        ValidateNotUnknown(trafficModel, nameof(trafficModel));

        _trafficModel = trafficModel;
        return this;
    }

    // TRANSIT
    public DistanceRequestBuilder WithTransitPreferences(TransitPreferences transitPreferences) {
        transitPreferences.ValidateForRequest();
        
        _transitPreferences = transitPreferences;
        return this;
    }

    // -------------------- INNER METHS --------------------
    private static void ValidateNotUnknown<TEnum>(TEnum value, string paramName)
        where TEnum : struct, Enum {
        if (value.ToString().Equals("Unknown", StringComparison.Ordinal))
            throw new MapsInvalidRequestException(
                $"Unknown {paramName} value is for internal use only and cannot be sent to the API."
            );
    }
}