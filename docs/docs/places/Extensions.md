---
title: Extensions
---

## 💉 Dependency Injection

The **Places module** integrates with .NET dependency injection through an extension method.

This registers all services required to interact with the Google Places API.

> ⚠️ Core must be registered first  
> 👉 See: [Core Dependency Injection](/docs/core/Extensions.md#-dependency-injection)

Registers all Places services into the DI container.

```csharp
public static IServiceCollection AddD3lg4doMapsPlaces(this IServiceCollection services);
```

### ⚡ Basic Usage

```csharp
services.AddD3lg4doMaps(configuration);
services.AddD3lg4doMapsPlaces();
```

### 💡 Behavior

- Each request gets a **new instance** of the service
- Services are **lightweight and stateless**
- Designed to work seamlessly with ASP.NET Core DI

### ⚠️ Validation

Places depends on Core (HTTP, config, serializartion), therefore this method validates that Core services are already registered. If not, it throws [MapsApiException](/docs/core/Exceptions.md#-mapsapiexception)

### 🧠 Notes

- Always register Core before Places
- Places is built on top of Core abstractions
- Keeps configuration centralized and consistent