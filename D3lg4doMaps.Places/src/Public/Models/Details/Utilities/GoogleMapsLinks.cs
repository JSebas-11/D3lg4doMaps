namespace D3lg4doMaps.Places.Public.Models.Details.Utilities;

/// <summary>
/// Represents a collection of useful Google Maps links related to a place.
/// </summary>
public sealed class GoogleMapsLinks {
    /// <summary>
    /// Gets the website URI of the place.
    /// </summary>
    public string? WebsiteUri { get; internal set; }

    /// <summary>
    /// Gets the Google Maps URI for the place.
    /// </summary>
    public string? GoogleMapsUri { get; internal set; }

    /// <summary>
    /// Gets the direct URI to the place page.
    /// </summary>
    public string? PlaceUri { get; internal set; }

    /// <summary>
    /// Gets the URI to write a review for the place.
    /// </summary>
    public string? WriteAReviewUri { get; internal set; }

    /// <summary>
    /// Gets the URI to view reviews.
    /// </summary>
    public string? ReviewsUri { get; internal set; }

    /// <summary>
    /// Gets the URI to view photos.
    /// </summary>
    public string? PhotosUri { get; internal set; }
}