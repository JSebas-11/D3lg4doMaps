using D3lg4doMaps.Core.Public.Abstractions;
using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Core.Public.Models;
using D3lg4doMaps.Places.Internal.Constants;
using D3lg4doMaps.Places.Internal.Models.Responses;
using D3lg4doMaps.Places.Internal.Services;
using D3lg4doMaps.Places.Public.Models.Requests;
using FluentAssertions;
using NSubstitute;

namespace D3lg4doMaps.Tests.Places.Services;

[Trait("Module", "Places")]
[Trait("Feature", "Service")]
[Trait("Service", "Search")]
public class SearchServiceTests {
    // -------------------- INNER PROPS --------------------
    private readonly IMapsApiClient _apiClient;
    private readonly SearchService _service;

    public SearchServiceTests() {
        _apiClient = Substitute.For<IMapsApiClient>();
        _service = new SearchService(_apiClient);
    }

    // -------------------- TESTS --------------------
    [Fact]
    public async Task SearchAsync_ShouldReturnMappedResults() {
        var apiResponse = new PlacesSearchResponse {
            Places = [
                new () {
                    Id = "123",
                    DisplayName = new() { Text = "coffee", LanguageCode = "en" },
                    Types = [ "coffee" ]
                }
            ]
        };

        _apiClient.SendAsync<PlacesSearchResponse>(Arg.Any<MapsApiRequest>())
            .Returns(apiResponse);
        
        var result = await _service.SearchByTextAsync("coffee shop");

        result.Should().HaveCount(1);
        result[0].PlaceId.Should().Be("123");
        result[0].DisplayName.Should().Be("coffee");
    }

    [Fact]
    public async Task SearchAsync_ShouldReturnEmpty() {
        var apiResponse = new PlacesSearchResponse { Places = [] };

        _apiClient.SendAsync<PlacesSearchResponse>(Arg.Any<MapsApiRequest>())
            .Returns(apiResponse);

        var result = await _service.SearchByTextAsync("asdas11#");

        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task SearchAsync_ShouldIgnoreNullResults() {
        var apiResponse = new PlacesSearchResponse {
            Places = [
                null,
                new () {
                    Id = "123",
                    DisplayName = new() { Text = "coffee", LanguageCode = "en" },
                    Types = [ "coffee" ]
                },
                null
            ]
        };

        _apiClient.SendAsync<PlacesSearchResponse>(Arg.Any<MapsApiRequest>())
            .Returns(apiResponse);

        var result = await _service.SearchByTextAsync("coffee");

        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task SearchByTextAsync_ThrowMapsInvalidRequestException() {
        var apiResponse = new PlacesSearchResponse { Places = [] };

        _apiClient.SendAsync<PlacesSearchResponse>(Arg.Any<MapsApiRequest>())
            .Returns(apiResponse);

        var act = async () => await _service.SearchByTextAsync("");

        await act.Should().ThrowAsync<MapsInvalidRequestException>();
    }

    [Fact]
    public async Task SearchByTextAsync_ShouldBuildCorrectRequest() {
        MapsApiRequest? captured = null;

        var apiResponse = new PlacesSearchResponse { Places = [] };

        _apiClient.SendAsync<PlacesSearchResponse>(Arg.Any<MapsApiRequest>())
            .Returns(call => {
                captured = call.Arg<MapsApiRequest>();
                return apiResponse;
            });

        var textQuery = "coffee";
        await _service.SearchByTextAsync(textQuery);

        captured.Should().NotBeNull();

        captured.Method.Should().Be(HttpMethod.Post);
        captured.BaseUrl.Should().Be(PlacesEndpoints.BaseUrl);
        captured.Endpoint.Should().Be(PlacesEndpoints.SearchText);

        captured.Headers.Should().HaveCount(2);
        captured.Headers["Content-Type"]
            .Should().Be("application/json");
        captured.Headers["X-Goog-FieldMask"].Should().
            Be("places.id,places.displayName,places.types");

        captured.Payload.Should().BeEquivalentTo(new { textQuery = "coffee" });
    }
    
    [Fact]
    public async Task SearchByNearbyAsync_ShouldBuildCorrectRequest() {
        MapsApiRequest? captured = null;

        var apiResponse = new PlacesSearchResponse { Places = [] };

        _apiClient.SendAsync<PlacesSearchResponse>(Arg.Any<MapsApiRequest>())
            .Returns(call => {
                captured = call.Arg<MapsApiRequest>();
                return apiResponse;
            });

        var request = new NearbyRequest() {
            IncludedTypes = [ "coffee" ],
            MaxResultCount = 1,
            LocationRestriction = new () {
                Circle = new () {
                    Radius = 10, Center = new (12.1, 23.1)
                }
            }
        };
        await _service.SearchByNearbyAsync(request);

        captured.Should().NotBeNull();

        captured.Method.Should().Be(HttpMethod.Post);
        captured.BaseUrl.Should().Be(PlacesEndpoints.BaseUrl);
        captured.Endpoint.Should().Be(PlacesEndpoints.SearchNearby);

        captured.Headers.Should().HaveCount(2);
        captured.Headers["Content-Type"]
            .Should().Be("application/json");
        captured.Headers["X-Goog-FieldMask"].Should().
            Be("places.id,places.displayName,places.types");

        captured.Payload.Should().BeEquivalentTo(request);
    }
}