namespace D3lg4doMaps.Routes.Public.Models.Common;

public sealed class Money {
    public string CurrencyCode { get; internal set; } = null!;
    public string Units { get; internal set; } = null!;
    public long Nanos { get; internal set; }
}