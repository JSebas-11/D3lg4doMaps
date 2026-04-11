using System.Text.Json;
using D3lg4doMaps.Routes.Internal.Extensions;
using D3lg4doMaps.Core.Public.Extensions;
using D3lg4doMaps.Core.Public.Models.Geometry;
using D3lg4doMaps.Routes.Internal.Mapping.Converters;
using D3lg4doMaps.Routes.Public.Models.Common;
using D3lg4doMaps.Routes.Public.Models.Components;
using D3lg4doMaps.Routes.Public.Models.Directions;
using D3lg4doMaps.Routes.Public.Models.Directions.Components;

namespace D3lg4doMaps.Routes.Internal.Mapping.Mappers;

internal static class DirectionsMapper {
    // -------------------- MAIN MODELS --------------------
    public static RouteResult ToRouteResult(JsonDocument json) {
        var root = json.RootElement;
        List<ComputeRoute> computeRoutes = [.. root.GetArray("routes").Select(ToComputeRoute)];

        return new RouteResult() {
            BestRoute = computeRoutes.FirstOrDefault(),
            Routes = computeRoutes.AsReadOnly()
        };
    }

    public static ComputeRoute ToComputeRoute(JsonElement json) {
        var route = new ComputeRoute() {
            DistanceMeters = json.GetInt("distanceMeters") ?? 0,
            Duration = json.GetStringValue("duration") ?? "",
            StaticDuration = json.GetStringValue("staticDuration"),
            Description = json.GetStringValue("description"),
            RouteLabels = json.GetArrayStringValues("routeLabels"),
            RouteToken = json.GetStringValue("routeToken"),
            Warnings = json.GetArrayStringValues("warnings"),
            OptimizedWaypointOrder = [.. 
                json.GetArray("optimizedIntermediateWaypointIndex").Select(s => s.GetInt32())
            ]
        };

        // VIEWPORT
        var viewport = json.GetObject("viewport");
        if (viewport is not null)
            route.Viewport = ToViewport((JsonElement)viewport);

        // LEGS
        route.Legs = [.. json.GetArray("legs").Select(ToRouteLeg)];

        // POLYLINE
        var polyline = json.GetObject("polyline");
        if (polyline is not null)
            route.Polyline = ToPolyline((JsonElement)polyline);
        
        // TRAVEL ADVISOR
        var traveladvisor = json.GetObject("travelAdvisory");
        if (traveladvisor is not null) 
            route.TravelAdvisory = ToRouteTravelAdvisory((JsonElement)traveladvisor);
        
        // LOCALIZED VALUES
        var localizedValues = json.GetObject("localizedValues");
        if (localizedValues is not null) {
            route.LocalizedValues = new RouteLocalizedValues() {
                Distance = localizedValues?.GetLocalizedValueText("distance") ?? "",
                Duration = localizedValues?.GetLocalizedValueText("duration") ?? "",
                StaticDuration = localizedValues?.GetLocalizedValueText("staticDuration"),
                TransitFare = localizedValues?.GetLocalizedValueText("transitFare")
            };
        }

        return route;
    }

    public static RouteLeg ToRouteLeg(JsonElement json) {
        var leg = new RouteLeg() {
            DistanceMeters = json.GetInt("distanceMeters") ?? 0,
            Duration = json.GetStringValue("duration") ?? "",
        };

        // LOCATION
        var startLocation = json.GetObject("startLocation");
        var endLocation = json.GetObject("endLocation");
        if (startLocation is not null)
            leg.StartLocation = ToLocation((JsonElement)startLocation);
        if (endLocation is not null)
            leg.EndLocation = ToLocation((JsonElement)endLocation);
        
        // POLYLINE
        var polyline = json.GetObject("polyline");
        if (polyline is not null)
            leg.Polyline = ToPolyline((JsonElement)polyline);
        
        // STEPS
        leg.Steps = [.. json.GetArray("steps").Select(ToRouteStep)];

        // TRAVEL ADVISOR
        var travelAdvisor = json.GetObject("travelAdvisory");
        if (travelAdvisor is not null)
            leg.TravelAdvisory = ToRouteLegTravelAdvisory((JsonElement)travelAdvisor);

        // LOCALIZED VALUES
        var localizedValues = json.GetObject("localizedValues");
        if (localizedValues is not null)
            leg.LocalizedValues = new RouteLegLocalizedValues() {
                Distance = localizedValues?.GetLocalizedValueText("distance"),
                Duration = localizedValues?.GetLocalizedValueText("duration")
            };

        // STEPS OVERVIEW
        var stepsOverview = json.GetObject("stepsOverview");
        if (stepsOverview is not null)
            leg.Overview = ToStepsOverview((JsonElement)stepsOverview);

        return leg;
    }

    public static RouteStep ToRouteStep(JsonElement json) {
        var step = new RouteStep() {
            DistanceMeters = json.GetInt("distanceMeters") ?? 0,
            StaticDuration = json.GetStringValue("staticDuration"),
            TravelMode = TravelModeConverter.FromApi(json.GetStringValue("travelMode"))
        };

        // LOCATION
        var startLocation = json.GetObject("startLocation");
        var endLocation = json.GetObject("endLocation");
        if (startLocation is not null)
            step.StartLocation = ToLocation((JsonElement)startLocation);
        if (endLocation is not null)
            step.EndLocation = ToLocation((JsonElement)endLocation);

        // POLYLINE
        var polyline = json.GetObject("polyline");
        if (polyline is not null)
            step.Polyline = ToPolyline((JsonElement)polyline);

        // NAVIGATION
        var navInstruction = json.GetObject("navigationInstruction");
        if (navInstruction is not null)
            step.NavigationInstruction = ToNavigationInstruction((JsonElement)navInstruction);

        // TRAVEL ADVISOR
        var travelAdvisor = json.GetObject("travelAdvisory");
        if (travelAdvisor is not null)
            step.TravelAdvisory = ToRouteLegTravelAdvisory((JsonElement)travelAdvisor);

        // LOCALIZED VALUES
        var localizedValues = json.GetObject("localizedValues");
        if (localizedValues is not null)
            step.LocalizedValues = new() {
                Distance = localizedValues?.GetLocalizedValueText("distance"),
                StaticDuration = localizedValues?.GetLocalizedValueText("staticDuration")
            };

        return step;
    }
    
    // -------------------- ASIDE MODELS --------------------
    // ROUTE MODELS
    public static RouteTravelAdvisory ToRouteTravelAdvisory(JsonElement json) {
        var travelAdvisory = new RouteTravelAdvisory() {
            SpeedReadingIntervals = [.. json.GetArray("speedReadingIntervals")
                .Select(ToSpeedInterval)],
            RouteRestrictionsPartiallyIgnored = json.GetBool("routeRestrictionsPartiallyIgnored")
        };

        var transitFare = json.GetObject("transitFare");
        if (transitFare is not null)
            travelAdvisory.TransitFare = ToMoney((JsonElement)transitFare);

        return travelAdvisory;
    }
    
    public static Viewport? ToViewport(JsonElement json) {
        var low = json.GetObject("low");
        var high = json.GetObject("high");

        if (low is null || high is null) return null;

        return new Viewport() {
            Low = new LatLng(
                (double)low?.GetDoubleValue("latitude")!,
                (double)low?.GetDoubleValue("longitude")!
            ),
            High = new LatLng(
                (double)high?.GetDoubleValue("latitude")!,
                (double)high?.GetDoubleValue("longitude")!
            )
        };
    }

    // LEG MODELS
    public static RouteLegTravelAdvisory ToRouteLegTravelAdvisory(JsonElement json) 
        => new () {
            SpeedReadingIntervals = [.. json.GetArray("speedReadingIntervals")
                .Select(ToSpeedInterval)]
        };
    
    public static StepsOverview ToStepsOverview(JsonElement json) 
        => new () {
            Segments = [.. json.GetArray("multiModalSegments").Select(ToSegment)]
        };
    
    public static Segment ToSegment(JsonElement json) {
        var segment = new Segment() {
            StartIndex = json.GetInt("stepStartIndex") ?? 0,
            EndIndex = json.GetInt("stepEndIndex") ?? 0,
            TravelMode = TravelModeConverter.FromApi(json.GetStringValue("travelMode"))
        };

        var navInstruction = json.GetObject("navigationInstruction");
        if (navInstruction is not null)
            segment.NavigationInstruction = ToNavigationInstruction((JsonElement)navInstruction);

        return segment;
    }
    
    // COMMON MODELS
    public static Polyline? ToPolyline(JsonElement json) {
        var encoded = json.GetStringValue("encodedPolyline");
        
        return string.IsNullOrWhiteSpace(encoded) ? null
            : new Polyline() { EncodedPolyline = encoded };
    }
    
    public static Money ToMoney(JsonElement json) {
        var hasNanos = json.TryGetProperty("nanos", out JsonElement nanos);
        
        return new () {
            CurrencyCode = json.GetStringValue("currencyCode") ?? "",
            Units = json.GetStringValue("units") ?? "",
            Nanos = hasNanos ? nanos.GetInt64() : 0
        };
    }
    
    public static SpeedInterval ToSpeedInterval(JsonElement json)
        =>  new () {
            StartPolylinePointIndex = json.GetInt("startPolylinePointIndex") ?? 0,
            EndPolylinePointIndex = json.GetInt("endPolylinePointIndex") ?? 0,
            Speed = SpeedConverter.FromApi(json.GetStringValue("speed")) 
        };
    
    public static Location? ToLocation(JsonElement json) {
        var latLng = json.GetObject("latLng");
        if (latLng is null) return null;

        return new Location() {
            LatLng = new LatLng(
                (double)latLng?.GetDoubleValue("latitude")!, 
                (double)latLng?.GetDoubleValue("longitude")! 
            ),
            Heading = json.GetInt("heading")
        };
    }
    
    public static NavigationInstruction ToNavigationInstruction(JsonElement json)
        => new () {
            Maneuver = json.GetStringValue("maneuver"),
            Instruction = json.GetStringValue("instructions")
        };
}