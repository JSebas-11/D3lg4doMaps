using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Routes.Enums;
using DelgadoMaps.Routes.Models.Requests;
using DelgadoMaps.Routes.Models.Requests.Common;

namespace DelgadoMaps.Routes.Builders;

/// <summary>
/// Provides a fluent API for constructing <see cref="RouteRequest"/> instances.
/// </summary>
/// <remarks>
/// This builder is used to configure route calculations by defining origin,
/// destination, intermediate waypoints, and routing preferences such as travel mode,
/// polyline configuration, and timing constraints.
/// 
/// It enforces validation rules to ensure requests are valid and compliant
/// with API limitations.
/// </remarks>
public sealed class RouteRequestBuilder {
    #region INIT
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
    #endregion
    
    #region BUILD
    /// <summary>
    /// Builds a new <see cref="RouteRequest"/> instance.
    /// </summary>
    /// <returns>
    /// A fully configured <see cref="RouteRequest"/>.
    /// </returns>
    /// <exception cref="MapsInvalidRequestException">
    /// Thrown when:
    /// - Origin or destination is not provided
    /// - Both arrival and departure times are set
    /// - Waypoint optimization is enabled without intermediates
    /// - More than 25 intermediate waypoints are provided
    /// - Waypoint heading is used with unsupported travel modes
    /// </exception>
    /// <remarks>
    /// Waypoint heading is only supported for:
    /// - <see cref="TravelMode.Drive"/>
    /// - <see cref="TravelMode.TwoWheeler"/>
    /// </remarks>
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
    #endregion

    #region CONFIG
    // WAYPOINTS

    /// <summary>
    /// Sets the origin waypoint of the route.
    /// </summary>
    public RouteRequestBuilder From(Waypoint origin) {
        _origin = origin;
        return this;
    }

    /// <summary>
    /// Sets the destination waypoint of the route.
    /// </summary>
    public RouteRequestBuilder To(Waypoint destination) {
        _destination = destination;
        return this;
    }

    /// <summary>
    /// Sets the intermediate waypoints for the route.
    /// </summary>
    /// <remarks>
    /// A maximum of 25 intermediate waypoints is allowed.
    /// </remarks>
    public RouteRequestBuilder WithIntermediates(IEnumerable<Waypoint> intermediates) {
        _intermediates = [.. intermediates];
        return this;
    }

    /// <summary>
    /// Adds a single intermediate waypoint to the route.
    /// </summary>
    public RouteRequestBuilder AddIntermediate(Waypoint intermediate) {
        _intermediates.Add(intermediate);
        return this;
    }
    
    // TIME

    /// <summary>
    /// Sets the departure time for the route.
    /// </summary>
    public RouteRequestBuilder WithDepartureTime(DateTimeOffset departureTime) {
        _departureTime = departureTime;
        return this;
    }

    /// <summary>
    /// Sets the desired arrival time for the route.
    /// </summary>
    public RouteRequestBuilder WithArrivalTime(DateTimeOffset arrivalTime) {
        _arrivalTime = arrivalTime;
        return this;
    }

    // POLYLINE

    /// <summary>
    /// Configures the polyline representation of the route.
    /// </summary>
    /// <param name="polyQuality">
    /// The level of detail for the polyline geometry.
    /// </param>
    /// <param name="polyEncoding">
    /// The encoding format used for the polyline.
    /// </param>
    public RouteRequestBuilder WithPolyline(PolylineQuality polyQuality, PolylineEncoding polyEncoding) {
        ValidateNotUnknown(polyQuality, nameof(polyQuality));
        ValidateNotUnknown(polyEncoding, nameof(polyEncoding));

        _polylineQuality = polyQuality;
        _polylineEncoding = polyEncoding;
        return this;
    }

    // ROUTING

    /// <summary>
    /// Sets the travel mode used for route calculation.
    /// </summary>
    public RouteRequestBuilder WithTravelMode(TravelMode travelMode) {
        ValidateNotUnknown(travelMode, nameof(travelMode));

        _travelMode = travelMode;
        return this;
    }

    /// <summary>
    /// Sets the routing preference (e.g., traffic-aware routing).
    /// </summary>
    public RouteRequestBuilder WithRoutingPreference(RoutingPreference routingPreference) {
        ValidateNotUnknown(routingPreference, nameof(routingPreference));

        _routingPreference = routingPreference;
        return this;
    }

    /// <summary>
    /// Applies route modifiers such as avoiding tolls or highways.
    /// </summary>
    public RouteRequestBuilder WithRouteModifiers(RouteModifiers routeModifiers) {
        routeModifiers.ValidateForRequest();

        _routeModifiers = routeModifiers;
        return this;
    }

    // MEASURES

    /// <summary>
    /// Sets the unit system used for distance values.
    /// </summary>
    public RouteRequestBuilder WithUnits(Units units) {
        ValidateNotUnknown(units, nameof(units));

        _units = units;
        return this;
    }

    // FLAGS

    /// <summary>
    /// Enables computation of alternative routes.
    /// </summary>
    /// <remarks>
    /// When enabled, multiple route options may be returned instead of a single optimal route.
    /// </remarks>
    public RouteRequestBuilder WithAlternativeRoutes() {
        _computeAlternativeRoutes = true;
        return this;
    }

    /// <summary>
    /// Enables automatic optimization of intermediate waypoint order.
    /// </summary>
    /// <remarks>
    /// Requires at least one intermediate waypoint.
    /// </remarks>
    public RouteRequestBuilder OptimizeWaypointOrder() {
        _optimizeWaypointOrder = true;
        return this;
    }
    #endregion

    #region INNER METHS
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
    #endregion
}