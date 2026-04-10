using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Requests;

namespace D3lg4doMaps.Routes.Internal.Factories;

internal static class FieldMaskFactory {
    // -------------------- PROPS --------------------
    // ROUTES
    private static readonly string _summFields = 
        "routes.distanceMeters,routes.duration,routes.description,routes.routeLabels,routes.polyline.encodedPolyline";
    private static readonly string _stdFields =
        _summFields + ",routes.staticDuration,routes.warnings,routes.viewport,routes.routeToken";
    private static readonly string _fullFields =
        _stdFields + ",routes.legs,routes.travelAdvisory,routes.localizedValues,routes.optimizedIntermediateWaypointIndex";
    
    // DISTANCE
    private static readonly string _summDMFields = 
        "originIndex,destinationIndex,status,condition,distanceMeters,duration";
    private static readonly string _stdDMFields =
        _summDMFields + ",staticDuration";
    private static readonly string _fullDMFields =
        _stdDMFields + ",travelAdvisory,fallbackInfo,localizedValues";

    // -------------------- METHS --------------------
    public static string GetFieldMask(RouteDetailLevel detailLevel)
        => detailLevel switch {
            RouteDetailLevel.Summary => _summFields,
            RouteDetailLevel.Standard => _stdFields,
            RouteDetailLevel.Full => _fullFields,
            _ => _summFields
        };

    public static string GetDistanceMatrixFieldMask(RouteDetailLevel detailLevel)
        => detailLevel switch {
            RouteDetailLevel.Summary => _summDMFields,
            RouteDetailLevel.Standard => _stdDMFields,
            RouteDetailLevel.Full => _fullDMFields,
            _ => _summDMFields
        };
    
    public static string GetFieldMask(RouteRequest request, RouteDetailLevel detailLevel) {
        var fieldMask = GetFieldMask(detailLevel);

        if (detailLevel != RouteDetailLevel.Full && request.OptimizeWaypointOrder == true)
            fieldMask += ",routes.optimizedIntermediateWaypointIndex";
        
        return fieldMask;
    }
}