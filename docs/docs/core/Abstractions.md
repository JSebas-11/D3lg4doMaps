---
title: Abstractions
---

# 📦 Core Abstractions

The Core package defines a set of abstractions that represent the internal
contracts of the SDK.

These abstractions define how the SDK:

- Executing HTTP requests
- Handling serialization and deserialization
- Translating SDK models into HTTP messages

> ⚠️ These interfaces are primarily intended for internal use, but are exposed
> for extensibility and advanced customization scenarios.

---

## 🧩 IMapsApiClient

Defines a client responsible for sending requests to a Maps API endpoint.

```csharp
public interface IMapsApiClient {
    Task<T> SendAsync<T>(MapsApiRequest apiRequest);
}
```

### ⚙️ Responsibilities

- Execute HTTP requests
- Handle request/response lifecycle
- Deserialize responses into strongly-typed models

### 🧠 Notes

- This is the **core entry point for all API communication**
- Used internally by higher-level services (e.g., Places, Routing)

---

## 🔄 IMapsJsonSerializer

Provides JSON serialization and deserialization functionality.

```csharp
public interface IMapsJsonSerializer {
    string Serialize(object value);
    T? Deserialize<T>(string json);
}
```

### ⚙️ Responsibilities

- Serialize request payloads
- Deserialize API responses
- Abstract JSON library choice

### 🧠 Notes

- Allows replacing the default serializer (e.g., System.Text.Json, Newtonsoft.Json)
- Ensures consistent JSON handling across the SDK

---

## 🏗️ IRequestFactory

Creates HTTP requests from SDK request definitions.

```csharp
public interface IRequestFactory {
    HttpRequestMessage CreateRequest(MapsApiRequest request);
}
```

### ⚙️ Responsibilities

- Convert [MapsApiRequest](Models#-mapsapirequest) into `HttpRequestMessage`
- Configure:
    - URL
    - HTTP method
    - Headers
    - Query parameters
    - Body content
    - API key placement (header or query parameter)

### 🧠 Notes

- Decouples request construction from execution
- Improves testability and separation of concerns

---

##  📝 Design Considerations

These abstractions follow a few key principles:

- Separation of concerns → each interface has a **single responsibility**
- Extensibility → can be replaced or customized if needed
- Testability → easy to mock in unit tests

---

## ⚠️ When should you use these?

In most cases, **you do not need to interact with these abstractions directly.**

Instead, use higher-level services like:

- **IPlacesService** (ISearchService, IAutocompleteService, IDetailsService)  
👉 See: [IPlacesService](/docs/places/Abstractions.md#-iplacesservice)
- **IRoutingService** (IDirectionsService, IDistanceMatrixService)  
👉 See: []()

Use these abstractions only if you need:

- Custom HTTP handling
- Custom serialization behavior
- Advanced SDK extension