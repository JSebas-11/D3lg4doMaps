using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Models;
using DelgadoMaps.Core.Models.Geometry;
using DelgadoMaps.Places.Internal.Constants;
using DelgadoMaps.Places.Internal.Models.DTOs;
using DelgadoMaps.Places.Internal.Models.Responses;
using DelgadoMaps.Places.Internal.Services;
using DelgadoMaps.Places.Models.Geometry;
using DelgadoMaps.Places.Models.Requests;
using FluentAssertions;
using NSubstitute;

namespace DelgadoMaps.Tests.Places.Services;

[Trait("Module", "Places")]
[Trait("Feature", "Service")]
[Trait("Service", "Autocomplete")]
public class AutocompleteServiceTests {
    // -------------------- INNER PROPS --------------------
    private readonly IMapsApiClient _apiClient;
    private readonly AutocompleteService _service;

    public AutocompleteServiceTests() {
        _apiClient = Substitute.For<IMapsApiClient>();
        _service = new AutocompleteService(_apiClient);
    }

    // -------------------- TESTS --------------------
    [Fact]
    public async Task SuggestPlacesAsync_ShouldReturnMappedSuggestions() {
        var apiResponse = new AutocompleteResponse {
            Suggestions = [
                new() {
                    PlacePrediction = new PlaceSuggestionDto {
                        PlaceId = "123",
                        Text = new() { Text = "Coffee Shop"},
                        Types = [ "coffee", "shop" ]
                    }
                }
            ]
        };

        _apiClient.SendAsync<AutocompleteResponse>(Arg.Any<MapsApiRequest>())
            .Returns(apiResponse);

        var request = new AutocompleteRequest() { Input = "coffee" };

        var result = await _service.SuggestPlacesAsync(request);

        result.Should().HaveCount(1);
        result[0].PlaceId.Should().Be("123");
        result[0].Text.Should().Be("Coffee Shop");
    }
    
    [Fact]
    public async Task SuggestPlacesAsync_ShouldReturnEmpty() {
        var apiResponse = new AutocompleteResponse { Suggestions = [] };

        _apiClient.SendAsync<AutocompleteResponse>(Arg.Any<MapsApiRequest>())
            .Returns(apiResponse);

        var request = new AutocompleteRequest() { Input = "assd121#" };

        var result = await _service.SuggestPlacesAsync(request);

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task SuggestPlacesAsync_ShouldIgnoreNullPredictions() {
        var apiResponse = new AutocompleteResponse {
            Suggestions = [
                new() { PlacePrediction = null },
                new() {
                    PlacePrediction = new PlaceSuggestionDto {
                        PlaceId = "123",
                        Text = new() { Text = "coffee" }
                    },
                },
                new() { PlacePrediction = null }
            ]
        };

        _apiClient.SendAsync<AutocompleteResponse>(Arg.Any<MapsApiRequest>())
            .Returns(apiResponse);

        var result = await _service.SuggestPlacesAsync(new AutocompleteRequest());

        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task SuggestPlacesAsync_ShouldBuildCorrectRequest() {
        MapsApiRequest? captured = null;

        var apiResponse = new AutocompleteResponse { Suggestions = [] };

        _apiClient.SendAsync<AutocompleteResponse>(Arg.Any<MapsApiRequest>())
            .Returns(call => {
                captured = call.Arg<MapsApiRequest>();
                return apiResponse;
            });

        var request = new AutocompleteRequest { 
            Input = "coffee", 
            LocationBias = new LocationBias() { 
                Circle = new GeoCircle() { 
                    Center = new LatLng(12.123, 22.2) 
                }
            }
        };

        await _service.SuggestPlacesAsync(request);

        captured.Should().NotBeNull();

        captured.Method.Should().Be(HttpMethod.Post);
        captured.BaseUrl.Should().Be(PlacesEndpoints.BaseUrl);
        captured.Endpoint.Should().Be(PlacesEndpoints.Autocomplete);

        captured.Headers.Should().HaveCount(2);
        captured.Headers["Content-Type"]
            .Should().Be("application/json");
        captured.Headers["X-Goog-FieldMask"].Should().
            Be("suggestions.placePrediction.placeId,suggestions.placePrediction.text,suggestions.placePrediction.types");

        captured.Payload.Should().BeEquivalentTo(request);
    }
}