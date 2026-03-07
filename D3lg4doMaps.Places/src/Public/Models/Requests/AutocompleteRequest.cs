using D3lg4doMaps.Places.Public.Models.Geometry;

namespace D3lg4doMaps.Places.Public.Models.Requests;

public sealed class AutocompleteRequest {
    public string Input { get; internal set; } = null!;
    public LocationBias? LocationBias { get; internal set; }
}