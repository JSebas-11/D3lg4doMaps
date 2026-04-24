using DelgadoMaps.Places.Internal.Models.DTOs;

namespace DelgadoMaps.Places.Internal.Models.Responses;

// MAIN MODEL
internal sealed class AutocompleteResponse {
    public List<SuggestionDto> Suggestions { get; set; } = [];
}

// ASIDE MODELS
internal sealed class SuggestionDto {
    public PlaceSuggestionDto? PlacePrediction { get; set; }
}