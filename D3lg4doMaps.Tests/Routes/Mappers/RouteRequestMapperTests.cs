using D3lg4doMaps.Routes.Internal.Mapping.Mappers;
using D3lg4doMaps.Routes.Public.Builders;
using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Requests;
using D3lg4doMaps.Routes.Public.Models.Requests.Common;
using FluentAssertions;

namespace D3lg4doMaps.Tests.Routes.Mappers;

[Trait("Module", "Routes")]
[Trait("Feature", "Mapper")]
[Trait("Mapper", "RouteRequest")]
public class RouteRequestMapperTests {
    #region HELPERS
    private static Waypoint CreateWaypoint(double lat = 1, double lng = 1)
        => new WaypointBuilder().FromLocation(lat, lng).Build();

    private static RouteRequest CreateBaseRequest()
        => new RouteRequestBuilder()
            .From(CreateWaypoint(1, 1))
            .To(CreateWaypoint(2, 2))
            .Build();
    #endregion

    #region BASIC MAPPING
    [Fact]
    public void ToRouteRequestDto_ShouldMapRequiredFields() {
        var request = CreateBaseRequest();

        var dto = RouteRequestMapper.ToRouteRequestDto(request);

        dto.Origin.Should().BeEquivalentTo(request.Origin);
        dto.Destination.Should().BeEquivalentTo(request.Destination);
        dto.Intermediates.Should().BeEquivalentTo(request.Intermediates);
    }
    #endregion

    #region ENUM MAPPING
    [Fact]
    public void ToRouteRequestDto_ShouldMapEnums_WhenPresent() {
        var request = new RouteRequestBuilder()
            .From(CreateWaypoint())
            .To(CreateWaypoint())
            .WithTravelMode(TravelMode.Drive)
            .WithRoutingPreference(RoutingPreference.TrafficAware)
            .WithUnits(Units.Metric)
            .WithPolyline(PolylineQuality.HighQuality, PolylineEncoding.EncodedPolyline)
            .Build();

        var dto = RouteRequestMapper.ToRouteRequestDto(request);

        dto.TravelMode.Should().NotBeNull();
        dto.RoutingPreference.Should().NotBeNull();
        dto.Units.Should().NotBeNull();
        dto.PolylineQuality.Should().NotBeNull();
        dto.PolylineEncoding.Should().NotBeNull();
    }

    [Fact]
    public void ToRouteRequestDto_ShouldLeaveEnumsNull_WhenNotProvided() {
        var request = CreateBaseRequest();

        var dto = RouteRequestMapper.ToRouteRequestDto(request);

        dto.TravelMode.Should().BeNull();
        dto.RoutingPreference.Should().BeNull();
        dto.Units.Should().BeNull();
        dto.PolylineQuality.Should().BeNull();
        dto.PolylineEncoding.Should().BeNull();
    }
    #endregion

    #region TIME + FLAGS
    [Fact]
    public void ToRouteRequestDto_ShouldMapTimeAndFlags() {
        var departure = DateTimeOffset.UtcNow;

        var request = new RouteRequestBuilder()
            .From(CreateWaypoint())
            .To(CreateWaypoint())
            .WithDepartureTime(departure)
            .WithAlternativeRoutes()
            .OptimizeWaypointOrder()
            .AddIntermediate(CreateWaypoint())
            .Build();

        var dto = RouteRequestMapper.ToRouteRequestDto(request);

        dto.DepartureTime.Should().Be(departure);
        dto.ComputeAlternativeRoutes.Should().BeTrue();
        dto.OptimizeWaypointOrder.Should().BeTrue();
    }
    #endregion

    #region ROUTE MODIFIERS
    [Fact]
    public void ToRouteRequestDto_ShouldMapRouteModifiers() {
        var modifiers = RouteModifiers.Create()
            .NoTolls()
            .NoHighways()
            .NoIndoor();

        var request = new RouteRequestBuilder()
            .From(CreateWaypoint())
            .To(CreateWaypoint())
            .WithRouteModifiers(modifiers)
            .Build();

        var dto = RouteRequestMapper.ToRouteRequestDto(request);

        dto.RouteModifiers.Should().NotBeNull();
        dto.RouteModifiers!.AvoidTolls.Should().BeTrue();
        dto.RouteModifiers.AvoidHighways.Should().BeTrue();
        dto.RouteModifiers.AvoidFerries.Should().BeFalse();
        dto.RouteModifiers.AvoidIndoor.Should().BeTrue();
    }

    [Fact]
    public void ToRouteRequestDto_ShouldMapVehicleInfo_WhenPresent() {
        var modifiers = RouteModifiers.Create()
            .WithVehicleInfo(new VehicleInfo(VehicleEmissionType.Electric));

        var request = new RouteRequestBuilder()
            .From(CreateWaypoint())
            .To(CreateWaypoint())
            .WithRouteModifiers(modifiers)
            .Build();

        var dto = RouteRequestMapper.ToRouteRequestDto(request);

        dto.RouteModifiers.Should().NotBeNull();
        dto.RouteModifiers!.VehicleInfo.Should().NotBeNull();
        dto.RouteModifiers.VehicleInfo!.EmissionType.Should().NotBeNull();
    }

    [Fact]
    public void ToRouteRequestDto_ShouldHandleNullVehicleInfo() {
        var modifiers = RouteModifiers.Create();

        var request = new RouteRequestBuilder()
            .From(CreateWaypoint())
            .To(CreateWaypoint())
            .WithRouteModifiers(modifiers)
            .Build();

        var dto = RouteRequestMapper.ToRouteRequestDto(request);

        dto.RouteModifiers.Should().NotBeNull();
        dto.RouteModifiers!.VehicleInfo.Should().BeNull();
    }

    [Fact]
    public void ToRouteRequestDto_ShouldHandleNullRouteModifiers() {
        var request = CreateBaseRequest();

        var dto = RouteRequestMapper.ToRouteRequestDto(request);

        dto.RouteModifiers.Should().BeNull();
    }
    #endregion

    #region ISOLATED MAPPERS
    [Fact]
    public void ToRouteModifiersDto_ShouldMapAllFields() {
        var modifiers = RouteModifiers.Create()
            .NoTolls()
            .NoFerries();

        var dto = RouteRequestMapper.ToRouteModifiersDto(modifiers);

        dto.AvoidTolls.Should().BeTrue();
        dto.AvoidHighways.Should().BeFalse();
        dto.AvoidFerries.Should().BeTrue();
        dto.AvoidIndoor.Should().BeFalse();
    }

    [Fact]
    public void ToVehicleInfoDto_ShouldMapEmissionType_WhenPresent() {
        var vehicle = new VehicleInfo(VehicleEmissionType.Electric);

        var dto = RouteRequestMapper.ToVehicleInfoDto(vehicle);

        dto.EmissionType.Should().NotBeNull();
    }
    #endregion
}