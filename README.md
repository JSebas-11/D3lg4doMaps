# 📦 D3lg4doMaps SDK

A modern, strongly-typed C# SDK for Google Maps Platform (Places, Directions, Distance Matrix, and more).

D3lg4doMaps is a modular .NET SDK built to make Google Maps Platform integration clean, consistent, and easy to maintain.
It abstracts the low-level HTTP logic, unifies configuration, and provides typed services for interacting with different Maps APIs.

## ✨ Features

- 🔌 Dependency Injection ready
- 🚀 Unified HTTP engine (builders, factories, serializer)
- 🔐 Automatic authentication headers
- 🌍 Language & region defaults
- 📡 Strongly typed requests/responses
- 🛠 Custom exception mapping
- 📜 Centralized configuration
- 🪵 Structured logging integration
- 🧱 Modular architecture (Core, Places, Routing, …)

--- 

# 📦 Packages

## 1. D3lg4doMaps.Core

Provides the shared infrastructure used by all SDK modules, including:
- 🌐 HTTP communication with Google Maps APIs
- 🧱 Request building and execution
- 🔄 JSON serialization
- ⚙️ Centralized configuration
- 🚨 Exception mapping
- 🪵 Logging integration (logging pipeline planned)

⚠ Note: This package is **not intended to be used standalone.**
Instead, it acts as the foundation for higher-level modules such as d3lg4doMaps.Places and d3lg4doMaps.Routing


### 🛠 Configuration & Dependency Injection
The SDK is configured using MapsConfiguration and integrates seamlessly with the ASP.NET Core dependency injection system.

Register the SDK in your Program.cs or Startup.cs:
```csharp
services.AddD3lg4doMaps(new MapsConfiguration {
    ApiKey = "YOUR_API_KEY",
    Language = "en",
    Region = "US",
    TimeOutSeconds = 30,
});
```
| Property |	Description |
| --- | --- |
| ApiKey |	Google Maps Platform API key |
| Language |	Default language for API responses |
| Region	| Regional bias for results |
| TimeOutSeconds |	HTTP request timeout |

### 📡 MapsApiRequest

All API calls in the SDK are represented by the MapsApiRequest model.
This model defines the structure used by the internal HTTP engine to communicate with Google Maps APIs.

| Property |	Description |
| --- | --- |
| Method |	HTTP method used for the request |
| BaseUrl |	Google Maps API base URL |
| Endpoint	| Specific API endpoint |
| Headers |	Request headers |
| Query |	Request body |
| Payload |	Optional query parameters |

---

## D3lg4doMaps.Places

A high-level wrapper for the **Google Places API (New)**.

This module provides strongly-typed services, request builders, and models for interacting with place data such as search results, autocomplete suggestions, and place details.

### Features

- 🔎 **Text Search** – Find places using natural text queries
- 📍 **Nearby Search** – Discover places around a location
- ✨ **Autocomplete** – Get place predictions while typing
- 🏢 **Place Details** – Retrieve detailed information about a place
- 📷 **Place Photos** – Access images associated with places
- ⭐ **Place Reviews** – User ratings and review summaries

**Depends on: D3lg4doMaps.Core**

---

# 🛠 Dependency Injection

Register the Places module alongside the Core SDK.

```csharp
services.AddD3lg4doMaps(configuration);
services.AddD3lg4doMapsPlaces();
```

## 3. D3lg4doMaps.Routing (Planned)

Unified access to:
- Directions API
- Distance Matrix API

Planned features:
- Route calculations
- Traffic-aware routing
- Distance/time matrix queries
- Support for modes, waypoints, alternatives

---

# ⚠ Error Handling

The SDK maps common Google Maps errors into custom exceptions:
| HTTP Code |	Exception |
| --- | --- |
| 401/403 |	MapsApiAuthException |
| 404 |	MapsNotFoundException |
| 400	| MapsInvalidRequestException |
| 429 |	MapsRateLimitException |
| Others |	MapsApiException |

---

# 🤝 Contributing
Contributions are welcome!
- Open issues
- Submit PRs
- Suggest improvements
- Propose new modules

---

# 📄 License
This project is licensed under the MIT License. See the `LICENSE` file for details.

---

# 🙌 Credits
Created & maintained by JSebas-11 (Sebastian Delgado)
Designed for clean architecture, developer ergonomics, and real-world Maps integrations.

