# 📦 D3lg4doMaps SDK

A modern, strongly-typed .NET SDK for Google Maps Platform (Places, Routes, Distance Matrix, and more).

---

## 🚀 Why D3lg4doMaps?

Working with Google Maps APIs directly can be verbose and error-prone.

D3lg4doMaps provides:

- Strongly-typed models
- Clean abstractions
- Fluent request builders
- Optional HTTP-layer caching (supports in-memory and distributed providers)
- Modular architecture (Core, Places, Routes)

---

## 📚 Documentation

👉 Full documentation: https://jsebas-11.github.io/D3lg4doMaps/

Includes:

- Getting Started
- Configuration
- Places (Search, Autocomplete, Details)
- Routes (Directions, Distance Matrix)
- Advanced usage

---

## ⚡ Quick Example

```csharp
var services = new ServiceCollection();

services.AddDelgadoMaps(opts => {
    opts.ApiKey = "YOUR_API_KEY"
});

services.AddDelgadoMapsMemoryCache(opts => {
    opts.Prefix = "d3lg4doMaps";
    opts.AbsoluteExpiration = TimeSpan.FromMinutes(30);
 });
 
services.AddDelgadoMapsPlaces();
services.AddDelgadoMapsRoutes();

var provider = services.BuildServiceProvider();

// PLACES
var places = provider.GetRequiredService<IPlacesService>();
var cafes = await places.Search.SearchByTextAsync("coffee near me");

// ROUTES
var routes = provider.GetRequiredService<IRoutesService>();

var request = new RouteRequestBuilder()
    .From(new WaypointBuilder().FromPlaceId("PLACE_ID_A").Build())
    .To(new WaypointBuilder().FromPlaceId("PLACE_ID_B").Build())
    .Build();

var result = await routes.Directions.GetRoutesAsync(request);
```

---

## 📦 Installation

```bash
dotnet add package D3lg4doMaps.Core
dotnet add package D3lg4doMaps.Places
dotnet add package D3lg4doMaps.Routes
```

--- 

## 🧩 Modules

- Core → Configuration, dependency injection, HTTP infrastructure, caching
- Places → Search, Autocomplete, Details
- Routes → Directions, Distance Matrix

---

## ⚠️ Breaking Changes

### v2.0.0

- `D3lg4doMaps.*` → `DelgadoMaps.*`
- Removed `.Public` namespace segment

👉 [Migration guide](https://jsebas-11.github.io/D3lg4doMaps/docs/migration/v2)

### v3.0.0

- `AddD3lg4doMaps...*` → `AddDelgadoMaps...*`
- Configuration registration now uses the `IOptions` pattern
- Request timeout type changed from `int` to `TimeSpan`

👉 [Migration guide](https://jsebas-11.github.io/D3lg4doMaps/docs/migration/v3)

---

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

---

## 📄 License
This project is licensed under the MIT License. See the `LICENSE` file for details.

---

## 🙌 Credits
Created & maintained by JSebas-11 (Sebastian Delgado)
Designed for clean architecture, developer ergonomics, and real-world Maps integrations.

