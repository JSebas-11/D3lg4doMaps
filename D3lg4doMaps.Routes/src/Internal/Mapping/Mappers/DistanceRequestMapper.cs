using DelgadoMaps.Routes.Internal.Mapping.Converters;
using DelgadoMaps.Routes.Internal.Models.DTOs;
using DelgadoMaps.Routes.Enums;
using DelgadoMaps.Routes.Models.Requests;
using DelgadoMaps.Routes.Models.Requests.Common;
using DelgadoMaps.Routes.Models.Requests.Components;

namespace DelgadoMaps.Routes.Internal.Mapping.Mappers;

internal static class DistanceRequestMapper {
    public static DistanceRequestDto ToDistanceRequestDto(DistanceRequest request)
        => new() {
            Origins = [.. request.Origins.Select(ToRouteMatrixOriginDto)], 
            Destinations = request.Destinations,
            TravelMode = request.TravelMode is TravelMode mode
                ? TravelModeConverter.ToApi(mode)
                : null,
            RoutingPreference = request.RoutingPreference is RoutingPreference preference
                ? RoutingPreferenceConverter.ToApi(preference) 
                : null,
            DepartureTime = request.DepartureTime, ArrivalTime = request.ArrivalTime,
            Units = request.Units is Units units
                ? UnitsConverter.ToApi(units) 
                : null,
            TrafficModel = request.TrafficModel is TrafficModel model
                ? TrafficModelConverter.ToApi(model)
                : null,
            TransitPreferences = request.TransitPreferences is TransitPreferences transitPreferences
                ? ToTransitPreferencesDto(transitPreferences)
                : null
        };

    public static RouteMatrixOriginDto ToRouteMatrixOriginDto(RouteMatrixOrigin origin)
        => new() {
            Waypoint = origin.Waypoint,
            RouteModifiers = origin.RouteModifiers is RouteModifiers modifiers
                ? RouteRequestMapper.ToRouteModifiersDto(modifiers)
                : null
        };

    public static TransitPreferencesDto ToTransitPreferencesDto(TransitPreferences preferences)
        => new() {
            AllowedTravelModes = [.. preferences.AllowedTravelModes
                .Select(TransitTravelModeConverter.ToApi)
            ],
            RoutingPreference = preferences.RoutingPreference is TransitRoutingPreference preference
                ? TransitRoutingPreferenceConverter.ToApi(preference)
                : null
        };
}