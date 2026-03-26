---
title: Details
---

# 📍 Details

The **Details feature** provides access to rich, structured information about places.

It allows you to retrieve:

- 📄 General place metadata
- 🖼️ Photos
- ⭐ Reviews
- 🧪 Raw JSON responses (advanced scenarios)

## 🔗 Related Abstractions

This feature is exposed through:

👉 See: [IDetailsService](/docs/places/Abstractions.md#-idetailsservice)

---

## 🧩 Service

```csharp
public interface IDetailsService {
    Task<PlaceDetails> GetDetailsAsync(string placeId);
    Task<JsonDocument> GetDetailsRawAsync(string placeId, params string[] fields);
    Task<IReadOnlyList<PlacePhoto>> GetPhotosAsync(string placeId, PhotoRequest? photoRequest = null);
    Task<PlaceReviews> GetReviewsAsync(string placeId);
}
```

### ⚙️ Methods

- GetDetailsAsync → **structured** place data
- GetDetailsRawAsync → **raw JSON** response
- GetPhotosAsync → place **photos**
- GetReviewsAsync → **reviews** and **summaries**

### 🧠 Notes

- `GetDetailsAsync` → returns **strongly-typed structured** data
- `GetDetailsRawAsync` → useful for **unmapped fields and debugging** (remember to dispose)
- `GetPhotosAsync` → supports optional configuration via `PhotoRequest`
- `GetReviewsAsync` → includes both user reviews and optional AI summaries
- Prefer strongly-typed methods for most scenarios
- Use raw JSON only for advanced/custom needs
- All methods are async and designed for high-performance API usage

---

## ⚡ Examples

### 📍 Place Details

```csharp
var details = await detailsService.GetDetailsAsync(placeId);

Console.WriteLine(details.DisplayName);
Console.WriteLine(details.FormattedAddress);
```

### 🧾 Get Raw Details (Advanced)

```csharp
using var json = await detailsService.GetDetailsRawAsync(
    placeId, 
    "displayName", 
    "rating"
);

var root = json.RootElement;
```

### ⭐ Get Reviews

```csharp
var reviews = await detailsService.GetReviewsAsync(placeId);

foreach (var review in reviews.Reviews) {
    Console.WriteLine($"{review.AuthorName}: {review.Text}");
}
```

### 📸 Get Photos

```csharp
var request = new PhotoRequestBuilder()
    .WithMaximumPhotos(5)
    .WithResolution(800, 800)
    .Build();

var photos = await detailsService.GetPhotosAsync(placeId, request);
```

---

## 📦 Models

### PlaceDetails

Represents detailed information about a place.

```csharp
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
```

#### 🧠 Includes

- **Basic info** → id, name, types, address (formatted and detailed -> see [PostalAddress](#postaladdress))
- **Location** → timezone, coordinates exposed by [LatLng](/docs/core/Models.md#-latlng) Core object
- **Contact** → phone numbers
- **Ratings** → score, user count and price level
- **Extras**
    - Opening hours
    - Links -> see [GoogleMapsLinks](#googlemapslinks),
    - Payments -> see [PaymentOtions](#paymentoptions), 
    - Parking -> see [ParkingOptions](#parkingoptions) 

---

### PlacePhoto

Represents a photo associated with a place.

```csharp
public sealed class PlacePhoto {
    public Uri Uri { get; internal set; } = null!;
    public string Name { get; internal set; } = null!;
    public string? AuthorName { get; internal set; }
    public int? HeightPx { get; internal set; }
    public int? WidthPx { get; internal set; }
}
```

--- 

### PlaceReviews

Represents reviews and summary information for a place.

```csharp
public sealed class PlaceReviews {
    public string PlaceId { get; internal set; } = null!;
    public string DisplayName { get; internal set; } = null!;
    public IReadOnlyList<Review> Reviews { get; internal set; } = [];
    public ReviewSummary? ReviewSummary { get; internal set; }
    public string? ReviewsUri { get; internal set; }
}
```

#### 🧠 Includes

- Individual **user reviews** -> see [Review](#review)
- Optional **AI-generated summary** -> see [ReviewSummary](#reviewsummary)
- **Link** to Google Maps reviews

--- 

### Raw JSON Access

- Useful for **unmapped fields**
- Useful for **debugging**

> ⚠️ JsonDocument is disposable

```csharp
using var json = await details.GetDetailsRawAsync(placeId);
```

---

##  🧰 Components

These models provide additional structured information used within `PlaceDetails` and `PlaceReviews`.

- These models are **optional and context-dependent**
- Values **may be null** depending on API response
- Designed to keep both `PlaceDetails` and `PlaceReviews` clean and modular

---

### PostalAddress

Represents a structured postal address. Used in [PlaceDetails](#placedetails)

```csharp
public sealed class PostalAddress {
    public string? RegionCode { get; internal set; }
    public string? PostalCode { get; internal set; }
    public string? AdministrativeArea { get; internal set; }
    public string? Locality { get; internal set; }
    public IReadOnlyList<string> AddressLines { get; internal set; } = [];
}
```

#### ⚙️ Properties

| Property             | Description              |
|:-------------------- |:------------------------ |
| `RegionCode`         | Country/region code      |
| `PostalCode`         | ZIP/postal code          |
| `AdministrativeArea` | State/province           |
| `Locality`           | Sity                     |
| `AddressLines`       | Formatted address lines  |

### GoogleMapsLinks

Represents useful Google Maps-related links. Used in [PlaceDetails](#placedetails)

```csharp
public sealed class GoogleMapsLinks {
    public string? WebsiteUri { get; internal set; }
    public string? GoogleMapsUri { get; internal set; }
    public string? PlaceUri { get; internal set; }
    public string? WriteAReviewUri { get; internal set; }
    public string? ReviewsUri { get; internal set; }
    public string? PhotosUri { get; internal set; }
}
```

#### ⚙️ Properties

| Property           | Description            |
|:------------------ |:---------------------- |
| `WebsiteUri`       | Official website       |
| `GoogleMapsUri`    | Maps view              |
| `PlaceUri`         | Direct place page      |
| `WriteAReviewUri`  | Review submission link |
| `ReviewsUri`       | Reviews page           |
| `PhotosUri`        | Photos page            |

### PaymentOptions

Represents supported payment methods. Used in [PlaceDetails](#placedetails)

```csharp
public sealed class PaymentOptions {
    public bool? AcceptsCreditCards { get; internal set; }
    public bool? AcceptsDebitCards { get; internal set; }
    public bool? AcceptsCashOnly { get; internal set; }
    public bool? AcceptsNfc { get; internal set; }
}
```

#### ⚙️ Properties

| Property             | Description            |
|:-------------------- |:---------------------- |
| `AcceptsCreditCards` | Credit card support    |
| `AcceptsDebitCards`  | Debit card support     |
| `AcceptsCashOnly`    | Cash-only business     |
| `AcceptsNfc`         | Contactless payments   |

### ParkingOptions

Represents parking availability. Used in [PlaceDetails](#placedetails)

```csharp
public sealed class ParkingOptions {
    public bool? FreeParkingLot { get; internal set; }
    public bool? FreeStreetParking { get; internal set; }
}
```

#### ⚙️ Properties

| Property            | Description              |
|:------------------- |:------------------------ |
| `FreeParkingLot`    | Dedicated parking lot    |
| `FreeStreetParking` | Street parking available |

---

### Review

Represents a single user review. Used in [PlaceReviews](#placereviews)

```csharp
public sealed class Review {
    public string Text { get; internal set; } = null!;
    public float? Rating { get; internal set; }
    public string? AuthorName { get; internal set; }
    public DateTimeOffset? PublishTime { get; internal set; }
    public string LanguageCode { get; internal set; } = null!;
}
```

#### ⚙️ Properties

| Property       | Description                   |
|:-------------- |:----------------------------- |
| `Text`         | Review content                |
| `Rating`       | User rating                   |
| `AuthorName`   | Reviewer name                 |
| `PublishTime`  | When the review was published |
| `LanguageCode` | Review language               |

### ReviewSummary

Represents an AI-generated summary of reviews (**optional** and may not always be present). Used in [PlaceReviews](#placereviews)

```csharp
public sealed class ReviewSummary {
    public string Text { get; internal set; } = null!;
    public string LanguageCode { get; internal set; } = null!;
}
```

#### ⚙️ Properties

| Property       | Description          |
|:-------------- |:-------------------- |
| `Text`         | summarized sentiment |
| `LanguageCode` | Summary language     |

--- 

## 🧾 Request

### PhotoRequest

Represents the configuration used when requesting photos for a place.

```csharp
public sealed class PhotoRequest {
    public int MaxPhotos { get; internal set; } = 10;
    public int MaxHeightPx { get; internal set; } = 480;
    public int MaxWidthPx { get; internal set; } = 480;
}
```

#### ⚙️ Properties

| Property      | Description                                       |
|:------------- |:------------------------------------------------- |
| `MaxPhotos`   | Maximum number of photos to retrieve (default: 10)|
| `MaxHeightPx` | Maximum height in pixels (default: 480)           |
| `MaxWidthPx`  | Maximum width in pixels (default: 480)            |

#### 🧠 Notes

- Must be constructed using `PhotoRequestBuilder`

### PhotoRequestBuilder

Provides a fluent API for constructing `PhotoRequest` instances.

```csharp
public sealed class PhotoRequestBuilder {
    public PhotoRequestBuilder WithMaximumPhotos(int maxPhotos);
    public PhotoRequestBuilder WithResolution(int maxHeight, int maxWidth);

    public PhotoRequest Build();
}
```

#### ⚙️ Configuration

- `WithMaximumPhotos(int)` → limits number of photos (1–10)
- `WithResolution(int, int)` → sets max height & width (1–4096 px)

#### 🧠 Notes

- **Defaults are applied** if no configuration is provided:
    - 10 photos
    - 480x480 resolution
- Validation is enforced through [MapsInvalidRequestException](/docs/core/Exceptions.md#️-mapsinvalidrequestexception)
- Designed to keep photo requests safe and within API limits
