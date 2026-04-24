namespace DelgadoMaps.Routes.Models.Directions.Components;

/// <summary>
/// Represents localized (human-readable) values for a route step.
/// </summary>
/// <remarks>
/// These values are intended for display in navigation UIs.
/// </remarks>
public sealed class RouteLegStepLocalizedValues {
    /// <summary>
    /// Gets the formatted distance of the step.
    /// </summary>
    public string? Distance { get; internal set; }

    /// <summary>
    /// Gets the formatted static duration of the step.
    /// </summary>
    public string? StaticDuration { get; internal set; }
}