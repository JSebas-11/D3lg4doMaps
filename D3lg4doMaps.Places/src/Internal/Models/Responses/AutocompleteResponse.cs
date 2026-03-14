using D3lg4doMaps.Places.Internal.Models.DTOs;

namespace D3lg4doMaps.Places.Internal.Models.Responses;

// MAIN MODEL
internal sealed class AutocompleteResponse {
    public List<SuggestionDto> Suggestions { get; set; } = [];
}

// ASIDE MODELS
internal sealed class SuggestionDto {
    public PlaceSuggestionDto? PlacePrediction { get; set; }
}