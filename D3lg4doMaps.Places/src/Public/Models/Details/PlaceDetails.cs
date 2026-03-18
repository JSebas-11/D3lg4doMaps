using D3lg4doMaps.Core.Public.Models.Geometry;
using D3lg4doMaps.Places.Public.Models.Details.Utilities;

namespace D3lg4doMaps.Places.Public.Models.Details;

public sealed class PlaceDetails {
    public string PlaceId { get; internal set; } = null!;
    public string? DisplayName { get; internal set; }
    public IReadOnlyList<string> Types { get; internal set; } = [];
    public string? FormattedAddress { get; internal set; }
    public string? GlobalCode { get; internal set; }
    public LatLng? Location { get; internal set; }
    public PostalAddress? PostalAddress { get; internal set; }
    public string? TimeZone { get; internal set; }
    public string? NationalPhoneNumber { get; internal set; }
    public string? InternationalPhoneNumber { get; internal set; }
    public string? PriceLevel { get; internal set; }
    public float? Rating { get; internal set; }
    public int? UserRatingCount { get; internal set; }
    public IReadOnlyList<string> RegularOpeningHoursDaysDescriptions { get; internal set; } = [];
    public GoogleMapsLinks? GoogleMapsLinks { get; internal set; }
    public PaymentOptions? PaymentOptions { get; internal set; }
    public ParkingOptions? ParkingOptions { get; internal set; }
}