using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Requests;
using D3lg4doMaps.Routes.Public.Models.Requests.Common;
using D3lg4doMaps.Routes.Public.Models.Requests.Components;

namespace D3lg4doMaps.Routes.Public.Builders;

/// <summary>
/// Provides a fluent API for constructing <see cref="DistanceRequest"/> instances.
/// </summary>
/// <remarks>
/// This builder is used to configure distance matrix requests by defining
/// origins, destinations, and optional routing preferences such as 
/// travel mode, traffic behavior, and time constraints.
/// 
/// It enforces validation rules to ensure requests comply with API limits
/// and constraints.
/// </remarks>
public sealed class DistanceRequestBuilder {
    #region FIELDS
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
    #endregion

    #region BUILD
    /// <summary>
    /// Builds a new <see cref="DistanceRequest"/> instance.
    /// </summary>
    /// <returns>
    /// A fully configured <see cref="DistanceRequest"/>.
    /// </returns>
    /// <exception cref="MapsInvalidRequestException">
    /// Thrown when:
    /// - Origins or destinations are missing
    /// - The total number of matrix elements exceeds 100
    /// - Both arrival and departure times are set
    /// - Transit preferences are used with a non-transit travel mode
    /// </exception>
    /// <remarks>
    /// The total number of computed routes is calculated as:
    /// <c>origins × destinations</c>, with a maximum of 100 elements.
    /// </remarks>
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
    #endregion

    #region CONFIG
    // WAYPOINTS

    /// <summary>
    /// Sets the collection of origin points.
    /// </summary>
    public DistanceRequestBuilder WithOrigins(IEnumerable<RouteMatrixOrigin> origins) {
        foreach (var item in origins) item.ValidateForRequest();
        
        _origins = [.. origins];
        return this;
    }

    /// <summary>
    /// Adds a single origin to the request.
    /// </summary>
    public DistanceRequestBuilder AddOrigin(RouteMatrixOrigin origin) {
        origin.ValidateForRequest();

        _origins.Add(origin);
        return this;
    }

    /// <summary>
    /// Adds a new origin from a waypoint and optional modifiers.
    /// </summary>
    public DistanceRequestBuilder AddOrigin(Waypoint originWaypoint, RouteModifiers? modifiers = null) {
        var origin = new RouteMatrixOrigin(originWaypoint, modifiers);
        origin.ValidateForRequest();

        _origins.Add(origin);
        return this;
    }

    /// <summary>
    /// Sets the collection of destination points.
    /// </summary>
    public DistanceRequestBuilder WithDestinations(IEnumerable<RouteMatrixDestination> destinations) {
        _destinations = [.. destinations];
        return this;
    }

    /// <summary>
    /// Adds a single destination to the request.
    /// </summary>
    public DistanceRequestBuilder AddDestination(RouteMatrixDestination destination) {
        _destinations.Add(destination);
        return this;
    }

    /// <summary>
    /// Adds a destination from a waypoint.
    /// </summary>
    public DistanceRequestBuilder AddDestination(Waypoint destinationWaypoint) {
        var destination = new RouteMatrixDestination(destinationWaypoint);

        _destinations.Add(destination);
        return this;
    }

    // ROUTING

    /// <summary>
    /// Sets the travel mode used for route computation.
    /// </summary>
    public DistanceRequestBuilder WithTravelMode(TravelMode travelMode) {
        ValidateNotUnknown(travelMode, nameof(travelMode));

        _travelMode = travelMode;
        return this;
    }

    /// <summary>
    /// Sets the routing preference.
    /// </summary>
    public DistanceRequestBuilder WithRoutingPreference(RoutingPreference routingPreference) {
        ValidateNotUnknown(routingPreference, nameof(routingPreference));

        _routingPreference = routingPreference;
        return this;
    }

    // TIME

    /// <summary>
    /// Sets the desired departure time for the route.
    /// </summary>
    public DistanceRequestBuilder WithDepartureTime(DateTimeOffset departureTime) {
        _departureTime = departureTime;
        return this;
    }

    /// <summary>
    /// Sets the desired arrival time for the route.
    /// </summary>
    public DistanceRequestBuilder WithArrivalTime(DateTimeOffset arrivalTime) {
        _arrivalTime = arrivalTime;
        return this;
    }

    // MEASURES

    /// <summary>
    /// Sets the unit system for distance values.
    /// </summary>
    public DistanceRequestBuilder WithUnits(Units units) {
        ValidateNotUnknown(units, nameof(units));

        _units = units;
        return this;
    }

    // TRAFFIC

    /// <summary>
    /// Sets the traffic model used for predictions.
    /// </summary>
    public DistanceRequestBuilder WithTrafficModel(TrafficModel trafficModel) {
        ValidateNotUnknown(trafficModel, nameof(trafficModel));

        _trafficModel = trafficModel;
        return this;
    }

    // TRANSIT

    /// <summary>
    /// Sets transit preferences for routes that influence the returned route.
    /// </summary>
    public DistanceRequestBuilder WithTransitPreferences(TransitPreferences transitPreferences) {
        transitPreferences.ValidateForRequest();
        
        _transitPreferences = transitPreferences;
        return this;
    }
    #endregion

    #region INNER METHS
    private static void ValidateNotUnknown<TEnum>(TEnum value, string paramName)
        where TEnum : struct, Enum {
        if (value.ToString().Equals("Unknown", StringComparison.Ordinal))
            throw new MapsInvalidRequestException(
                $"Unknown {paramName} value is for internal use only and cannot be sent to the API."
            );
    }
    #endregion
}