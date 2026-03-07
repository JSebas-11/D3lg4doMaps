using System.Text.Json.Serialization;

namespace D3lg4doMaps.Places.Public.Models.Geometry;

public sealed class GeoCircle {
    [JsonPropertyName("center")]
    public GeoCenter Center { get; internal set; } = new();
    public double Radius { get; internal set; }
}