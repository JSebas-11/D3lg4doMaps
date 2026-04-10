using D3lg4doMaps.Core.Public.Models.Geometry;
using D3lg4doMaps.Places.Public.Models.Details.Components;

namespace D3lg4doMaps.Places.Public.Models.Details;

/// <summary>
/// Represents detailed information about a place.
/// </summary>
/// <remarks>
/// This model aggregates metadata returned by the Google Places Details API,
/// including location, contact information, ratings, and additional attributes.
/// </remarks>
public sealed class PlaceDetails {
    /// <summary>
    /// Gets the unique identifier of the place.
    /// </summary>
    public string PlaceId { get; internal set; } = null!;

    /// <summary>
    /// Gets the name of the place.
    /// </summary>
    public string? DisplayName { get; internal set; }

    /// <summary>
    /// Gets the list of place types associated with the place.
    /// </summary>
    public IReadOnlyList<string> Types { get; internal set; } = [];

    /// <summary>
    /// Gets the formatted address of the place.
    /// </summary>
    public string? FormattedAddress { get; internal set; }

    /// <summary>
    /// Gets the global plus code of the place.
    /// </summary>
    public string? GlobalCode { get; internal set; }

    /// <summary>
    /// Gets the geographic location of the place.
    /// </summary>
    public LatLng? Location { get; internal set; }

    /// <summary>
    /// Gets the structured postal address of the place.
    /// </summary>
    public PostalAddress? PostalAddress { get; internal set; }

    /// <summary>
    /// Gets the time zone of the place.
    /// </summary>
    public string? TimeZone { get; internal set; }
    
    /// <summary>
    /// Gets the national phone number of the place.
    /// </summary>
    public string? NationalPhoneNumber { get; internal set; }
    
    /// <summary>
    /// Gets the international phone number of the place.
    /// </summary>
    public string? InternationalPhoneNumber { get; internal set; }

    /// <summary>
    /// Gets the price level indicator for the place.
    /// </summary>
    public string? PriceLevel { get; internal set; }

    /// <summary>
    /// Gets the average rating of the place.
    /// </summary>
    public float? Rating { get; internal set; }

    /// <summary>
    /// Gets the total number of user ratings.
    /// </summary>
    public int? UserRatingCount { get; internal set; }

    /// <summary>
    /// Gets the regular opening hours descriptions.
    /// </summary>
    public IReadOnlyList<string> RegularOpeningHoursDaysDescriptions { get; internal set; } = [];

    /// <summary>
    /// Gets links related to the place.
    /// </summary>
    public GoogleMapsLinks? GoogleMapsLinks { get; internal set; }

    /// <summary>
    /// Gets the supported payment options for the place.
    /// </summary>
    public PaymentOptions? PaymentOptions { get; internal set; }

    /// <summary>
    /// Gets parking-related information for the place.
    /// </summary>
    public ParkingOptions? ParkingOptions { get; internal set; }
}