using System.Net;
using DelgadoMaps.Core.Internal.Caching;
using DelgadoMaps.Core.Internal.Mappers;
using FluentAssertions;

namespace DelgadoMaps.Tests.Core.Mappers;

[Trait("Module", "Core")]
[Trait("Feature", "Mapper")]
[Trait("Mapper", "Response")]
public class ResponseMapperTests {
     [Fact]
    public async Task ToCacheResponseAsync_ShouldMapStatusCodeAndBody() {
        using var response = new HttpResponseMessage(HttpStatusCode.OK) {
            Content = new StringContent("{\"success\":true}")
        };

        var result = await ResponseMapper.ToCacheResponseAsync(response);

        result.StatusCode.Should().Be(200);
        result.Body.Should().Be("{\"success\":true}");
    }

    [Fact]
    public async Task ToCacheResponseAsync_ShouldHandleEmptyBody() {
        using var response = new HttpResponseMessage(HttpStatusCode.NoContent) {
            Content = new StringContent(string.Empty)
        };

        var result = await ResponseMapper.ToCacheResponseAsync(response);

        result.StatusCode.Should().Be(204);
        result.Body.Should().BeEmpty();
    }

    [Fact]
    public async Task ToHttpResponse_ShouldMapStatusCodeAndBody() {
        var cacheResponse = new HttpCacheResponse(
            200,
            "{\"name\":\"Sebas\"}"
        );

        using var result = ResponseMapper.ToHttpResponse(cacheResponse);
        var body = await result.Content.ReadAsStringAsync();

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        body.Should().Be("{\"name\":\"Sebas\"}");
    }

    [Fact]
    public async Task Mapper_ShouldPreserveRoundtripSemantics() {
        using var original = new HttpResponseMessage(HttpStatusCode.Accepted) {
            Content = new StringContent("{\"route\":\"fastest\"}")
        };

        var cached = await ResponseMapper.ToCacheResponseAsync(original);

        using var restored = ResponseMapper.ToHttpResponse(cached);

        var restoredBody = await restored.Content.ReadAsStringAsync();

        restored.StatusCode.Should().Be(HttpStatusCode.Accepted);
        restoredBody.Should().Be("{\"route\":\"fastest\"}");
    }
}