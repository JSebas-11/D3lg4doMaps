---
title: Extensions
---

## 💉 Dependency Injection

The **Routes module** integrates with .NET dependency injection through an extension method.

This registers all services required to interact with the Google Routes API, including:

- Compute Routes (Directions)
- Compute Route Matrix (Distance Matrix)

> ⚠️ Core must be registered first  
> 👉 See: [Core Dependency Injection](/docs/core/extensions.md#-dependency-injection)

Registers all Routes services into the DI container.

```csharp
public static IServiceCollection AddD3lg4doMapsRoutes(this IServiceCollection services);
```

### ⚡ Basic Usage

```csharp
services.AddD3lg4doMaps(configuration);
services.AddD3lg4doMapsRoutes();
```

### 💡 Behavior

- Services are registered with **Transient** lifetime
- Each request gets a **new instance**
- Services are **lightweight and stateless**

### ⚠️ Validation

Routes depends on Core (HTTP, configuration, serialization). This method ensures Core is already registered.
If not, it throws: [MapsApiException](/docs/core/exceptions.md#-mapsapiexception)

### 🧠 Notes

- Always register Core before Routes
- Routes is built on top of Core abstractions
- Keeps configuration centralized and consistent