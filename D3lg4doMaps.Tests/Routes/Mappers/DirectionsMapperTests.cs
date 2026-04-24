using System.Text.Json;
using DelgadoMaps.Routes.Internal.Mapping.Mappers;
using FluentAssertions;

namespace DelgadoMaps.Tests.Routes.Mappers;

[Trait("Module", "Routes")]
[Trait("Feature", "Mapper")]
[Trait("Mapper", "Directions")]
public class DirectionsMapperTests {
    #region MAIN
    [Fact]
    public void ToRouteResult_ShouldMapRoutes_AndSetBestRoute() {
        var json = JsonDocument.Parse("""
        {
            "routes": [
                { "distanceMeters": 100, "duration": "10s" },
                { "distanceMeters": 200, "duration": "20s" }
            ]
        }
        """);

        var result = DirectionsMapper.ToRouteResult(json);

        result.Routes.Should().HaveCount(2);
        result.BestRoute.Should().NotBeNull();
        result.BestRoute!.DistanceMeters.Should().Be(100);
    }

    [Fact]
    public void ToComputeRoute_ShouldMapBasicFields() {
        var json = JsonDocument.Parse("""
        {
            "distanceMeters": 150,
            "duration": "15s",
            "staticDuration": "20s",
            "description": "Test route",
            "routeLabels": ["A", "B"],
            "warnings": ["warn1"],
            "routeToken": "abc123",
            "optimizedIntermediateWaypointIndex": [1,2]
        }
        """).RootElement;

        var route = DirectionsMapper.ToComputeRoute(json);

        route.DistanceMeters.Should().Be(150);
        route.Duration.Should().Be("15s");
        route.StaticDuration.Should().Be("20s");
        route.Description.Should().Be("Test route");
        route.RouteLabels.Should().Contain("A");
        route.Warnings.Should().Contain("warn1");
        route.RouteToken.Should().Be("abc123");
        route.OptimizedWaypointOrder.Should().BeEquivalentTo([1,2]);
    }

    [Fact]
    public void ToComputeRoute_ShouldHandleMissingFields_Gracefully() {
        var json = JsonDocument.Parse("{}").RootElement;

        var route = DirectionsMapper.ToComputeRoute(json);

        route.DistanceMeters.Should().Be(0);
        route.Duration.Should().Be("");
        route.RouteLabels.Should().BeEmpty();
        route.Legs.Should().BeEmpty();
    }
    #endregion

    #region ASIDE
    [Fact]
    public void ToRouteLeg_ShouldMapBasicFields_AndSteps() {
        var json = JsonDocument.Parse("""
        {
            "distanceMeters": 50,
            "duration": "5s",
            "steps": [
                { "distanceMeters": 10, "travelMode": "DRIVE" }
            ]
        }
        """).RootElement;

        var leg = DirectionsMapper.ToRouteLeg(json);

        leg.DistanceMeters.Should().Be(50);
        leg.Duration.Should().Be("5s");

        leg.Steps.Should().HaveCount(1);
        leg.Steps[0].DistanceMeters.Should().Be(10);
    }

    [Fact]
    public void ToRouteStep_ShouldMapNavigationInstruction() {
        var json = JsonDocument.Parse("""
        {
            "distanceMeters": 10,
            "travelMode": "DRIVE",
            "navigationInstruction": {
                "maneuver": "TURN_LEFT",
                "instructions": "Turn left"
            }
        }
        """).RootElement;

        var step = DirectionsMapper.ToRouteStep(json);

        step.NavigationInstruction.Should().NotBeNull();
        step.NavigationInstruction!.Maneuver.Should().Be("TURN_LEFT");
    }
    #endregion

    #region NESTED
    [Fact]
    public void ToComputeRoute_ShouldMapPolyline_AndViewport() {
        var json = JsonDocument.Parse("""
        {
            "polyline": { "encodedPolyline": "abc" },
            "viewport": {
                "low": { "latitude": 1, "longitude": 2 },
                "high": { "latitude": 3, "longitude": 4 }
            }
        }
        """).RootElement;

        var route = DirectionsMapper.ToComputeRoute(json);

        route.Polyline.Should().NotBeNull();
        route.Polyline!.EncodedPolyline.Should().Be("abc");

        route.Viewport.Should().NotBeNull();
        route.Viewport!.Low.Latitude.Should().Be(1);
        route.Viewport.High.Longitude.Should().Be(4);
    }

    [Fact]
    public void ToPolyline_ShouldReturnNull_WhenEmpty() {
        var json = JsonDocument.Parse("""
        { "encodedPolyline": "" }
        """).RootElement;

        var result = DirectionsMapper.ToPolyline(json);

        result.Should().BeNull();
    }

    [Fact]
    public void ToPolyline_ShouldReturnValue_WhenValid() {
        var json = JsonDocument.Parse("""
        { "encodedPolyline": "abc" }
        """).RootElement;

        var result = DirectionsMapper.ToPolyline(json);

        result.Should().NotBeNull();
        result!.EncodedPolyline.Should().Be("abc");
    }

    [Fact]
    public void ToLocation_ShouldMapLatLng_AndHeading() {
        var json = JsonDocument.Parse("""
        {
            "latLng": { "latitude": 1.5, "longitude": 2.5 },
            "heading": 90
        }
        """).RootElement;

        var loc = DirectionsMapper.ToLocation(json);

        loc.Should().NotBeNull();
        loc!.LatLng.Latitude.Should().Be(1.5);
        loc.Heading.Should().Be(90);
    }

    [Fact]
    public void ToLocation_ShouldReturnNull_WhenLatLngMissing() {
        var json = JsonDocument.Parse("{}").RootElement;

        var loc = DirectionsMapper.ToLocation(json);

        loc.Should().BeNull();
    }

    [Fact]
    public void ToMoney_ShouldMapAllFields() {
        var json = JsonDocument.Parse("""
        {
            "currencyCode": "USD",
            "units": "10",
            "nanos": 500000000
        }
        """).RootElement;

        var money = DirectionsMapper.ToMoney(json);

        money.CurrencyCode.Should().Be("USD");
        money.Units.Should().Be("10");
        money.Nanos.Should().Be(500000000);
    }
    #endregion
}