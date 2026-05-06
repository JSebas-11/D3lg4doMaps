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
public static IServiceCollection AddDelgadoMapsRoutes(this IServiceCollection services);
```

### ⚡ Basic Usage

```csharp
services.AddDelgadoMaps(configuration);
services.AddDelgadoMapsRoutes();
```

### ⚡ HTTP Caching

Routes automatically integrates with the Core HTTP caching layer when enabled.

This helps reduce repeated API calls and improve response performance.

👉 See: [Core Caching Extensions](/docs/core/extensions.md#-http-caching-injection)

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