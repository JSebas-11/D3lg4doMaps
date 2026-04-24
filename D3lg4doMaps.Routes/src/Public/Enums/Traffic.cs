namespace DelgadoMaps.Routes.Enums;

/// <summary>
/// Specifies how traffic conditions are considered when calculating routes.
/// </summary>
public enum TrafficModel {
    /// <summary>
    /// Uses a balanced estimate based on historical and live traffic.
    /// </summary>
    BestGuess,

    /// <summary>
    /// Assumes worse-than-average traffic conditions.
    /// </summary>
    Pessimistic,

    /// <summary>
    /// Assumes better-than-average traffic conditions.
    /// </summary>
    Optimistic,
    
    /// <summary>
    /// Internal value representing an undefined state.
    /// Not intended for external use.
    /// </summary>
    Unknown
}