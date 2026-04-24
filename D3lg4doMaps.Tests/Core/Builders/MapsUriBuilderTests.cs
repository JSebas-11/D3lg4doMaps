using DelgadoMaps.Core.Internal.Builders;
using FluentAssertions;

namespace DelgadoMaps.Tests.Core.Builders;

[Trait("Module", "Core")]
[Trait("Feature", "Builder")]
public class MapsUriBuilderTests {
    // -------------------- INNER PROPS --------------------
    private readonly string _baseUrl = "https://places.googleapis.com/v1/";
    private readonly string _endpoint = "places:searchText";

    // -------------------- METHS --------------------
    [Theory]
    [InlineData("places:searchText")]
    [InlineData("/places:searchText")]
    [InlineData("//places:searchText")]
    public void Build_CreatePathSuccessfully(string endpoint) {
        Uri uri = new MapsUriBuilder()
            .WithPath(_baseUrl, endpoint)
            .Build();

        uri.Should().Be(new Uri($"{_baseUrl}{_endpoint}"));
    }
    
    [Fact]
    public void Build_AddQuerySuccessfully() {
        var query = new Dictionary<string, string>() {
            { "key", "12345" }, { "q", "query" }
        };
        
        Uri uri = new MapsUriBuilder()
            .WithPath(_baseUrl, _endpoint)
            .AddQuery(query)
            .Build();

        uri.AbsoluteUri.Should().Be($"{_baseUrl}{_endpoint}?key=12345&q=query");
    }
}