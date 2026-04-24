namespace DelgadoMaps.Places.Models.Requests;

/// <summary>
/// Represents a request for retrieving photos associated with a place.
/// </summary>
/// <remarks>
/// This model is constructed using <see cref="Builders.PhotoRequestBuilder"/>
/// to configure limits and resolution constraints.
/// </remarks>
public sealed class PhotoRequest {
    /// <summary>
    /// Gets the maximum number of photos to retrieve.
    /// </summary>
    /// <remarks>
    /// Defaults to 10 if not explicitly configured.
    /// </remarks>
    public int MaxPhotos { get; internal set; } = 10;

    /// <summary>
    /// Gets the maximum height of the returned photos in pixels.
    /// </summary>
    /// <remarks>
    /// Defaults to 480 pixels.
    /// </remarks>
    public int MaxHeightPx { get; internal set; } = 480;
    
    /// <summary>
    /// Gets the maximum width of the returned photos in pixels.
    /// </summary>
    /// <remarks>
    /// Defaults to 480 pixels.
    /// </remarks>
    public int MaxWidthPx { get; internal set; } = 480;
}