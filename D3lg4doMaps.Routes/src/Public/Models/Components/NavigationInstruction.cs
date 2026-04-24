namespace DelgadoMaps.Routes.Models.Components;

/// <summary>
/// Represents a navigation instruction for a route step or segment.
/// </summary>
/// <remarks>
/// Contains both machine-readable maneuver type and human-readable instructions.
/// </remarks>
public sealed class NavigationInstruction {
    /// <summary>
    /// Gets the maneuver type (e.g., "turn-left", "merge").
    /// </summary>
    public string? Maneuver { get; internal set; }

    /// <summary>
    /// Gets the human-readable navigation instruction.
    /// </summary>
    public string? Instruction { get; internal set; }
}