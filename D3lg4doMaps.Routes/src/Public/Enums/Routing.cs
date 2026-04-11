namespace D3lg4doMaps.Routes.Public.Enums;

/// <summary>
/// Specifies the level of detail returned for routes.
/// </summary>
public enum RouteDetailLevel {
    /// <summary>
    /// Returns only high-level summary information.
    /// </summary>
    Summary,

    /// <summary>
    /// Returns standard route information.
    /// </summary>
    Standard,

    /// <summary>
    /// Returns full route details including additional metadata.
    /// </summary>
    Full
}

/// <summary>
/// Defines the routing strategy used when computing routes.
/// </summary>
public enum RoutingPreference {
    /// <summary>
    /// Ignores traffic conditions.
    /// </summary>
    TrafficUnaware,

    /// <summary>
    /// Considers current traffic conditions.
    /// </summary>
    TrafficAware,

    /// <summary>
    /// Optimizes routes using advanced traffic-aware strategies.
    /// </summary>
    TrafficAwareOptimal,

    /// <summary>
    /// Internal value representing an undefined state.
    /// Not intended for external use.
    /// </summary>
    Unknown
}

/// <summary>
/// Represents the condition of a route matrix element.
/// </summary>
public enum RouteElementCondition {
    /// <summary>
    /// A valid route exists between origin and destination.
    /// </summary>
    RouteExists,

    /// <summary>
    /// No route could be found.
    /// </summary>
    RouteNotFound,
    
    /// <summary>
    /// Internal value representing an undefined state.
    /// Not intended for external use.
    /// </summary>
    Unknown
}