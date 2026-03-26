---
title: Core Overview
sidebar_position: 1
---

# 🧩 Core Overview

The **Core** package provides the foundational infrastructure for the entire SDK.

It is responsible for handling:

- Configuration
- HTTP communication
- Serialization
- Dependency injection
- Error handling

All feature modules (such as Places and Routing) are built on top of Core.

---

## 🏗️ Architecture Role

Core acts as the **central layer** of the SDK:

- Defines shared contracts (abstractions)
- Implements low-level communication with Google Maps APIs
- Provides reusable infrastructure for all modules

> 📌 You only configure Core once — everything else builds on top of it.

---

## 📦 What’s inside Core

### 🧾 Configuration

Centralized SDK configuration:

- API Key
- Language and region
- Request timeout
- Logging *(planned)*
- MemoryCache *(planned)*

👉 See: [Configuration](configuration)

---

### 🧩 Dependency Injection

Registers all required services into your application:

- HTTP client
- Serialization
- Builders and factories

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
- Specialized exceptions (Auth, RateLimit, etc.)

👉 See: [Exceptions](exceptions)

---

### 🛠️ Extensions

Helper utilities and integration helpers:

- Dependency injection extensions
- JSON parsing helpers

👉 See: [Extensions](extensions)

---

## ⚡ Typical Flow

Using the SDK follows a simple pattern:

```csharp
var services = new ServiceCollection();

// 1. Configure Core
services.AddD3lg4doMaps(new MapsConfiguration {
    ApiKey = "YOUR_API_KEY"
});

// 2. Register feature modules
services.AddD3lg4doMapsPlaces();

// 3.1 Resolve and use services
var provider = services.BuildServiceProvider();
var places = provider.GetRequiredService<IPlacesService>();

// 3.2 Receive services into your modules
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

---

## 🔗 Next Step

Now that Core is configured, you can start using a feature module:

- 👉 See: [Places Overview](/docs/places/overview)
- 👉 See: [Routing Overview](/docs/routing/overview)