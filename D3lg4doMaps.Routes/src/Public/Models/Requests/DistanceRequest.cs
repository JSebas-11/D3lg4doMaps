using DelgadoMaps.Routes.Builders;
using DelgadoMaps.Routes.Enums;
using DelgadoMaps.Routes.Models.Requests.Components;

namespace DelgadoMaps.Routes.Models.Requests;

/// <summary>
/// Represents a request for computing distances and durations between multiple origins and destinations.
/// </summary>
/// <remarks>
/// This model is constructed using <see cref="DistanceRequestBuilder"/> and is used by
/// IDistanceMatrixService to calculate travel metrics across multiple route combinations.
///
/// Supports advanced configuration such as travel mode, traffic modeling, and transit preferences.
/// </remarks>
public sealed class DistanceRequest {
    /// <summary>
    /// Gets the list of origin waypoints.
    /// </summary>
    public IReadOnlyList<RouteMatrixOrigin> Origins { get; internal set; } = [];

    /// <summary>
    /// Gets the list of destination waypoints.
    /// </summary>
    public IReadOnlyList<RouteMatrixDestination> Destinations { get; internal set; } = [];

    /// <summary>
    /// Gets the travel mode used for distance calculations.
    /// </summary>
    public TravelMode? TravelMode { get; internal set; } 

    /// <summary>
    /// Gets the routing preference object.
    /// </summary>
    public RoutingPreference? RoutingPreference { get; internal set; } 

    /// <summary>
    /// Gets the departure time used for calculations.
    /// </summary>
    public DateTimeOffset? DepartureTime { get; internal set; }

    /// <summary>
    /// Gets the arrival time for the requested routes.
    /// </summary>
    public DateTimeOffset? ArrivalTime { get; internal set; }

    /// <summary>
    /// Gets the unit system used in the response.
    /// </summary>
    public Units? Units { get; internal set; }

    /// <summary>
    /// Gets the unit system used in the response.
    /// </summary>
    public TrafficModel? TrafficModel { get; internal set; }

    /// <summary>
    /// Gets transit-specific preferences when using transit travel mode.
    /// </summary>
    public TransitPreferences? TransitPreferences { get; internal set; }
}