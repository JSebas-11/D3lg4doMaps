using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Routes.Builders;
using DelgadoMaps.Routes.Enums;
using DelgadoMaps.Routes.Models.Requests.Common;
using FluentAssertions;

namespace DelgadoMaps.Tests.Routes.Builders;

[Trait("Module", "Routes")]
[Trait("Feature", "Builder")]
[Trait("Builder", "RouteRequest")]
public class RouteRequestBuilderTests {
    #region  HELPERS
    private static Waypoint CreateWaypoint(double lat = 1, double lng = 1, int? heading = null)
        => new WaypointBuilder()
            .FromLocation(lat, lng, heading)
            .Build();
    #endregion

    #region SUCCESS
    [Fact]
    public void Build_ShouldCreateValidRequest() {
        var request = new RouteRequestBuilder()
            .From(CreateWaypoint())
            .To(CreateWaypoint())
            .Build();

        request.Should().NotBeNull();
        request.Origin.Should().NotBeNull();
        request.Destination.Should().NotBeNull();
    }

    [Fact]
    public void Build_ShouldSetOptionalValues() {
        var departure = DateTimeOffset.UtcNow;

        var request = new RouteRequestBuilder()
            .From(CreateWaypoint())
            .To(CreateWaypoint())
            .WithDepartureTime(departure)
            .WithAlternativeRoutes()
            .Build();

        request.DepartureTime.Should().Be(departure);
        request.ComputeAlternativeRoutes.Should().BeTrue();
    }

    [Fact]
    public void Build_ShouldAddIntermediates() {
        var request = new RouteRequestBuilder()
            .From(CreateWaypoint())
            .To(CreateWaypoint())
            .AddIntermediate(CreateWaypoint(2, 2))
            .AddIntermediate(CreateWaypoint(3, 3))
            .Build();

        request.Intermediates.Should().HaveCount(2);
    }
    #endregion

    #region VALIDATIONS
    [Fact]
    public void Build_ShouldThrow_WhenOriginMissing() {
        var act = () => new RouteRequestBuilder()
            .To(CreateWaypoint())
            .Build();

        act.Should().Throw<MapsInvalidRequestException>();
    }

    [Fact]
    public void Build_ShouldThrow_WhenDestinationMissing() {
        var act = () => new RouteRequestBuilder()
            .From(CreateWaypoint())
            .Build();

        act.Should().Throw<MapsInvalidRequestException>();
    }

    [Fact]
    public void Build_ShouldThrow_WhenBothArrivalAndDepartureSet() {
        var act = () => new RouteRequestBuilder()
            .From(CreateWaypoint())
            .To(CreateWaypoint())
            .WithDepartureTime(DateTimeOffset.UtcNow)
            .WithArrivalTime(DateTimeOffset.UtcNow.AddHours(1))
            .Build();

        act.Should().Throw<MapsInvalidRequestException>()
            .WithMessage("*Only one of ArrivalTime or DepartureTime*");
    }

    [Fact]
    public void Build_ShouldThrow_WhenOptimizeWithoutIntermediates() {
        var act = () => new RouteRequestBuilder()
            .From(CreateWaypoint())
            .To(CreateWaypoint())
            .OptimizeWaypointOrder()
            .Build();

        act.Should().Throw<MapsInvalidRequestException>()
            .WithMessage("*OptimizeWaypointOrder requires providing Intermediates*");
    }

    [Fact]
    public void Build_ShouldThrow_WhenTooManyIntermediates() {
        var builder = new RouteRequestBuilder()
            .From(CreateWaypoint())
            .To(CreateWaypoint());

        for (int i = 0; i < 26; i++)
            builder.AddIntermediate(CreateWaypoint());

        var act = () => builder.Build();

        act.Should().Throw<MapsInvalidRequestException>()
            .WithMessage("*Maximum 25 Intermediates*");
    }

    [Fact]
    public void Build_ShouldThrow_WhenHeadingUsedWithInvalidTravelMode() {
        var waypointWithHeading = CreateWaypoint(1, 1, heading: 90);

        var act = () => new RouteRequestBuilder()
            .From(waypointWithHeading)
            .To(CreateWaypoint())
            .WithTravelMode(TravelMode.Walk)
            .Build();

        act.Should().Throw<MapsInvalidRequestException>()
            .WithMessage("*Waypoint heading is only supported*");
    }
    #endregion

    # region ENUM VALIDATION
    [Fact]
    public void WithEnumUnkwnown_ShouldThrow() {
        var act = () => new RouteRequestBuilder()
            .WithTravelMode(TravelMode.Unknown);

        act.Should().Throw<MapsInvalidRequestException>();
    }
    #endregion

    # region EDGE CASES
    [Fact]
    public void Build_ShouldAllowHeading_WithDriveMode() {
        var waypointWithHeading = CreateWaypoint(1, 1, heading: 90);

        var request = new RouteRequestBuilder()
            .From(waypointWithHeading)
            .To(CreateWaypoint())
            .WithTravelMode(TravelMode.Drive)
            .Build();

        request.Should().NotBeNull();
    }

    [Fact]
    public void Build_ShouldAllowHeading_WithTwoWheelerMode() {
        var waypointWithHeading = CreateWaypoint(1, 1, heading: 90);

        var request = new RouteRequestBuilder()
            .From(waypointWithHeading)
            .To(CreateWaypoint())
            .WithTravelMode(TravelMode.TwoWheeler)
            .Build();

        request.Should().NotBeNull();
    }
    #endregion
}