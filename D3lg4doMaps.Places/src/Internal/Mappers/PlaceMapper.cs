using DelgadoMaps.Places.Internal.Models.DTOs;
using DelgadoMaps.Places.Models;

namespace DelgadoMaps.Places.Internal.Mappers;

internal static class PlaceMapper {
    public static PlaceSearchResult ToSearchResult(PlaceSearchDto dto)
        => new () { 
            PlaceId = dto.Id, 
            Types = dto.Types ?? [], 
            DisplayName = dto.DisplayName?.Text
        };
}