using DelgadoMaps.Places.Internal.Models.DTOs;

namespace DelgadoMaps.Places.Internal.Models.Responses;

internal sealed class PlacesSearchResponse {
    public List<PlaceSearchDto> Places { get; set; } = [];
}