using D3lg4doMaps.Places.Internal.Models.DTOs;
using D3lg4doMaps.Places.Public.Models;

namespace D3lg4doMaps.Places.Internal.Mappers;

internal static class AutocompleteMapper {
    public static PlaceSuggestion ToPlaceSuggestion(PlaceSuggestionDto dto)
        => new() {
            PlaceId = dto.PlaceId ?? "", 
            Text = dto.Text?.Text ?? "", 
            EndOffset = dto.Text?.Matches.FirstOrDefault()?.EndOffset ?? -1,
            Types = dto.Types
        };
}