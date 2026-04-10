using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Routes.Public.Enums;

namespace D3lg4doMaps.Routes.Public.Models.Requests.Components;

public sealed class TransitPreferences {
    public IReadOnlyList<TransitTravelMode> AllowedTravelModes { get; } = [];
    public TransitRoutingPreference? RoutingPreference { get; }

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