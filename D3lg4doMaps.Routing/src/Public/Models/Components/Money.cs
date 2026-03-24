namespace D3lg4doMaps.Routing.Public.Models.Components;

public sealed class Money {
    public string CurrencyCode { get; internal set; } = null!;
    public string Units { get; internal set; } = null!;
    public Int64 Nanos { get; internal set; }
}