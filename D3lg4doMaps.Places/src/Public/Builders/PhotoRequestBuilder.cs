using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Public.Builders;

/// <summary>
/// Provides a fluent API for constructing <see cref="PhotoRequest"/> instances.
/// </summary>
/// <remarks>
/// This builder allows configuring how many photos to retrieve and their resolution.
/// </remarks>
public sealed class PhotoRequestBuilder {
    // -------------------- INIT --------------------
    private int _maxPhotos = -1;
    private (int Height, int Width)? _resolution;

    public PhotoRequestBuilder() {}

    #region BUILD
    /// <summary>
    /// Builds a new <see cref="PhotoRequest"/> instance.
    /// </summary>
    /// <returns>
    /// A fully configured <see cref="PhotoRequest"/>.
    /// </returns>
    /// <remarks>
    /// Default values are applied when no configuration is provided:
    /// - Maximum photos: 10
    /// - Resolution: 480x480
    /// </remarks>
    public PhotoRequest Build() 
        => new () {
            MaxPhotos = _maxPhotos == -1 ? 10 : _maxPhotos,
            MaxHeightPx = _resolution?.Height ?? 480,
            MaxWidthPx = _resolution?.Width ?? 480
        };
    #endregion

    #region CONFIG
    /// <summary>
    /// Sets the maximum number of photos to retrieve.
    /// </summary>
    /// <param name="maxPhotos">
    /// The number of photos to retrieve (between 1 and 10).
    /// </param>
    /// <returns>
    /// The current <see cref="PhotoRequestBuilder"/> instance.
    /// </returns>
    /// <exception cref="MapsInvalidRequestException">
    /// Thrown when the value is outside the allowed range.
    /// </exception>
    public PhotoRequestBuilder WithMaximumPhotos(int maxPhotos) {
        if (maxPhotos is <= 0 or > 10) 
            throw new MapsInvalidRequestException("MaxPhotos must be between 1 and 10.");

        _maxPhotos = maxPhotos;
        return this;
    }

    /// <summary>
    /// Sets the maximum resolution for returned photos.
    /// </summary>
    /// <param name="maxHeight">
    /// Maximum height in pixels.
    /// </param>
    /// <param name="maxWidth">
    /// Maximum width in pixels.
    /// </param>
    /// <returns>
    /// The current <see cref="PhotoRequestBuilder"/> instance.
    /// </returns>
    /// <exception cref="MapsInvalidRequestException">
    /// Thrown when the resolution exceeds allowed limits (1–4096 pixels).
    /// </exception>
    public PhotoRequestBuilder WithResolution(int maxHeight, int maxWidth) {
        if ((maxHeight is <= 0 or > 4096) || (maxWidth is <= 0 or > 4096)) 
            throw new MapsInvalidRequestException("Resolution must be between 1 and 4096 pixels.");

        _resolution = (maxHeight, maxWidth);
        return this;
    }
    #endregion
}