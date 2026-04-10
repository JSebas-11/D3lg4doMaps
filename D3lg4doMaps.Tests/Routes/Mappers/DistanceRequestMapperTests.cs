using D3lg4doMaps.Routes.Internal.Mapping.Mappers;
using D3lg4doMaps.Routes.Public.Builders;
using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Requests;
using D3lg4doMaps.Routes.Public.Models.Requests.Common;
using D3lg4doMaps.Routes.Public.Models.Requests.Components;
using FluentAssertions;

namespace D3lg4doMaps.Tests.Routes.Mappers;

[Trait("Module", "Routes")]
[Trait("Feature", "Mapper")]
[Trait("Mapper", "DistanceRequest")]
public class DistanceRequestMapperTests {
    #region HELPERS
    private static Waypoint CreateWaypoint(double lat = 1, double lng = 1)
        => new WaypointBuilder().FromLocation(lat, lng).Build();

    private static RouteMatrixOrigin CreateOrigin(bool withModifiers = false)
        => new (
            CreateWaypoint(),
            withModifiers ? RouteModifiers.Create().NoTolls() : null
        );

    private static RouteMatrixDestination CreateDestination()
        => new(CreateWaypoint());

    private static DistanceRequest CreateBaseRequest()
        => new DistanceRequestBuilder()
            .AddOrigin(CreateOrigin())
            .AddDestination(CreateDestination())
            .Build();
    #endregion

    #region ROOT MAPPING
    [Fact]
    public void ToDistanceRequestDto_ShouldMapOriginsAndDestinations() {
        var request = new DistanceRequestBuilder()
            .AddOrigin(CreateOrigin())
            .AddOrigin(CreateOrigin())
            .AddDestination(CreateDestination())
            .Build();

        var dto = DistanceRequestMapper.ToDistanceRequestDto(request);

        dto.Origins.Should().HaveCount(2);
        dto.Destinations.Should().HaveCount(1);
    }

    [Fact]
    public void ToDistanceRequestDto_ShouldMapOptionalFields_WhenPresent() {
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

        var dto = DistanceRequestMapper.ToDistanceRequestDto(request);

        dto.TravelMode.Should().NotBeNull();
        dto.RoutingPreference.Should().NotBeNull();
        dto.DepartureTime.Should().Be(departure);
        dto.Units.Should().NotBeNull();
        dto.TrafficModel.Should().NotBeNull();
    }

    [Fact]
    public void ToDistanceRequestDto_ShouldLeaveOptionalFieldsNull_WhenNotProvided() {
        var request = CreateBaseRequest();

        var dto = DistanceRequestMapper.ToDistanceRequestDto(request);

        dto.TravelMode.Should().BeNull();
        dto.RoutingPreference.Should().BeNull();
        dto.Units.Should().BeNull();
        dto.TrafficModel.Should().BeNull();
        dto.TransitPreferences.Should().BeNull();
    }
    #endregion

    #region ORIGIN MAPPING
    [Fact]
    public void ToRouteMatrixOriginDto_ShouldMapWaypoint() {
        var origin = CreateOrigin();

        var dto = DistanceRequestMapper.ToRouteMatrixOriginDto(origin);

        dto.Waypoint.Should().BeEquivalentTo(origin.Waypoint);
    }

    [Fact]
    public void ToRouteMatrixOriginDto_ShouldMapRouteModifiers_WhenPresent() {
        var origin = CreateOrigin(withModifiers: true);

        var dto = DistanceRequestMapper.ToRouteMatrixOriginDto(origin);

        dto.RouteModifiers.Should().NotBeNull();
        dto.RouteModifiers!.AvoidTolls.Should().BeTrue();
    }

    [Fact]
    public void ToRouteMatrixOriginDto_ShouldLeaveRouteModifiersNull_WhenAbsent() {
        var origin = CreateOrigin(withModifiers: false);

        var dto = DistanceRequestMapper.ToRouteMatrixOriginDto(origin);

        dto.RouteModifiers.Should().BeNull();
    }
    #endregion

    #region TRANSIT PREFERENCES
    [Fact]
    public void ToTransitPreferencesDto_ShouldMapAllowedTravelModes() {
        var preferences = new TransitPreferences(
            [ TransitTravelMode.Bus, TransitTravelMode.Rail ]
        );

        var dto = DistanceRequestMapper.ToTransitPreferencesDto(preferences);

        dto.AllowedTravelModes.Should().HaveCount(2);
    }

    [Fact]
    public void ToTransitPreferencesDto_ShouldMapRoutingPreference_WhenPresent() {
        var preferences = new TransitPreferences(
            [ TransitTravelMode.Bus ], TransitRoutingPreference.LessWalking
        );

        var dto = DistanceRequestMapper.ToTransitPreferencesDto(preferences);

        dto.RoutingPreference.Should().NotBeNull();
    }

    [Fact]
    public void ToTransitPreferencesDto_ShouldLeaveRoutingPreferenceNull_WhenAbsent() {
        var preferences = new TransitPreferences( [ TransitTravelMode.Bus ] );

        var dto = DistanceRequestMapper.ToTransitPreferencesDto(preferences);

        dto.RoutingPreference.Should().BeNull();
    }
    #endregion

    #region INTEGRATION-LIKE
    [Fact]
    public void ToDistanceRequestDto_ShouldMapTransitPreferences_WhenPresent() {
        var request = new DistanceRequestBuilder()
            .AddOrigin(CreateOrigin())
            .AddDestination(CreateDestination())
            .WithTravelMode(TravelMode.Transit)
            .WithTransitPreferences(new TransitPreferences([ TransitTravelMode.Bus ]))
            .Build();

        var dto = DistanceRequestMapper.ToDistanceRequestDto(request);

        dto.TransitPreferences.Should().NotBeNull();
        dto.TransitPreferences!.AllowedTravelModes.Should().HaveCount(1);
    }
    #endregion
}