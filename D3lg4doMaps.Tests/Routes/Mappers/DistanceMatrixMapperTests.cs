using System.Text.Json;
using D3lg4doMaps.Routes.Internal.Mapping.Mappers;
using D3lg4doMaps.Routes.Public.Enums;
using FluentAssertions;

namespace D3lg4doMaps.Tests.Routes.Mappers;

[Trait("Module", "Routes")]
[Trait("Feature", "Mapper")]
[Trait("Mapper", "DistanceMatrix")]
public class DistanceMatrixMapperTests {
    #region BASIC
    [Fact]
    public void ToRouteMatrixElement_ShouldMapBasicFields() {
        var json = JsonDocument.Parse("""
        {
            "condition": "ROUTE_EXISTS",
            "distanceMeters": 1500,
            "duration": "120s",
            "staticDuration": "100s",
            "originIndex": 1,
            "destinationIndex": 2
        }
        """);

        var result = DistanceMatrixMapper.ToRouteMatrixElement(json);

        result.Condition.Should().Be(RouteElementCondition.RouteExists);
        result.DistanceMeters.Should().Be(1500);
        result.Duration.Should().Be("120s");
        result.StaticDuration.Should().Be("100s");
        result.OriginIndex.Should().Be(1);
        result.DestinationIndex.Should().Be(2);
    }
    #endregion

    #region STATUS
    [Fact]
    public void ToRouteMatrixElement_ShouldMapStatus_WhenPresent() {
        var json = JsonDocument.Parse("""
        {
            "condition": "ROUTE_EXISTS",
            "originIndex": 0,
            "destinationIndex": 0,
            "status": {
                "code": 404,
                "message": "Not found"
            }
        }
        """);

        var result = DistanceMatrixMapper.ToRouteMatrixElement(json);

        result.Status.Should().NotBeNull();
        result.Status!.Code.Should().Be(404);
        result.Status.Message.Should().Be("Not found");
    }
    #endregion

    #region FALLBACK
    [Fact]
    public void ToRouteMatrixElement_ShouldMapFallbackInfo_WhenPresent() {
        var json = JsonDocument.Parse("""
        {
            "condition": "ROUTE_EXISTS",
            "originIndex": 0,
            "destinationIndex": 0,
            "fallbackInfo": {
                "routingMode": "FALLBACK_TRAFFIC_UNAWARE",
                "reason": "LATENCY_EXCEEDED"
            }
        }
        """);

        var result = DistanceMatrixMapper.ToRouteMatrixElement(json);

        result.FallbackInfo.Should().NotBeNull();
        result.FallbackInfo!.RoutingMode.Should().Be(RoutingMode.FallbackTrafficUnaware);
        result.FallbackInfo.Reason.Should().Be(Reason.LatencyExceeded);
    }
    #endregion

    #region LOCALIZED VALUES
    [Fact]
    public void ToRouteMatrixElement_ShouldMapLocalizedValues() {
        var json = JsonDocument.Parse("""
        {
            "condition": "ROUTE_EXISTS",
            "originIndex": 0,
            "destinationIndex": 0,
            "localizedValues": {
                "distance": { "text": "1.5 km" },
                "duration": { "text": "2 mins" },
                "staticDuration": { "text": "1.5 mins" }
            }
        }
        """);

        var result = DistanceMatrixMapper.ToRouteMatrixElement(json);

        result.LocalizedValues.Should().NotBeNull();
        result.LocalizedValues!.Distance.Should().Be("1.5 km");
        result.LocalizedValues.Duration.Should().Be("2 mins");
        result.LocalizedValues.StaticDuration.Should().Be("1.5 mins");
    }
    #endregion

    #region TRAVEL ADVISORY
    [Fact]
    public void ToRouteMatrixElement_ShouldMapTravelAdvisory_WhenPresent() {
        var json = JsonDocument.Parse("""
        {
            "condition": "ROUTE_EXISTS",
            "originIndex": 0,
            "destinationIndex": 0,
            "travelAdvisory": {
                "routeRestrictionsPartiallyIgnored": true,
                "speedReadingIntervals": [
                    {
                        "startPolylinePointIndex": 0,
                        "endPolylinePointIndex": 5,
                        "speed": "NORMAL"
                    }
                ]
            }
        }
        """);

        var result = DistanceMatrixMapper.ToRouteMatrixElement(json);

        result.TravelAdvisory.Should().NotBeNull();
        result.TravelAdvisory!.SpeedReadingIntervals.Should().HaveCount(1);
    }
    #endregion

    #region MINIMAL JSON
    [Fact]
    public void ToRouteMatrixElement_ShouldHandleMissingOptionalFields() {
        var json = JsonDocument.Parse("""
        {
            "originIndex": 0,
            "destinationIndex": 0
        }
        """);

        var result = DistanceMatrixMapper.ToRouteMatrixElement(json);

        result.Should().NotBeNull();
        result.DistanceMeters.Should().BeNull();
        result.Duration.Should().BeNull();
        result.Status.Should().BeNull();
        result.FallbackInfo.Should().BeNull();
        result.LocalizedValues.Should().BeNull();
    }
    #endregion
}