using D3lg4doMaps.Routes.Internal.Mapping.Converters;
using D3lg4doMaps.Routes.Internal.Models.DTOs;
using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Requests;
using D3lg4doMaps.Routes.Public.Models.Requests.Common;

namespace D3lg4doMaps.Routes.Internal.Mapping.Mappers;

internal static class RouteRequestMapper {
    public static RouteRequestDto ToRouteRequestDto(RouteRequest request)
        => new() {
            Origin = request.Origin, Destination = request.Destination,
            Intermediates = request.Intermediates,
            TravelMode = request.TravelMode is TravelMode mode 
                ? TravelModeConverter.ToApi(mode) 
                : null,
            RoutingPreference = request.RoutingPreference is RoutingPreference preference 
                ? RoutingPreferenceConverter.ToApi(preference) 
                : null,
            PolylineQuality = request.PolylineQuality is PolylineQuality quality 
                ? PolylineConverter.QualityToApi(quality) 
                : null, 
            PolylineEncoding = request.PolylineEncoding is PolylineEncoding encoding 
                ? PolylineConverter.EncodingToApi(encoding) 
                : null,
            DepartureTime = request.DepartureTime, ArrivalTime = request.ArrivalTime,
            ComputeAlternativeRoutes = request.ComputeAlternativeRoutes,
            OptimizeWaypointOrder = request.OptimizeWaypointOrder,
            RouteModifiers = request.RouteModifiers is RouteModifiers modifiers
                ? ToRouteModifiersDto(modifiers)
                : null,
            Units = request.Units is Units units 
                ? UnitsConverter.ToApi(units) 
                : null
        };
    
    public static RouteModifiersDto ToRouteModifiersDto(RouteModifiers modifiers)
        => new() {
            AvoidTolls = modifiers.AvoidTolls, AvoidHighways = modifiers.AvoidHighways,
            AvoidFerries = modifiers.AvoidFerries, AvoidIndoor = modifiers.AvoidIndoor,
            VehicleInfo = modifiers.VehicleInfo is VehicleInfo vehicleInfo 
                ? ToVehicleInfoDto(vehicleInfo)
                : null
        };
    
    public static VehicleInfoDto ToVehicleInfoDto(VehicleInfo vehicleInfo)
        => new() {
            EmissionType = vehicleInfo.EmissionType is VehicleEmissionType emissionType 
                ? VehicleConverter.EmissionTypeToApi(emissionType) 
                : null
        };
}