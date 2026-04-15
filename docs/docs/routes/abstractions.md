---
title: Abstractions
---

# 🧩 Routes Abstractions

The Routes module defines a set of service abstractions that provide access  
to Google Routes functionality.

These services expose high-level operations such as:

- Route computation (Directions)  
- Distance matrix calculations  
- Travel time and distance estimation  

> ⚠️ These interfaces are typically consumed through `IRoutesService`.

---

## 🧾 IRoutesService

Provides a unified entry point for all routing functionality.

```csharp
public interface IRoutesService {
    IDirectionsService Directions { get; }
    IDistanceMatrixService DistanceMatrix { get; }
}
```

### ⚙️ Responsibilities

- Aggregate all Routes services
- Provide a **single access point** for consumers

### 🧠 Notes

- Recommended entry point for most use cases
- Avoids injecting multiple services individually

--- 

## 🧭 IDirectionsService

Provides functionality for **calculating routes between locations**.

This service represents the core **routing workflow**, where you define an origin, destination, and optional parameters to compute optimized routes.

```csharp
public interface IDirectionsService {
    Task<RouteResult> GetRoutesAsync(
        RouteRequest routeRequest,
        RouteDetailLevel detailLevel = RouteDetailLevel.Standard
    );

    Task<JsonDocument> GetRoutesRawAsync(
        RouteRequest routeRequest,
        params string[] fields
    );
}
```

### ⚙️ Responsibilities

- Compute routes between locations
- Support multiple levels of detail → see [RouteDetailLevel](/docs/routes/enums.md#routedetaillevel)
- Provide both **strongly-typed** and **raw JSON** responses

👉 See: [RouteRequest](/docs/routes/services/directions.md#routerequest)

### 🧠 Notes

- Use `Summary` for lightweight responses
- Use `Full` for detailed navigation data (legs, steps, polyline, etc.)
- Returns structured results → see [RouteResult](/docs/routes/services/directions.md#routeresult)

### ⚠️ Important

The returned JsonDocument must be **disposed** properly.

```csharp
using var json = await directionsService.GetRoutesRawAsync(request);
```

---

## 📏 IDistanceMatrixService

Provides functionality for computing **distances and travel times between** multiple origins and destinations.

This service is designed for **matrix-based scenarios**, where you need pairwise comparisons between locations.

```csharp
public interface IDistanceMatrixService {
    Task<IReadOnlyList<RouteMatrixElement>> GetDistancesAsync(
        DistanceRequest distanceRequest,
        RouteDetailLevel detailLevel = RouteDetailLevel.Standard
    );
}
```

### ⚙️ Responsibilities

- Compute distances and durations between location pairs
- Support bulk operations (multiple origins × destinations)
- Provide structured matrix results
- Support multiple levels of detail → see [RouteDetailLevel](/docs/routes/enums.md#routedetaillevel)

👉 See: [DistanceRequest](/docs/routes/services/distanceMatrix.md#distancerequest)

### 🧠 Notes

- Ideal for:
    - Logistics and delivery systems
    - Travel time estimation
    - Route comparison and optimization
- Returns one element per origin-destination pair → see [RouteMatrixElement](/docs/routes/services/distanceMatrix.md#routematrixelement)
- Internally optimized using **streaming responses** for performance
