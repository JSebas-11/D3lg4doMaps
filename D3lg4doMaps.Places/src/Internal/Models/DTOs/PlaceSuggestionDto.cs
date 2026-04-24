namespace DelgadoMaps.Places.Internal.Models.DTOs;

// MAIN MODEL
internal sealed class PlaceSuggestionDto {
    public string? PlaceId { get; set; }
    public TextDto? Text { get; set; }
    public List<string> Types { get; set; } = [];
}

// ASIDE MODELS
internal sealed class TextDto {
    public string? Text { get; set; }
    public List<Match> Matches { get; set; } = [];
}

internal sealed class Match {
    public int EndOffset { get; set; } = -1;
}