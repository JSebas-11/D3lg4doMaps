namespace D3lg4doMaps.Routes.Public.Models.Common;

/// <summary>
/// Represents a monetary value.
/// </summary>
/// <remarks>
/// Typically used for representing transit fares.
/// </remarks>
public sealed class Money {
    /// <summary>
    /// Gets the ISO currency code (e.g., "USD", "EUR").
    /// </summary>
    public string CurrencyCode { get; internal set; } = null!;

    /// <summary>
    /// Gets the whole units of the amount.
    /// </summary>
    public string Units { get; internal set; } = null!;

    /// <summary>
    /// Gets the fractional units of the amount.
    /// </summary>
    public long Nanos { get; internal set; }
}