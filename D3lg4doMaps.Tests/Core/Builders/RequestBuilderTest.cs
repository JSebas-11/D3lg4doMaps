using DelgadoMaps.Core.Internal.Builders;
using FluentAssertions;

namespace DelgadoMaps.Tests.Core.Builders;

[Trait("Module", "Core")]
[Trait("Feature", "Builder")]
public class RequestBuilderTest {
    // -------------------- INNER PROPS --------------------
    private readonly string _baseUrl = "https://places.googleapis.com/v1/";
    private readonly string _endpoint = "places:searchText";

    // -------------------- METHS --------------------
    [Fact]
    public async Task Build_CreateRequestSuccessfully() {
        var uriBuilder = new MapsUriBuilder();
        
        var headers = new Dictionary<string, string>() {
            { "type", "json" }, { "client", "asp.net" }
        };
        var payload = new { TextQuery = "test", Radius = 1.25 };
        
        HttpRequestMessage request = new RequestBuilder(uriBuilder)
            .SetMethod(HttpMethod.Get)
            .SetPath(_baseUrl, _endpoint)
            .AddHeaders(headers)
            .AddQuery(new Dictionary<string, string>())
            .SetJsonPayload(payload)
            .Build();

        // METHOD
        request.Method.Should().Be(HttpMethod.Get);

        // HEADERS
        request.Headers.Should().ContainKeys("type", "client");

        // CONTENT
        request.Content.Should().NotBeNull();

        var content = await request.Content!.ReadAsStringAsync();

        content.Should().Contain("textQuery");
        content.Should().Contain("radius");
    }
}