using DelgadoMaps.Routes.Enums;

namespace DelgadoMaps.Routes.Internal.Mapping.Converters;

internal static class RouteElementConditionConverter {
    public static RouteElementCondition FromApi(string? condition)
        => condition?.ToUpperInvariant() switch {
            "ROUTE_EXISTS" => RouteElementCondition.RouteExists,
            "ROUTE_NOT_FOUND" => RouteElementCondition.RouteNotFound,
            _ => RouteElementCondition.Unknown
        };
        
    public static string ToApi(RouteElementCondition condition)
        => condition switch {
            RouteElementCondition.RouteExists => "ROUTE_EXISTS",
            RouteElementCondition.RouteNotFound => "ROUTE_NOT_FOUND",
            RouteElementCondition.Unknown => throw new ArgumentException("Cannot convert Unknown RouteElementCondition to API value."),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
}