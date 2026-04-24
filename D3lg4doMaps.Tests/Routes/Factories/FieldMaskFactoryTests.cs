using DelgadoMaps.Routes.Internal.Factories;
using DelgadoMaps.Routes.Builders;
using DelgadoMaps.Routes.Enums;
using DelgadoMaps.Routes.Models.Requests;
using DelgadoMaps.Routes.Models.Requests.Common;
using FluentAssertions;

namespace DelgadoMaps.Tests.Routes.Factories;

[Trait("Module", "Routes")]
[Trait("Feature", "Factory")]
public class FieldMaskFactoryTests {
    #region HELPERS
    private static Waypoint CreateWaypoint(double lat = 1, double lng = 1, int? heading = null)
        => new WaypointBuilder()
            .FromLocation(lat, lng, heading)
            .Build();

    private static RouteRequest CreateRequest(bool optimized = false) {
        var builder = new RouteRequestBuilder()
            .From(CreateWaypoint())
            .To(CreateWaypoint())
            .AddIntermediate(CreateWaypoint());

        if (optimized) builder.OptimizeWaypointOrder();

        return builder.Build();
    }
    #endregion

    #region ROUTE DETAIL LEVEL
    [Fact]
    public void GetFieldMask_Summary_ShouldContainOnlySummaryFields() {
        var mask = FieldMaskFactory.GetFieldMask(RouteDetailLevel.Summary);

        mask.Should().Contain("routes.distanceMeters");
        mask.Should().Contain("routes.duration");
        mask.Should().Contain("routes.polyline.encodedPolyline");
        mask.Should().NotContain("routes.staticDuration");
        mask.Should().NotContain("routes.legs");
    }

    [Fact]
    public void GetFieldMask_Standard_ShouldExtendSummaryFields() {
        var summary = FieldMaskFactory.GetFieldMask(RouteDetailLevel.Summary);
        var standard = FieldMaskFactory.GetFieldMask(RouteDetailLevel.Standard);

        standard.Should().StartWith(summary);
        standard.Should().Contain("routes.staticDuration");
        standard.Should().Contain("routes.routeToken");

        standard.Should().NotContain("routes.legs");
    }

    [Fact]
    public void GetFieldMask_Full_ShouldExtendStandardFields() {
        var standard = FieldMaskFactory.GetFieldMask(RouteDetailLevel.Standard);
        var full = FieldMaskFactory.GetFieldMask(RouteDetailLevel.Full);

        full.Should().StartWith(standard);
        full.Should().Contain("routes.legs");
        full.Should().Contain("routes.travelAdvisory");
        full.Should().Contain("routes.optimizedIntermediateWaypointIndex");
    }

    [Fact]
    public void GetFieldMask_InvalidEnum_ShouldFallbackToSummary() {
        var invalid = (RouteDetailLevel)999;

        var mask = FieldMaskFactory.GetFieldMask(invalid);

        var summary = FieldMaskFactory.GetFieldMask(RouteDetailLevel.Summary);

        mask.Should().Be(summary);
    }
    #endregion

    #region DISTANCE DETAIL LEVEL
    [Fact]
    public void GetDistanceMatrixFieldMask_Summary_ShouldContainOnlySummaryFields() {
        var mask = FieldMaskFactory.GetDistanceMatrixFieldMask(RouteDetailLevel.Summary);

        mask.Should().Contain("originIndex");
        mask.Should().Contain("destinationIndex");
        mask.Should().Contain("status");
        mask.Should().Contain("condition");
        mask.Should().Contain("distanceMeters");
        mask.Should().Contain("duration");
        
        mask.Should().NotContain("staticDuration");
        mask.Should().NotContain("localizedValues");
    }

    [Fact]
    public void GetDistanceMatrixFieldMask_Standard_ShouldExtendSummaryFields() {
        var summary = FieldMaskFactory.GetDistanceMatrixFieldMask(RouteDetailLevel.Summary);
        var standard = FieldMaskFactory.GetDistanceMatrixFieldMask(RouteDetailLevel.Standard);

        standard.Should().StartWith(summary);
        standard.Should().Contain("staticDuration");
    }

    [Fact]
    public void GetDistanceMatrixFieldMask_Full_ShouldExtendStandardFields() {
        var standard = FieldMaskFactory.GetDistanceMatrixFieldMask(RouteDetailLevel.Standard);
        var full = FieldMaskFactory.GetDistanceMatrixFieldMask(RouteDetailLevel.Full);

        full.Should().StartWith(standard);
        full.Should().Contain("travelAdvisory");
        full.Should().Contain("fallbackInfo");
        full.Should().Contain("localizedValues");
    }

    [Fact]
    public void GetDistanceMatrixFieldMask_InvalidEnum_ShouldFallbackToSummary() {
        var invalid = (RouteDetailLevel)999;

        var mask = FieldMaskFactory.GetDistanceMatrixFieldMask(invalid);

        var summary = FieldMaskFactory.GetDistanceMatrixFieldMask(RouteDetailLevel.Summary);

        mask.Should().Be(summary);
    }
    #endregion

    #region REQUEST OVERLOAD
    [Fact]
    public void GetFieldMask_Request_NotOptimized_ShouldNotContainOptimizedField() {
        var mask = FieldMaskFactory.GetFieldMask(
            CreateRequest(optimized: false),
            RouteDetailLevel.Standard
        );

        mask.Should().NotContain("routes.optimizedIntermediateWaypointIndex");
    }

    [Fact]
    public void GetFieldMask_Request_Optimized_ShouldAppendOptimizedField() {
        var mask = FieldMaskFactory.GetFieldMask(
            CreateRequest(optimized: true),
            RouteDetailLevel.Summary
        );

        mask.Should().Contain("routes.optimizedIntermediateWaypointIndex");
    }

    [Fact]
    public void GetFieldMask_Request_Optimized_ShouldNotDuplicateField_WhenFull() {
        var mask = FieldMaskFactory.GetFieldMask(
            CreateRequest(optimized: true),
            RouteDetailLevel.Full
        );

        mask.Split(',')
            .Count(f => f == "routes.optimizedIntermediateWaypointIndex")
            .Should().Be(1);
    }

    [Fact]
    public void GetFieldMask_Request_ShouldPreserveBaseFields() {
        var baseMask = FieldMaskFactory.GetFieldMask(RouteDetailLevel.Summary);

        var mask = FieldMaskFactory.GetFieldMask(
            CreateRequest(true),
            RouteDetailLevel.Summary
        );

        mask.Should().StartWith(baseMask);
    }
    #endregion
}