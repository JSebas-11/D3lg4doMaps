using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Routes.Enums;

namespace DelgadoMaps.Routes.Models.Requests.Components;

/// <summary>
/// Represents preferences used when routing with transit travel mode.
/// </summary>
/// <remarks>
/// Allows specifying allowed transit modes and routing preferences
/// such as minimizing walking or transfers.
/// </remarks>
public sealed class TransitPreferences {
    /// <summary>
    /// Gets the list of allowed transit travel modes.
    /// </summary>
    public IReadOnlyList<TransitTravelMode> AllowedTravelModes { get; } = [];
    
    /// <summary>
    /// Gets the routing preference for transit.
    /// </summary>
    public TransitRoutingPreference? RoutingPreference { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TransitPreferences"/> class.
    /// </summary>
    /// <param name="transitTravelModes">
    /// The allowed transit travel modes.
    /// </param>
    /// <param name="transitRoutingPreference">
    /// Optional routing preference.
    /// </param>
    public TransitPreferences(
        List<TransitTravelMode> transitTravelModes,
        TransitRoutingPreference? transitRoutingPreference = null
    ) {
        AllowedTravelModes = transitTravelModes.AsReadOnly();
        RoutingPreference = transitRoutingPreference;
    }

    internal void ValidateForRequest() {
        if (AllowedTravelModes.Count == 0)
            throw new MapsInvalidRequestException(
                "At least one TransitTravelMode must be specified."
            );

        foreach (var item in AllowedTravelModes) {
            if (item == TransitTravelMode.Unknown)
                throw new MapsInvalidRequestException(
                    "Unknown TransitTravelMode value is for internal use only and cannot be sent to the API."
                );
        }

        if (RoutingPreference is TransitRoutingPreference.Unknown)
            throw new MapsInvalidRequestException(
                "Unknown TransitRoutingPreference value is for internal use only and cannot be sent to the API."
            );
    }
}