---
title: Abstractions
---

# 🧩 Places Abstractions

The Places module defines a set of service abstractions that provide access
to Google Places functionality.

These services expose high-level operations such as:

- Autocomplete suggestions
- Place search
- Detailed place information (details, photos, reviews)

> ⚠️ These interfaces are typically consumed through `IPlacesService`.

---

## 🧾 IPlacesService

Provides a unified entry point for all Places functionality.

```csharp
public interface IPlacesService {
    IAutocompleteService Autocomplete { get; }
    IDetailsService Details { get; }
    ISearchService Search { get; }
}
```

### ⚙️ Responsibilities

- Aggregate all Places services
- Provide a **single access point** for consumers

### 🧠 Notes

- Recommended entry point for most use cases
- Avoids injecting multiple services individually

---

## 🔍 IAutocompleteService

Provides **autocomplete suggestions** based on user input.

This service is typically the **entry point of the Places workflow**, where users provide partial input and receive suggestions that include a `PlaceId`.

> The returned `PlaceId` can then be used to retrieve full place details via the `IDetailsService`.

```csharp
public interface IAutocompleteService {
    Task<IReadOnlyList<PlaceSuggestion>> SuggestPlacesAsync(
        AutocompleteRequest autocompleteRequest
    );
}
```

### ⚙️ Responsibilities

- Retrieve predicted places from partial input
- Support search-as-you-type scenarios

👉 See: [AutocompleteRequest](services/autocomplete.md#autocompleterequest)

### 🧠 Notes

- Useful for UI inputs (search bars, forms)
- Returns lightweight suggestion models -> see [PlaceSuggestion](services/autocomplete.md#placesuggestion)

---

## 🔍 ISearchService

Provides place **search functionality**.

This service represents **another entry point of the Places workflow**, allowing users to search for places using text queries or geographic constraints.

> The results include `PlaceId` values, which can be used to fetch detailed information through the `IDetailsService`.

```csharp
public interface ISearchService {
    Task<IReadOnlyList<PlaceSearchResult>> SearchByTextAsync(string textQuery);
    Task<IReadOnlyList<PlaceSearchResult>> SearchByNearbyAsync(NearbyRequest nearbyRequest);
}
```

### ⚙️ Responsibilities

- Perform text-based searches
- Perform location-based (nearby) searches -> see [NearbyRequest](services/search.md#nearbyrequest)

### 🧠 Notes

- Covers the most common discovery scenarios
- Returns structured search results -> see [PlaceSearchResult](services/search.md#placesearchresult)

---

## 🔍 IDetailsService

Provides **detailed information** about places based on `PlaceId`.

```csharp
public interface IDetailsService {
    Task<PlaceDetails> GetDetailsAsync(string placeId);
    Task<JsonDocument> GetDetailsRawAsync(string placeId, params string[] fields);
    Task<IReadOnlyList<PlacePhoto>> GetPhotosAsync(string placeId, PhotoRequest? photoRequest = null);
    Task<PlaceReviews> GetReviewsAsync(string placeId);
}
```

### ⚙️ Responsibilities

- Retrieve **structured place details** -> see [PlaceDetails](services/details.md#placedetails)
- Provide raw JSON access for **advanced scenarios**
- Fetch photos and reviews -> see [PlacePhoto](services/details#placephoto) - [PlaceReviews](services/details#placereviews)

### 🧠 Notes

- Combines **multiple endpoints** into a single service
- Raw access allows flexibility beyond mapped models
- Designed for both **simple and advanced** use cases

### ⚠️ Important

```csharp
using var json = await detailsService.GetDetailsRawAsync(placeId);
```

The returned `JsonDocument` must be **disposed** properly.