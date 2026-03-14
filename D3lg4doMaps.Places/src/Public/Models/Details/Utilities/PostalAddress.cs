namespace D3lg4doMaps.Places.Public.Models.Details.Utilities;

public sealed class PostalAddress {
    public string? RegionCode { get; internal set; }
    public string? PostalCode { get; internal set; }
    public string? AdministrativeArea { get; internal set; }
    public string? Locality { get; internal set; }
    public IReadOnlyList<string> AddressLines { get; internal set; } = [];
}