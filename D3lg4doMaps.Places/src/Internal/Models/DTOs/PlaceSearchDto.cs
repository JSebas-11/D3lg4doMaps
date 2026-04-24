namespace DelgadoMaps.Places.Internal.Models.DTOs;

// MAIN MODEL
internal sealed class PlaceSearchDto {
    public string? Id { get; set; }
    public DisplayName? DisplayName { get; set; }
    public List<string> Types { get; set; } = [];
}

// ASIDE MODELS
internal sealed class DisplayName {
    public string? Text { get; set; }
    public string? LanguageCode { get; set; }
}