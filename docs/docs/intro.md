---
sidebar_position: 1
title: Introduction
---

:::info Namespace update (v2.0.0)
Namespaces were changed, update your `using` statements accordingly.

👉 See the full migration guide here: [v2 Migration Guide](/docs/migration/v2.md)
:::

:::info Injection update (v3.0.0)
Dependency injection methods and configuration registration were updated.

👉 See the full migration guide here: [v3 Migration Guide](/docs/migration/v3.md)
:::

# D3lg4doMaps

**D3lg4doMaps** is a modern, modular .NET SDK that simplifies interaction with Google Maps APIs.

It provides clean, strongly-typed abstractions over complex HTTP endpoints, allowing you to focus on building features instead of handling low-level API details.

---

## 🚀 Why D3lg4doMaps?

Working directly with Google Maps APIs can be:

- Verbose
- Error-prone
- Hard to maintain

D3lg4doMaps solves this by:

- ✅ Centralizing configuration in a Core module  
- ✅ Providing high-level services (Places, Routes, etc.)  
- ✅ Enforcing clean architecture and best practices  
- ✅ Offering a fluent and developer-friendly API  
- ✅ Supporting optional HTTP-layer caching  
- ✅ Integrating seamlessly with .NET dependency injection  

---

## 🧩 Architecture Overview

D3lg4doMaps is built around a **Core-first architecture**:

- **Core** → Configuration, HTTP handling, shared infrastructure  
- **Places** → Search, Autocomplete, Details, Reviews, Photos  
- **Routes** → Compute Routes (Directions), Compute Route Matrix (DistanceMatrix)

All feature modules depend on Core, ensuring consistency and simplicity.

---

## ⚡ Quick Example

```csharp
// CONFIGURATION
var services = new ServiceCollection();

services.AddDelgadoMaps(opts => {
    opts.ApiKey = "YOUR_API_KEY";
});

services.AddDelgadoMapsMemoryCache(opts => {
    opts.AbsoluteExpiration = TimeSpan.FromMinutes(30);
});

services.AddDelgadoMapsPlaces();
services.AddDelgadoMapsRoutes();

var provider = services.BuildServiceProvider();

// PLACES
var places = provider.GetRequiredService<IPlacesService>();

var medellin = await places.Search
    .SearchByTextAsync("medellin, co");

var bogota = await places.Search
    .SearchByTextAsync("bogota, co");

// ROUTES
var routes = provider.GetRequiredService<IRoutesService>();

var origin = new WaypointBuilder()
    .FromPlaceId(medellin[0].PlaceId!)
    .Build();

var destination = new WaypointBuilder()
    .FromPlaceId(bogota[0].PlaceId!)
    .Build();

var request = new RouteRequestBuilder()
    .From(origin)
    .To(destination)
    .Build();

var result = await routes.Directions
    .GetRoutesAsync(request);
```

---

## 📦 Installation

Install via NuGet:

```bash
dotnet add package D3lg4doMaps.Core
dotnet add package D3lg4doMaps.Places
dotnet add package D3lg4doMaps.Routes
```

---

## 🛠️ Next Steps

- 👉 Configure Core → [Core Overview](core/overview)
- 👉 Explore Places → [Places Overview](places/overview)
- 👉 Explore Routes → [Routes Overview](routes/overview)

---

## 💡 Design Philosophy

D3lg4doMaps follows a few key principles:

- **Simplicity first** → minimal boilerplate
- **Strong typing** → fewer runtime errors
- **Modularity** → use only what you need
- **Clean abstractions** → no API noise
- **Extensibility** → infrastructure designed for growth

---

## 🔗 Links

- GitHub: https://github.com/JSebas-11/D3lg4doMaps
- NuGet: https://www.nuget.org/packages?q=D3lg4doMaps