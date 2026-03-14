namespace D3lg4doMaps.Places.Public.Models.Details.Utilities;

public sealed class PaymentOptions {
    public bool? AcceptsCreditCards { get; internal set; }
    public bool? AcceptsDebitCards { get; internal set; }
    public bool? AcceptsCashOnly { get; internal set; }
    public bool? AcceptsNfc { get; internal set; }
}