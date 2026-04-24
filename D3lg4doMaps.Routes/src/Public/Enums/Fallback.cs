namespace DelgadoMaps.Routes.Enums;

/// <summary>
/// Represents the routing mode used when fallback occurs.
/// </summary>
public enum RoutingMode {
    /// <summary>
    /// Fallback using traffic-unaware routing.
    /// </summary>
    FallbackTrafficUnaware,

    /// <summary>
    /// Fallback using traffic-aware routing.
    /// </summary>
    FallbackTrafficAware,

    /// <summary>
    /// Internal value representing an undefined state.
    /// Not intended for external use.
    /// </summary>
    Unknown
}

/// <summary>
/// Represents the reason for fallback routing.
/// </summary>
public enum Reason {
    /// <summary>
    /// A server-side error occurred.
    /// </summary>
    ServerError,

    /// <summary>
    /// The request exceeded latency constraints.
    /// </summary>
    LatencyExceeded,
    
    /// <summary>
    /// Internal value representing an undefined state.
    /// Not intended for external use.
    /// </summary>
    Unknown
}