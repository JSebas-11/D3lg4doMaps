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

Core infrastructure and shared components.
✔ Includes:
- IMapsApiClient (HTTP client wrapper)
- IRequestBuilder, IMapsUriBuilder, IRequestFactory
- MapsConfiguration + DI extension
- Custom exceptions
- JSON serializer
- Logging support (CORE LOGGING not implemented yet)

## 2. D3lg4doMaps.Places (Upcoming)

A high-level wrapper over the Places API (New).

Planned features:
- Text Search
- Nearby Search
- Autocomplete
- Place Details
- Place Photos
  
Depends on:
D3lg4doMaps.Core

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

# 🚀 Getting Started

1. Install Core
```
dotnet add package D3lg4doMaps.Core
```

2. Register the SDK
```
services.AddD3lg4doMaps(new MapsConfiguration {
    ApiKey = "YOUR_API_KEY",
    Language = "en",
    Region = "US",
    TimeOutSeconds = 30,
});
```

3. Inject & use the API client
```
public class MyService {
    private readonly IMapsApiClient _client;

    public MyService(IMapsApiClient client) {
        _client = client;
    }

    public async Task DoSomethingAsync() {
        var request = new MapsApiRequest () {
            Method = HttpMethod.Post,
            BaseUrl = "https://places.googleapis.com",
            Endpoint = "/v1/places:searchText",
            Payload = new { textQuery = "coffee shops" }
        };

        var result = await _client.SendAsync<MyResponseModel>(request);
    }
}
```

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

