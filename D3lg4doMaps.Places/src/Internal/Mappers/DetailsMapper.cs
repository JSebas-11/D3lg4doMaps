using System.Text.Json;
using D3lg4doMaps.Places.Internal.Helpers;
using D3lg4doMaps.Places.Public.Models.Details;
using D3lg4doMaps.Places.Public.Models.Details.Reviews;
using D3lg4doMaps.Places.Public.Models.Details.Utilities;
using D3lg4doMaps.Places.Public.Models.Geometry;

namespace D3lg4doMaps.Places.Internal.Mappers;

internal static class DetailsMapper {
    // -------------------- MAIN MODELS --------------------
    public static PlaceDetails ToPlaceDetails(string originalId, JsonDocument json) {
        var root = json.RootElement;

        var details = new PlaceDetails() { 
            PlaceId = originalId,
            DisplayName = root.GetObject("displayName")?.GetStringValue("text"),
            Types = [.. root.GetArray("types")
                .Select(s => s.GetString())
                .Where(s => !string.IsNullOrWhiteSpace(s))!],
            FormattedAddress = root.GetStringValue("formattedAddress"),
            GlobalCode = root.GetObject("plusCode")?.GetStringValue("globalCode"),
            TimeZone = root.GetStringValue("timeZone"),
            NationalPhoneNumber = root.GetStringValue("nationalPhoneNumber"),
            InternationalPhoneNumber = root.GetStringValue("internationalPhoneNumber"),
            PriceLevel = root.GetStringValue("priceLevel"),
            Rating = root.GetFloat("rating"),
            UserRatingCount = root.GetInt("userRatingCount"),
            GoogleMapsLinks = ToMapsLinks(root)
        };

        // LOCATION
        var location = root.GetObject("location");
        if (location is not null)
            details.Location = new GeoCenter() {
                Latitude = (double)location?.GetDoubleValue("latitude")!,
                Longitude = (double)location?.GetDoubleValue("longitude")!
            };

        // POSTAL ADDRESS
        var postal = root.GetObject("postalAddress");
        if (postal is not null)
            details.PostalAddress = ToPostalAddress((JsonElement)postal);
            
        // OPENING HOURS
        var hourInfo = root.GetObject("regularOpeningHours");
        if (hourInfo is not null)
            details.RegularOpeningHoursDaysDescriptions = [.. 
                hourInfo?.GetArray("weekdayDescriptions")
                    .Select(wd => wd.GetString())
                    .Where(s => !string.IsNullOrWhiteSpace(s))!
            ];

        // OPTIONS
        var payOpts = root.GetObject("paymentOptions");
        if (payOpts is not null)
            details.PaymentOptions = new PaymentOptions() {
                AcceptsCreditCards = payOpts?.GetBool("acceptsCreditCards"),
                AcceptsDebitCards = payOpts?.GetBool("acceptsDebitCards"),
                AcceptsCashOnly = payOpts?.GetBool("acceptsCashOnly"),
                AcceptsNfc = payOpts?.GetBool("acceptsNfc")
            };
        
        var parkOpts = root.GetObject("parkingOptions");
        if (parkOpts is not null)
            details.ParkingOptions = new ParkingOptions() {
                FreeParkingLot = parkOpts?.GetBool("freeParkingLot"),
                FreeStreetParking = parkOpts?.GetBool("freeStreetParking")
            };

        return details;
    }
    
    public static PlaceReviews ToPlaceReviews(string originalId, JsonDocument json) {
        var root = json.RootElement;
        
        var placeRev = new PlaceReviews () { 
            PlaceId = originalId, 
            DisplayName = root.GetObject("displayName")?.GetStringValue("text") ?? "",
            Reviews = [.. root.GetArray("reviews").Select(ToReview)]
        };

        // AI SUMMARIZE
        var reviewSumm = root.GetObject("reviewSummary")?.GetObject("text");
        if (reviewSumm is not null) {
            placeRev.ReviewSummary = new ReviewSummary() {
                Text = reviewSumm.Value.GetStringValue("text")!,
                LanguageCode = reviewSumm.Value.GetStringValue("languageCode")!
            };
        }

        // REVIEWS URI
        var uris = root.GetObject("googleMapsLinks")?.GetStringValue("reviewsUri");
        if (!string.IsNullOrWhiteSpace(uris))
            placeRev.ReviewsUri = uris;

        return placeRev;
    }
    
    // -------------------- ASIDE MODELS --------------------
    public static Review ToReview(JsonElement json) {
        var txtObj = json.GetObject("text");

        return new Review() {
            Text = txtObj?.GetStringValue("text") ?? "",
            LanguageCode = txtObj?.GetStringValue("languageCode") ?? "",
            Rating = json.GetFloat("rating"),
            AuthorName = json.GetObject("authorAttribution")?.GetStringValue("displayName"),
            PublishTime = DateTimeOffset.TryParse(json.GetStringValue("publishTime"), out var dt)
                ? dt : null
        };
    }
    
    public static PostalAddress ToPostalAddress(JsonElement json) 
        => new () {
            RegionCode = json.GetStringValue("regionCode"),
            PostalCode = json.GetStringValue("postalCode"),
            AdministrativeArea = json.GetStringValue("administrativeArea"),
            Locality = json.GetStringValue("locality"),
            AddressLines = [.. json.GetArray("addressLines")
                .Select(al => al.GetString())
                .Where(s => !string.IsNullOrWhiteSpace(s))!]
        };
    
    public static GoogleMapsLinks ToMapsLinks(JsonElement json) {
        var googleLinks = json.GetObject("googleMapsLinks");
        
        return new GoogleMapsLinks() {
            WebsiteUri = json.GetStringValue("websiteUri"),
            GoogleMapsUri = json.GetStringValue("googleMapsUri"),
            PlaceUri = googleLinks?.GetStringValue("placeUri"),
            WriteAReviewUri = googleLinks?.GetStringValue("writeAReviewUri"),
            ReviewsUri = googleLinks?.GetStringValue("reviewsUri"),
            PhotosUri = googleLinks?.GetStringValue("photosUri")
        };
    }
}