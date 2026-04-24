using System.Text.Json;
using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Enums;
using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Core.Extensions;
using DelgadoMaps.Core.Models;
using DelgadoMaps.Places.Internal.Constants;
using DelgadoMaps.Places.Internal.Services;
using FluentAssertions;
using NSubstitute;

namespace DelgadoMaps.Tests.Places.Services;

[Trait("Module", "Places")]
[Trait("Feature", "Service")]
[Trait("Service", "Details")]
public class DetailsServiceTests {
    // -------------------- INNER PROPS --------------------
    private readonly IMapsApiClient _apiClient;
    private readonly DetailsService _service;

    public DetailsServiceTests() {
        _apiClient = Substitute.For<IMapsApiClient>();
        _service = new DetailsService(_apiClient);
    }

    // -------------------- TESTS --------------------
    #region DETAILS
    
    [Fact]
    public async Task GetDetailsAsync_ShouldReturnMappedDetails() {
        var json = JsonDocument.Parse("""
        {
            "displayName": { "text": "Test" },
            "plusCode": { "globalCode": "t-11" }
        }
        """);

        _apiClient.SendAsync<JsonDocument>(Arg.Any<MapsApiRequest>())
            .Returns(json);

        var details = await _service.GetDetailsAsync("123");

        details.PlaceId.Should().Be("123");
        details.DisplayName.Should().Be("Test");
        details.GlobalCode.Should().Be("t-11");
    }
    
    [Fact]
    public async Task GetDetailsRawAsync_ShouldReturnJsonDetails() {
        var json = JsonDocument.Parse("""
        {
            "displayName": { "text": "Test" },
            "rating": 5.0,
            "userRatingCount": 2057,
            "plusCode": { "globalCode": "t-11" }
        }
        """);

        _apiClient.SendAsync<JsonDocument>(Arg.Any<MapsApiRequest>())
            .Returns(json);

        string[] fields = [ "displayName", "rating", "userRatingCount", "plusCode" ];
        var details = await _service.GetDetailsRawAsync("123", fields);

        details.Should().NotBeNull();

        var root = details.RootElement;
        root.GetObject("displayName")?.GetStringValue("text")
            .Should().Be("Test");
        root.GetObject("plusCode")?.GetStringValue("globalCode")
            .Should().Be("t-11");
        root.GetFloat("rating").Should().Be(5.0f);
        root.GetInt("userRatingCount").Should().Be(2057);
    }

    [Fact]
    public async Task GetDetailsRawAsync_ShouldThrow_WhenPlaceIdIsEmpty() {
        var act = async () => await _service.GetDetailsRawAsync("", "displayName");
        await act.Should().ThrowAsync<MapsInvalidRequestException>();
    }

    [Fact]
    public async Task GetDetailsRawAsync_ShouldThrow_WhenFieldsAreEmpty() {
        var act = async () => await _service.GetDetailsRawAsync("123");
        await act.Should().ThrowAsync<MapsInvalidRequestException>();
    }

    [Fact]
    public async Task GetDetailsRawAsync_ShouldBuildCorrectRequest() {
        MapsApiRequest? captured = null;

        _apiClient.SendAsync<JsonDocument>(Arg.Any<MapsApiRequest>())
            .Returns(call => {
                captured = call.Arg<MapsApiRequest>();
                return JsonDocument.Parse("{}");
            });

        var fields = new[] { "displayName", "rating" };

        await _service.GetDetailsRawAsync("123", fields);

        captured.Should().NotBeNull();

        captured!.Method.Should().Be(HttpMethod.Get);
        captured.BaseUrl.Should().Be(PlacesEndpoints.BaseUrl);
        captured.Endpoint.Should().Contain("/123");

        captured.Headers.Should().ContainKey("X-Goog-FieldMask");
        captured.Headers["X-Goog-FieldMask"]
            .Should().Be("displayName,rating");

        captured.ApiKeyLocation.Should().Be(ApiKeyLocation.Header);
    }

    #endregion

    #region REVIEWS

    [Fact]
    public async Task GetReviewsAsync_ShouldReturnMappedReviews() {
        var json = JsonDocument.Parse("""
        {
            "reviews": [
                {
                    "rating": 5,
                    "text": { "text": "Great place!" }
                }
            ]
        }
        """);

        _apiClient.SendAsync<JsonDocument>(Arg.Any<MapsApiRequest>())
            .Returns(json);

        var result = await _service.GetReviewsAsync("123");

        result.Should().NotBeNull();
        result.Reviews.Should().HaveCount(1);

        result.Reviews[0].Rating.Should().Be(5);
        result.Reviews[0].Text.Should().Be("Great place!");
    }
    #endregion

    #region PHOTOS
    
    [Fact]
    public async Task GetPhotosAsync_ShouldReturnEmpty_WhenNoPhotos() {
        var json = JsonDocument.Parse("""
        {
            "photos": []
        }
        """);

        _apiClient.SendAsync<JsonDocument>(Arg.Any<MapsApiRequest>())
            .Returns(json);

        var result = await _service.GetPhotosAsync("123");

        result.Should().BeEmpty();
    }

    #endregion
}