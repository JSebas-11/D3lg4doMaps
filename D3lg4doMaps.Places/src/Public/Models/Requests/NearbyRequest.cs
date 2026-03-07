using D3lg4doMaps.Places.Public.Models.Geometry;

namespace D3lg4doMaps.Places.Public.Models.Requests;

public sealed class NearbyRequest {
    public IReadOnlyList<string> IncludedTypes { get; internal set; } = [];
    public int MaxResultCount { get; internal set; } = 1;
    public LocationRestriction LocationRestriction { get; internal set; } = new();
}