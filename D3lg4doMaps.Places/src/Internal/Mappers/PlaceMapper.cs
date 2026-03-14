using D3lg4doMaps.Places.Internal.Models.DTOs;
using D3lg4doMaps.Places.Public.Models;

namespace D3lg4doMaps.Places.Internal.Mappers;

internal static class PlaceMapper {
    public static PlaceSearchResult ToSearchResult(PlaceSearchDto dto)
        => new () { 
            PlaceId = dto.Id, 
            Types = dto.Types ?? [], 
            DisplayName = dto.DisplayName?.Text
        };
}