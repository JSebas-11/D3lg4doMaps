using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Routes.Public.Builders;
using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Requests.Common;
using D3lg4doMaps.Routes.Public.Models.Requests.Components;
using FluentAssertions;

namespace D3lg4doMaps.Tests.Routes.Builders;

[Trait("Module", "Routes")]
[Trait("Feature", "Builder")]
[Trait("Builder", "DistanceRequest")]
public class DistanceRequestBuilderTests {
    #region HELPERS
    private static Waypoint CreateWaypoint(double lat = 1, double lng = 1)
        => new WaypointBuilder().FromLocation(lat, lng).Build();

    private static RouteMatrixOrigin CreateOrigin()
        => new(CreateWaypoint());

    private static RouteMatrixDestination CreateDestination()
        => new(CreateWaypoint());
    #endregion

    #region SUCCESS
    [Fact]
    public void Build_ShouldCreateRequest_WhenValid() {
        var request = new DistanceRequestBuilder()
            .AddOrigin(CreateOrigin())
            .AddDestination(CreateDestination())
            .Build();

        request.Origins.Should().HaveCount(1);
        request.Destinations.Should().HaveCount(1);
    }

    [Fact]
    public void Build_ShouldMapOptionalFields() {
        var departure = DateTimeOffset.UtcNow;

        var request = new DistanceRequestBuilder()
            .AddOrigin(CreateOrigin())
            .AddDestination(CreateDestination())
            .WithTravelMode(TravelMode.Drive)
            .WithRoutingPreference(RoutingPreference.TrafficAware)
            .WithDepartureTime(departure)
            .WithUnits(Units.Metric)
            .WithTrafficModel(TrafficModel.BestGuess)
            .Build();

        request.TravelMode.Should().Be(TravelMode.Drive);
        request.RoutingPreference.Should().Be(RoutingPreference.TrafficAware);
        request.DepartureTime.Should().Be(departure);
        request.Units.Should().Be(Units.Metric);
        request.TrafficModel.Should().Be(TrafficModel.BestGuess);
    }
    #endregion

    #region VALIDATIONS
    [Fact]
    public void Build_ShouldThrow_WhenOriginsMissing() {
        var builder = new DistanceRequestBuilder()
            .AddDestination(CreateDestination());

        var act = () => builder.Build();

        act.Should().Throw<MapsInvalidRequestException>()
            .WithMessage("*Origin*");
    }

    [Fact]
    public void Build_ShouldThrow_WhenDestinationsMissing() {
        var builder = new DistanceRequestBuilder()
            .AddOrigin(CreateOrigin());

        var act = () => builder.Build();

        act.Should().Throw<MapsInvalidRequestException>()
            .WithMessage("*Destination*");
    }

    [Fact]
    public void Build_ShouldThrow_WhenMatrixTooLarge() {
        var builder = new DistanceRequestBuilder();

        for (int i = 0; i < 11; i++)
            builder.AddOrigin(CreateOrigin());

        for (int i = 0; i < 10; i++)
            builder.AddDestination(CreateDestination());

        var act = () => builder.Build();
        act.Should().Throw<MapsInvalidRequestException>()
            .WithMessage("*maximum 100*");
    }

    [Fact]
    public void Build_ShouldThrow_WhenArrivalAndDepartureSet() {
        var builder = new DistanceRequestBuilder()
            .AddOrigin(CreateOrigin())
            .AddDestination(CreateDestination())
            .WithDepartureTime(DateTimeOffset.UtcNow)
            .WithArrivalTime(DateTimeOffset.UtcNow.AddHours(1));

        var act = () => builder.Build();

        act.Should().Throw<MapsInvalidRequestException>()
            .WithMessage("*Only one*");
    }
    #endregion

    #region ENUM VALIDATION
    [Fact]
    public void WithTravelMode_ShouldThrow_WhenUnknown() {
        var builder = new DistanceRequestBuilder();

        var act = () => builder.WithTravelMode((TravelMode)999).Build();

        act.Should().Throw<MapsInvalidRequestException>();
    }
    #endregion

    #region COLLECTION BUILDERS
    [Fact]
    public void WithOrigins_ShouldReplaceExistingOrigins() {
        var builder = new DistanceRequestBuilder()
            .AddOrigin(CreateOrigin());

        builder.WithOrigins([CreateOrigin(), CreateOrigin()]);

        var request = builder
            .AddDestination(CreateDestination())
            .Build();

        request.Origins.Should().HaveCount(2);
    }

    [Fact]
    public void WithDestinations_ShouldReplaceExistingDestinations() {
        var builder = new DistanceRequestBuilder()
            .AddDestination(CreateDestination());

        builder.WithDestinations([CreateDestination(), CreateDestination()]);

        var request = builder
            .AddOrigin(CreateOrigin())
            .Build();

        request.Destinations.Should().HaveCount(2);
    }
    #endregion

    #region OVERLOADS
    [Fact]
    public void AddOrigin_WithWaypoint_ShouldCreateOrigin() {
        var waypoint = CreateWaypoint();

        var request = new DistanceRequestBuilder()
            .AddOrigin(waypoint)
            .AddDestination(CreateDestination())
            .Build();

        request.Origins.Should().HaveCount(1);
    }

    [Fact]
    public void AddDestination_WithWaypoint_ShouldCreateDestination() {
        var waypoint = CreateWaypoint();

        var request = new DistanceRequestBuilder()
            .AddOrigin(CreateOrigin())
            .AddDestination(waypoint)
            .Build();

        request.Destinations.Should().HaveCount(1);
    }
    #endregion
}