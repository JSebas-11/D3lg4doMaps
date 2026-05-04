using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Configuration;
using DelgadoMaps.Core.Internal.Caching.KeyStrategy;
using DelgadoMaps.Core.Internal.Json;
using DelgadoMaps.Core.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;

namespace DelgadoMaps.Tests.Core.Caching;

[Trait("Module", "Core")]
[Trait("Feature", "Caching")]
[Trait("Type", "KeyStrategy")]
public class RequestFingerprintStrategyTests {
    // -------------------- INIT --------------------
    private readonly RequestFingerprintCacheKeyStrategy _sut;

    public RequestFingerprintStrategyTests() {
        IMapsJsonSerializer serializer = new MapsJsonSerializer();

        var opts = Options.Create(new MapsCachingOptions {
            Prefix = "test-prefix"
        });

        _sut = new RequestFingerprintCacheKeyStrategy(serializer, opts);
    }

    // -------------------- TESTS --------------------
     [Fact]
    public void GenerateCacheKey_ShouldGenerateSameKey_WhenPayloadPropertiesOrderChanges() {
        var req1 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchText",
            Payload = new {
                Query = "Medellin",
                Language = "en"
            }
        };

        var req2 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchText",
            Payload = new {
                Language = "en",
                Query = "Medellin"
            }
        };

        var key1 = _sut.GenerateCacheKey(req1);
        var key2 = _sut.GenerateCacheKey(req2);

        key1.Should().Be(key2);
    }

    [Fact]
    public void GenerateCacheKey_ShouldGenerateDifferentKey_WhenPayloadChanges() {
        var req1 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchText",
            Payload = new {
                Query = "Medellin"
            }
        };

        var req2 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchText",
            Payload = new {
                Query = "Bogota"
            }
        };

        var key1 = _sut.GenerateCacheKey(req1);
        var key2 = _sut.GenerateCacheKey(req2);

        key1.Should().NotBe(key2);
    }

    [Fact]
    public void GenerateCacheKey_ShouldIgnoreQueryApiKey() {
        var req1 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:autocomplete",
            Query = new Dictionary<string, string> {
                ["input"] = "pizza",
                ["key"] = "secret-1"
            }
        };

        var req2 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:autocomplete",
            Query = new Dictionary<string, string> {
                ["input"] = "pizza",
                ["key"] = "secret-2"
            }
        };

        var key1 = _sut.GenerateCacheKey(req1);
        var key2 = _sut.GenerateCacheKey(req2);

        key1.Should().Be(key2);
    }

    [Fact]
    public void GenerateCacheKey_ShouldGenerateSameKey_WhenQueryOrderChanges() {
        var req1 = new MapsApiRequest {
            BaseUrl = "https://routes.googleapis.com",
            Endpoint = "directions/v2:computeRoutes",
            Query = new Dictionary<string, string> {
                ["language"] = "en",
                ["region"] = "CO"
            }
        };

        var req2 = new MapsApiRequest {
            BaseUrl = "https://routes.googleapis.com",
            Endpoint = "directions/v2:computeRoutes",
            Query = new Dictionary<string, string> {
                ["region"] = "CO",
                ["language"] = "en"
            }
        };

        var key1 = _sut.GenerateCacheKey(req1);
        var key2 = _sut.GenerateCacheKey(req2);

        key1.Should().Be(key2);
    }

    [Fact]
    public void GenerateCacheKey_ShouldGenerateDifferentKey_WhenArrayOrderChanges() {
        var req1 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchNearby",
            Payload = new {
                Types = new[] { "restaurant", "cafe" }
            }
        };

        var req2 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchNearby",
            Payload = new {
                Types = new[] { "cafe", "restaurant" }
            }
        };

        var key1 = _sut.GenerateCacheKey(req1);
        var key2 = _sut.GenerateCacheKey(req2);

        key1.Should().NotBe(key2);
    }

    [Fact]
    public void GenerateCacheKey_ShouldGenerateSameKey_WhenNestedPayloadPropertiesOrderChanges() {
        var req1 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchText",
            Payload = new {
                Query = "coffee",
                LocationBias = new {
                    Latitude = 6.2442,
                    Longitude = -75.5812
                }
            }
        };

        var req2 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchText",
            Payload = new {
                LocationBias = new {
                    Longitude = -75.5812,
                    Latitude = 6.2442
                },
                Query = "coffee"
            }
        };

        var key1 = _sut.GenerateCacheKey(req1);
        var key2 = _sut.GenerateCacheKey(req2);

        key1.Should().Be(key2);
    }

    [Fact]
    public void GenerateCacheKey_ShouldGenerateSameKey_WhenDictionaryOrderChangesInsidePayload() {
        var req1 = new MapsApiRequest {
            BaseUrl = "https://routes.googleapis.com",
            Endpoint = "directions/v2:computeRoutes",
            Payload = new {
                Filters = new Dictionary<string, object> {
                    ["vehicle"] = "car",
                    ["avoidTolls"] = true
                }
            }
        };

        var req2 = new MapsApiRequest {
            BaseUrl = "https://routes.googleapis.com",
            Endpoint = "directions/v2:computeRoutes",
            Payload = new {
                Filters = new Dictionary<string, object> {
                    ["avoidTolls"] = true,
                    ["vehicle"] = "car"
                }
            }
        };

        var key1 = _sut.GenerateCacheKey(req1);
        var key2 = _sut.GenerateCacheKey(req2);

        key1.Should().Be(key2);
    }

    [Fact]
    public void GenerateCacheKey_ShouldGenerateDifferentKey_WhenNestedPayloadValueChanges() {
        var req1 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchNearby",
            Payload = new {
                Circle = new {
                    Radius = 500
                }
            }
        };

        var req2 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchNearby",
            Payload = new {
                Circle = new {
                    Radius = 1000
                }
            }
        };

        var key1 = _sut.GenerateCacheKey(req1);
        var key2 = _sut.GenerateCacheKey(req2);

        key1.Should().NotBe(key2);
    }

    [Fact]
    public void GenerateCacheKey_ShouldGenerateDifferentKey_WhenArrayOrderChangesInsideNestedPayload() {
        var req1 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchText",
            Payload = new {
                Filters = new {
                    Types = new[] { "restaurant", "cafe", "bar" }
                }
            }
        };

        var req2 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchText",
            Payload = new {
                Filters = new {
                    Types = new[] { "bar", "restaurant", "cafe" }
                }
            }
        };

        var key1 = _sut.GenerateCacheKey(req1);
        var key2 = _sut.GenerateCacheKey(req2);

        key1.Should().NotBe(key2);
    }

    [Fact]
    public void GenerateCacheKey_ShouldGenerateSameKey_WhenPayloadContainsNullProperties() {
        var req1 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchText",
            Payload = new {
                Query = "pizza",
                Region = (string?)null
            }
        };

        var req2 = new MapsApiRequest {
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "places:searchText",
            Payload = new {
                Region = (string?)null,
                Query = "pizza"
            }
        };

        var key1 = _sut.GenerateCacheKey(req1);
        var key2 = _sut.GenerateCacheKey(req2);

        key1.Should().Be(key2);
    }
}
