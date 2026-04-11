namespace D3lg4doMaps.Routes.Public.Models.Components;

/// <summary>
/// Represents an encoded polyline describing a path.
/// </summary>
/// <remarks>
/// Polylines are used to efficiently encode route geometry.
/// </remarks>
public sealed class Polyline {
    /// <summary>
    /// Gets the encoded polyline string.
    /// </summary>
    public string EncodedPolyline { get; internal set; } = null!;
}