using System.Text.Json.Serialization;

namespace D3lg4doMaps.Places.Public.Models.Geometry;

public sealed class LocationRestriction {
    [JsonPropertyName("circle")]
    public GeoCircle? Circle { get; internal set; }
}