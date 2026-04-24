using DelgadoMaps.Places.Internal.Models.DTOs;
using DelgadoMaps.Places.Models;

namespace DelgadoMaps.Places.Internal.Mappers;

internal static class AutocompleteMapper {
    public static PlaceSuggestion ToPlaceSuggestion(PlaceSuggestionDto dto)
        => new() {
            PlaceId = dto.PlaceId ?? "", 
            Text = dto.Text?.Text ?? "", 
            EndOffset = dto.Text?.Matches.FirstOrDefault()?.EndOffset ?? -1,
            Types = dto.Types
        };
}