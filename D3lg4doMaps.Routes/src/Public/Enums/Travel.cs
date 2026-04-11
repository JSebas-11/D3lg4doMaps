namespace D3lg4doMaps.Routes.Public.Enums;

/// <summary>
/// Represents the mode of transportation used for a route or step.
/// </summary>
public enum TravelMode {
    /// <summary>
    /// Driving by car.
    /// </summary>
    Drive,

    /// <summary>
    /// Traveling by bicycle.
    /// </summary>
    Bicycle,

    /// <summary>
    /// Walking.
    /// </summary>
    Walk,

    /// <summary>
    /// Two-wheeled motor vehicles (e.g., motorcycles).
    /// </summary>
    TwoWheeler,
    
    /// <summary>
    /// Public transit (bus, train, etc.).
    /// </summary>
    Transit,

    /// <summary>
    /// Internal value representing an undefined state.
    /// Not intended for external use.
    /// </summary>
    Unknown
}

/// <summary>
/// Specifies the emission type of a vehicle.
/// </summary>
public enum VehicleEmissionType {
    /// <summary>Gasoline-powered vehicle.</summary>
    Gasoline,

    /// <summary>Electric vehicle.</summary>
    Electric,

    /// <summary>Hybrid vehicle.</summary>
    Hybrid,

    /// <summary>Diesel-powered vehicle.</summary>
    Diesel,

    /// <summary>
    /// Internal value representing an undefined state.
    /// Not intended for external use.
    /// </summary>
    Unknown
}

/// <summary>
/// Gets the traffic speed condition for this interval.
/// </summary>
public enum Speed {
    /// <summary>
    /// Normal traffic conditions.
    /// </summary>
    Normal,

    /// <summary>
    /// Slower than normal traffic.
    /// </summary>
    Slow,

    /// <summary>
    /// Heavy congestion or traffic jam.
    /// </summary>
    TrafficJam,
    
    /// <summary>
    /// Internal value representing an undefined state.
    /// Not intended for external use.
    /// </summary>
    Unknown
}