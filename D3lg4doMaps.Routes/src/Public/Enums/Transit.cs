namespace DelgadoMaps.Routes.Enums;

/// <summary>
/// Specifies the allowed transit travel modes.
/// </summary>
public enum TransitTravelMode {
    /// <summary>Bus transit.</summary>
    Bus,

    /// <summary>Subway transit.</summary>
    Subway,

    /// <summary>Train transit.</summary>
    Train,

    /// <summary>Light rail transit.</summary>
    LightRail,

    /// <summary>General rail transit.</summary>
    Rail,

    /// <summary>
    /// Internal value representing an undefined state.
    /// Not intended for external use.
    /// </summary>
    Unknown
}

/// <summary>
/// Specifies routing preferences for transit routes.
/// </summary>
public enum TransitRoutingPreference {
    /// <summary>Prefer routes with less walking.</summary>
    LessWalking,

    /// <summary>Prefer routes with fewer transfers.</summary>
    FewerTransfers,
    
    /// <summary>
    /// Internal value representing an undefined state.
    /// Not intended for external use.
    /// </summary>
    Unknown
}