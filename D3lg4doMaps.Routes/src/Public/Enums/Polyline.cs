namespace DelgadoMaps.Routes.Enums;

/// <summary>
/// Specifies the level of detail for route polylines.
/// </summary>
public enum PolylineQuality {
    /// <summary>
    /// High-resolution polyline with detailed geometry.
    /// </summary>
    HighQuality,

    /// <summary>
    /// Simplified polyline for overview display.
    /// </summary>
    Overview,

    /// <summary>
    /// Internal value representing an undefined state.
    /// Not intended for external use.
    /// </summary>
    Unknown
}

/// <summary>
/// Specifies the encoding format used for polylines.
/// </summary>
public enum PolylineEncoding {
    /// <summary>
    /// Encoded polyline string format.
    /// </summary>
    EncodedPolyline,

    // GeoJsonLinestring
    
    /// <summary>
    /// Internal value representing an undefined state.
    /// Not intended for external use.
    /// </summary>
    Unknown
}