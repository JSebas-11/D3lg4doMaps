namespace DelgadoMaps.Routes.Models.DistanceMatrix.Components;

/// <summary>
/// Represents the status of a route matrix element computation.
/// </summary>
/// <remarks>
/// Contains a status code and an optional descriptive message.
/// </remarks>
public sealed class Status {
    /// <summary>
    /// Gets the status code.
    /// </summary>
    public int Code { get; internal set; }

    /// <summary>
    /// Gets the status message, if available.
    /// </summary>
    public string? Message { get; internal set; }
}