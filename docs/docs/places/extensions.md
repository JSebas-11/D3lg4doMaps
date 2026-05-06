---
title: Extensions
---

## 💉 Dependency Injection

The **Places module** integrates with .NET dependency injection through an extension method.

This registers all services required to interact with the Google Places API.

> ⚠️ Core must be registered first  
> 👉 See: [Core Dependency Injection](/docs/core/extensions.md#-dependency-injection)

Registers all Places services into the DI container.

```csharp
public static IServiceCollection AddDelgadoMapsPlaces(this IServiceCollection services);
```

### ⚡ Basic Usage

```csharp
services.AddDelgadoMaps(configuration);
services.AddDelgadoMapsPlaces();
```

### ⚡ HTTP Caching

Places automatically integrates with the Core HTTP caching layer when enabled.

This helps reduce repeated API calls and improve response performance.

👉 See: [Core Caching Extensions](/docs/core/extensions.md#-http-caching-injection)

### 💡 Behavior

- Each request gets a **new instance** of the service
- Services are **lightweight and stateless**
- Designed to work seamlessly with ASP.NET Core DI

### ⚠️ Validation

Places depends on Core (HTTP, config, serializartion), therefore this method validates that Core services are already registered. If not, it throws [MapsApiException](/docs/core/exceptions.md#-mapsapiexception)

### 🧠 Notes

- Always register Core before Places
- Places is built on top of Core abstractions
- Keeps configuration centralized and consistent