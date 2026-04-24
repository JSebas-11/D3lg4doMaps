namespace DelgadoMaps.Places.Models.Details.Photos;

/// <summary>
/// Represents a photo associated with a place.
/// </summary>
/// <remarks>
/// Photos can be retrieved using <c>IDetailsService.GetPhotosAsync</c>.
/// </remarks>
public sealed class PlacePhoto {
    /// <summary>
    /// Gets the URI used to access the photo.
    /// </summary>
    public Uri Uri { get; internal set; } = null!;

    /// <summary>
    /// Gets the unique name or identifier of the photo.
    /// </summary>
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// Gets the name of the photo's author, if available.
    /// </summary>
    public string? AuthorName { get; internal set; }

    /// <summary>
    /// Gets the height of the photo in pixels.
    /// </summary>
    public int? HeightPx { get; internal set; }
    
    /// <summary>
    /// Gets the width of the photo in pixels.
    /// </summary>
    public int? WidthPx { get; internal set; }
}