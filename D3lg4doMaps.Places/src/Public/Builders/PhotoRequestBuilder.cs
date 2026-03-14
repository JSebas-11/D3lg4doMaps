using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Public.Builders;

public sealed class PhotoRequestBuilder {
    // -------------------- INIT --------------------
    private int _maxPhotos = -1;
    private (int Height, int Width)? _resolution;

    public PhotoRequestBuilder() {}

    // -------------------- BUILD --------------------
    public PhotoRequest Build() 
        => new () {
            MaxPhotos = _maxPhotos == -1 ? 10 : _maxPhotos,
            MaxHeightPx = _resolution?.Height ?? 480,
            MaxWidthPx = _resolution?.Width ?? 480
        };

    // -------------------- CONFIG --------------------
    public PhotoRequestBuilder WithMaximumPhotos(int maxPhotos) {
        if (maxPhotos is <= 0 or > 10) 
            throw new MapsInvalidRequestException("MaxPhotos must be between 1 and 10.");

        _maxPhotos = maxPhotos;
        return this;
    }

    public PhotoRequestBuilder WithResolution(int maxHeight, int maxWidth) {
        if ((maxHeight is <= 0 or > 4096) || (maxWidth is <= 0 or > 4096)) 
            throw new MapsInvalidRequestException("Resolution must be between 1 and 4096 pixels.");

        _resolution = (maxHeight, maxWidth);
        return this;
    }
}