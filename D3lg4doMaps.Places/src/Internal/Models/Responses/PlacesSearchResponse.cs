using D3lg4doMaps.Places.Internal.Models.DTOs;

namespace D3lg4doMaps.Places.Internal.Models.Responses;

internal sealed class PlacesSearchResponse {
    public List<PlaceSearchDto> Places { get; set; } = [];
}