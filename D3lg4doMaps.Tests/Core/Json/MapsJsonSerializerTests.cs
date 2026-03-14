using System.Text.Json;
using D3lg4doMaps.Core.Internal.Json;
using FluentAssertions;

namespace D3lg4doMaps.Tests.Core.Json;

[Trait("Module", "Core")]
[Trait("Feature", "Serializer")]
public class MapsJsonSerializerTests {
    // -------------------- INNER PROPS --------------------
    private readonly string _json = """
    {
        "serialize": true,
        "code": "secret"
    }
    """;

    private class TestModel {
        public bool Serialize { get; set; }
        public string? Code { get; set; }
    }

    // -------------------- METHS --------------------
    [Fact]
    public void Serialize_ShouldReturnValidJson() {
        var serializer = new MapsJsonSerializer();
        var obj = new { Serialize = true,  Code = "secret" };

        var jsonStr = serializer.Serialize(obj);

        jsonStr.Should().NotBeNullOrWhiteSpace();
        jsonStr.Should().Contain("serialize");
        jsonStr.Should().Contain("code");
    }
    
    [Fact]
    public void Deserialize_ToJsonDocumentSuccessfully() {
        var doc = new MapsJsonSerializer().Deserialize<JsonDocument>(_json);

        doc.Should().NotBeNull();
        doc.RootElement.GetProperty("serialize").GetBoolean().Should().BeTrue();
        doc.RootElement.GetProperty("code").GetString().Should().Be("secret");
    }
    
    [Fact]
    public void Deserialize_ToObjectSuccessfully() {
        var obj = new MapsJsonSerializer().Deserialize<TestModel>(_json);

        obj.Should().NotBeNull();
        obj.Serialize.Should().BeTrue();
        obj.Code.Should().Be("secret");
    }
    
    [Fact]
    public void Deserialize_ShouldReturnNull_WhenJsonIsNull() {
        var obj = new MapsJsonSerializer().Deserialize<TestModel>("");

        obj.Should().BeNull();
    }
}