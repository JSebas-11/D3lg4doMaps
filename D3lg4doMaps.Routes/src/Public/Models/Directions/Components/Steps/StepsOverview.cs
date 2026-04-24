namespace DelgadoMaps.Routes.Models.Directions.Components;

/// <summary>
/// Represents a summarized overview of route steps.
/// </summary>
/// <remarks>
/// Provides a simplified representation of navigation instructions,
/// useful for previews or compact UI displays.
/// </remarks>
public sealed class StepsOverview {
    /// <summary>
    /// Gets the list of summarized segments.
    /// </summary>
    public IReadOnlyList<Segment> Segments { get; internal set; } = [];
}