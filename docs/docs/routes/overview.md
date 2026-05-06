---
sidebar_position: 1
title: Routes Overview
---

:::info Namespace update (v2.0.0)
Namespaces were changed, update your `using` statements accordingly.

👉 See the full migration guide here: [v2 Migration Guide](/docs/migration/v2.md)
:::

:::info Injection update (v3.0.0)
Dependency injection methods and configuration registration were updated.

👉 See the full migration guide here: [v3 Migration Guide](/docs/migration/v3.md)
:::

# 🧭 Routes Overview

The **Routes** module provides a powerful and developer-friendly way to compute routes and travel distances between locations.

It wraps the Google Routes API and exposes **strongly-typed, flexible abstractions** for both Compute Route (Directions) and Compute Route Matrix (Distance Matrix).

---

## 🚀 What You Can Do

- 🧭 Compute routes between locations with turn-by-turn data  
- 📏 Calculate distances and durations across multiple origins and destinations  
- 🚗 Customize routing behavior (traffic, tolls, vehicle type, etc.)  
- 🔀 Generate alternative routes and optimize waypoint order  
- 🌍 Work with localized, human-readable route data  
- ⚡ Improve API efficiency with optional HTTP-layer caching

---

## 🧩 Architecture

The module is built around two main services:

- `IDirectionsService` → Computes detailed routes between locations  
- `IDistanceMatrixService` → Computes distances across origin-destination pairs  

Both services share a common set of **routing components** and **metadata models**.

> 👉 See: [Abstractions](/docs/routes/abstractions.md)

---

## ⚡ Quick Example

```csharp
var services = new ServiceCollection();

services.AddDelgadoMaps(opts => {
    opts.ApiKey = "YOUR_API_KEY";
});
services.AddDelgadoMapsRoutes();

var provider = services.BuildServiceProvider();

var directions = provider.GetRequiredService<IDirectionsService>();

var origin = new WaypointBuilder()
    .FromPlaceId("place_id_1")
    .Build();

var destination = new WaypointBuilder()
    .FromPlaceId("place_id_2")
    .Build();

var request = new RouteRequestBuilder()
    .From(origin)
    .To(destination)
    .WithTravelMode(TravelMode.Drive)
    .Build();

var result = await directions.GetRoutesAsync(request);

Console.WriteLine(result.BestRoute?.DistanceMeters);
```

---

## 🧠 Design Highlights

- Clear separation between **Directions** and **Distance Matrix**
- Strongly-typed models for safe and predictable usage
- Fluent builders to prevent invalid requests
- Shared routing components across features
- Modular metadata system for scalable route representation
- Async-first design for performance

---

## 🧱 Core Concepts

### 🧭 Directions

Used when you need **full route navigation data**:

- Route geometry (polylines)
- Legs and steps (turn-by-turn navigation)
- Route metadata (advisories, warnings, localized values)

👉 See: [Directions Service](/docs/routes/services/directions.md)

### 📏 Distance Matrix

Used when you need **distance, duration, etc between many points:**

- Multiple origins and destinations
- Optimized for bulk computations
- Lightweight compared to full directions

👉 See: [Distance Matrix](/docs/routes/services/distanceMatrix.md)

### 🧩 Routing Components

Reusable building blocks used across requests:

- Waypoints and locations
- Route modifiers (avoid tolls, highways, etc.)
- Transit preferences
- Vehicle information

👉 See: [Routing Components](/docs/routes/routing-components.md)

### 📦 Route Metadata

Rich, structured data returned from routing operations:

- Routes, legs, and steps
- Geometry (polylines, viewport)
- Travel advisories and traffic data
- Localized display values

👉 See: [Route Metadata](/docs/routes/route-metadata.md)

---

## 💡 When to Use What?

- Use **Directions** when you need full navigation data
- Use **Distance Matrix** when you need bulk distance calculations

---

## 🔗 Next Steps

- 👉 Directions → [Directions Service](/docs/routes/services/directions.md)
- 👉 Distance Matrix → [Distance Matrix](/docs/routes/services/distanceMatrix.md)
- 👉 Dependency Injection → [Routes Injection](/docs/routes/extensions.md#-dependency-injection)
- 👉 HTTP caching → [Caching Injection](/docs/core/extensions.md#-http-caching-injection)