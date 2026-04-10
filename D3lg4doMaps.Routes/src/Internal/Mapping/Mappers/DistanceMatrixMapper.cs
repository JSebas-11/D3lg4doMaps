using System.Text.Json;
using D3lg4doMaps.Core.Public.Extensions;
using D3lg4doMaps.Routes.Internal.Extensions;
using D3lg4doMaps.Routes.Internal.Mapping.Converters;
using D3lg4doMaps.Routes.Public.Models.Common;
using D3lg4doMaps.Routes.Public.Models.DistanceMatrix;
using D3lg4doMaps.Routes.Public.Models.DistanceMatrix.Components;

namespace D3lg4doMaps.Routes.Internal.Mapping.Mappers;

internal static class DistanceMatrixMapper {
    // -------------------- MAIN MODELS --------------------
    public static RouteMatrixElement ToRouteMatrixElement(JsonDocument json) {
        var root = json.RootElement;

        var matrixElement = new RouteMatrixElement() {
            Condition = RouteElementConditionConverter.FromApi(root.GetStringValue("condition")),
            DistanceMeters = root.GetInt("distanceMeters"),
            Duration = root.GetStringValue("duration"),
            StaticDuration = root.GetStringValue("staticDuration"),
            OriginIndex = root.GetInt("originIndex"),
            DestinationIndex = root.GetInt("destinationIndex")
        };

        // STATUS
        var status = root.GetObject("status");
        if (status is not null)
            matrixElement.Status = ToStatus((JsonElement)status);

        // TRAVELADVISOR
        var travelAdvisory = root.GetObject("travelAdvisory");
        if (travelAdvisory is not null)
            matrixElement.TravelAdvisory = DirectionsMapper.ToRouteTravelAdvisory((JsonElement)travelAdvisory);

        // FALLBACKINFO
        var fallback = root.GetObject("fallbackInfo");
        if (fallback is not null)
            matrixElement.FallbackInfo = ToFallbackInfo((JsonElement)fallback);

        // LOCALIZEDVALUES
        var locValues = root.GetObject("localizedValues");
        if (locValues is not null)
            matrixElement.LocalizedValues = new RouteLocalizedValues() {
                Distance = locValues?.GetLocalizedValueText("distance") ?? "",
                Duration = locValues?.GetLocalizedValueText("duration") ?? "",
                StaticDuration = locValues?.GetLocalizedValueText("staticDuration"),
                TransitFare = locValues?.GetLocalizedValueText("transitFare")
            };

        return matrixElement;
    }

    // -------------------- ASIDE MODELS --------------------
    public static Status ToStatus(JsonElement json)
        => new() {
            Code = json.GetInt("code") ?? 0,
            Message = json.GetStringValue("message")
        };

    public static FallbackInfo ToFallbackInfo(JsonElement json)
        => new() {
            RoutingMode = RoutingModeConverter.FromApi(json.GetStringValue("routingMode")),
            Reason = ReasonConverter.FromApi(json.GetStringValue("reason"))
        };
}