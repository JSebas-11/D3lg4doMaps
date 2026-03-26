---
title: Search
---

# 🔍 Search

The **Search feature** allows you to find places using either:

- Text queries (e.g., "restaurants near me")
- Geographic constraints (nearby search)

## 🔗 Related Abstractions

This feature is exposed through:

👉 See: [ISearchService](/docs/places/Abstractions.md#-isearchservice)

---

## 🧩 Service

```csharp
public interface ISearchService {
    Task<IReadOnlyList<PlaceSearchResult>> SearchByTextAsync(string textQuery);
    Task<IReadOnlyList<PlaceSearchResult>> SearchByNearbyAsync(NearbyRequest nearbyRequest);
}
```

### ⚙️ Methods

- SearchByTextAsync → search using a **text query**
- SearchByNearbyAsync → search using **location**

### 🧠 Notes

- Text search is quick and flexible
- Nearby search gives more control and precision
- Use nearby search when location matters (maps, GPS, etc.)

---

## ⚡ Examples

### ⌨️ Text Search

```csharp
var results = await searchService.SearchByTextAsync("coffee near Medellin");
```

### 🗺️ Nearby Search

```csharp
var request = new NearbyRequestBuilder()
    .WithTypes(["restaurant"])
    .WithLocationRestriction(500, 40.7128, -74.0060)
    .WithMaxResults(5)
    .Build();

var results = await places.Search.SearchByNearbyAsync(request);
```

---

## 📦 Models

### PlaceSearchResult

Represents a place returned from a search query.

```csharp
public sealed class PlaceSearchResult {
    public string? PlaceId { get; internal set; }
    public string? DisplayName { get; internal set; }
    public IReadOnlyList<string> Types { get; internal set; } = [];
}
```

#### ⚙️ Properties

| Property      | Description                    |
|:------------- |:------------------------------ |
| `PlaceId`     | Unique identifier of the place |
| `DisplayName` | Name of the place              |
| `Types`       | Associated place types         |


> 💡 Use [IDetailsService](/docs/places/Abstractions.md#-idetailsservice) if you need richer information.

---

## 🧾 Request

### NearbyRequest

Represents a request for retrieving place suggestions.

```csharp
public sealed class NearbyRequest {
    public IReadOnlyList<string> IncludedTypes { get; internal set; } = [];
    public int MaxResultCount { get; internal set; } = 1;
    public LocationRestriction LocationRestriction { get; internal set; } = new();
}
```

#### ⚙️ Properties

| Property              | Description                                  |
|:--------------------- |:-------------------------------------------- |
| `IncludedTypes`       | List of place types to include in the search |
| `MaxResultCount`      | Maximum number of results to return          |
| `LocationRestriction` | Geographic restriction applied to the search |

#### 🧠 Notes

- Must be constructed using `NearbyRequestBuilder`

### NearbyRequestBuilder

Provides a fluent API for constructing nearby search requests.

```csharp
public sealed class NearbyRequestBuilder {
    public NearbyRequestBuilder WithTypes(IEnumerable<string> includedTypes);
    public NearbyRequestBuilder WithMaxResults(int maxResults);
    public NearbyRequestBuilder WithLocationRestriction(
        double radius, double latitude, double longitude
    );

    public NearbyRequest Build();
}
```

#### ⚙️ Configuration

- `WithTypes(...)` → **required**, defines place types (e.g., restaurant, cafe)
- `WithMaxResults(...)` → **optional**, limits number of results
- `WithLocationRestriction(...)` → **required**, defines search area

#### ⚠️ Validation

- Types must be provided
- Location restriction must be defined
- Max results must be greater than 0
