---
title: Core Overview
sidebar_position: 1
---

:::info Namespace update (v2.0.0)
Namespaces were changed, update your `using` statements accordingly.

👉 See the full migration guide here: [v2 Migration Guide](/docs/migration/v2.md)
:::

:::info Injection update (v3.0.0)
Dependency injection methods and configuration registration were updated.

👉 See the full migration guide here: [v3 Migration Guide](/docs/migration/v3.md)
:::

# 🧩 Core Overview

The **Core** package provides the foundational infrastructure for the entire SDK.

It is responsible for handling:

- Configuration
- HTTP communication
- Serialization
- Dependency injection
- HTTP-layer caching
- Error handling

All feature modules (such as Places and Routes) are built on top of Core.

---

## 🏗️ Architecture Role

Core acts as the **central layer** of the SDK:

- Defines shared contracts (abstractions)
- Implements low-level communication with Google Maps APIs
- Provides reusable infrastructure for all modules
- Centralizes SDK configuration and caching

> 📌 You only configure Core once — everything else builds on top of it.

---

## 📦 What’s inside Core

### 🧾 Configuration

Centralized SDK configuration:

- API Key
- Language and region
- Request timeout
- HTTP-layer caching
- Logging *(planned)*

👉 See: [Configuration](configuration)

---

### 🧩 Dependency Injection

Registers all required services into your application:

- HTTP client
- Serialization
- Builders and factories
- Cache infrastructure

👉 See: [Dependency Injection](extensions#-dependency-injection)

---

### 🧱 Abstractions

Core contracts that define how the SDK works internally:

- `IMapsApiClient`
- `IMapsJsonSerializer`
- `IRequestFactory`

👉 See: [Abstractions](abstractions)

---

### 📦 Models

Shared data structures used across the SDK:

- `MapsApiRequest`
- `StreamResponse`
- `LatLng`

👉 See: [Models](models)

---

### 📚 Enums

Common enumerations:

- `ApiKeyLocation`

👉 See: [Enums](enums)

---

### 🚨 Exceptions

Strongly-typed error handling model:

- `MapsApiException` (base)
- Specialized exceptions (Auth, RateLimit, Cache, etc.)

👉 See: [Exceptions](exceptions)

---

### 🛠️ Extensions

Helper utilities and integration helpers:

- Dependency injection extensions
- Cache registration extensions
- JSON parsing helpers

👉 See: [Extensions](extensions)

---

## ⚡ Typical Flow

Using the SDK follows a simple pattern:

```csharp
var services = new ServiceCollection();

// 1. Configure Core
services.AddDelgadoMaps(opts => {
    opts.ApiKey = "YOUR_API_KEY";
});

// 2. Optional cache layer
services.AddDelgadoMapsMemoryCache(opts => {
    opts.AbsoluteExpiration = TimeSpan.FromMinutes(30);
});

// 3. Register feature modules
services.AddDelgadoMapsPlaces();

// 4 Resolve and use services
var provider = services.BuildServiceProvider();
var places = provider.GetRequiredService<IPlacesService>();

// Or inject directly into your services
public class MyService {
    private readonly IPlacesService _placesService;

    public MyService(IPlacesService placesService) {
        _placesService = placesService;   
    }
}
```

---

## 🧠 Design Principles

Core is built around a few key ideas:

- **Centralization** → one configuration, shared everywhere
- **Separation of concerns** → clear boundaries between components
- **Extensibility** → replace internal pieces if needed
- **Developer experience** → minimal setup, clean API
- **Modularity** → optional infrastructure such as caching

---

## 🔗 Next Step

Now that Core is configured, you can start using a feature module:

- 👉 See: [Places Overview](/docs/places/overview)
- 👉 See: [Routes Overview](/docs/routes/overview)