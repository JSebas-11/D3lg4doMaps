---
title: Extensions
---

# 🧩 Core Extensions

The Core package provides a set of extension methods to integrate the SDK
into your application and simplify common tasks.

These extensions cover:

- Dependency injection setup
- HTTP Caching setup
- JSON parsing helpers

They are designed to improve developer experience by reducing boilerplate
and providing safe, reusable patterns.

---

## 💉 Dependency Injection 

The SDK integrates with .NET dependency injection through extension methods.

The `AddDelgadoMaps` method registers all required Core services, including:

- HTTP client
- Serialization
- Request builders and factories

Registers the core services required to use the SDK.

```csharp
public static IServiceCollection AddDelgadoMaps(
    this IServiceCollection services, MapsConfiguration config
)
```

### ⚡ Basic Usage

```csharp
var services = new ServiceCollection();

services.AddDelgadoMaps(opts => {
    opts.ApiKey = "YOUR_API_KEY";
});
```

### ⚙️ Registered Services

- `IMapsApiClient` → HTTP communication layer
- `IMapsJsonSerializer` → JSON serialization
- `IRequestFactory` → request creation
- Internal builders and utilities

### ⚠️ Validation

Throws [MapsApiAuthException](exceptions#-mapsapiauthexception) if `ApiKey` is null or empty

### 🧠 Notes

- [MapsConfiguration](configuration#-mapsconfiguration) is registered as a **singleton**
- `HttpClient` is configured with the provided timeout
- Services use a mix of:
    - Singleton → shared configuration and utilities
    - Transient → lightweight builders and factories
- This method only registers **Core services**
- Feature modules (e.g., Places, Routing) must be registered separately

---

## ⛃ HTTP Caching Injection

The SDK provides optional HTTP-layer caching for standard request/response endpoints.

Supported cache providers:

- In-memory cache (`IMemoryCache`)
- Distributed cache (`IDistributedCache`)
    - Redis
    - SQL Server
    - NCache
    - custom providers

Caching is shared across all SDK modules:

- Core
- Places
- Routes

> ⚠️ Stream endpoints such as `ComputeRouteMatrix`
> (`DistanceMatrixService`) are not cached.

### 🧠 Notes

- Caching is optional
- SDK configuration via [CachingConfiguration](/docs/core/configuration.md#-mapscachingoptions)
- Only one cache layer can be registered at a time
- Cache keys are automatically generated per request fingerprint
- Cache configuration is shared across all SDK modules
- Distributed cache implementations must be registered before calling:
    - AddDelgadoMapsDistributedCache()

---

## 💾 In-Memory Cache

Registers an in-memory caching layer using `IMemoryCache` and configured via [MapsCachingOptions](/docs/core/configuration.md#-mapscachingoptions).

```csharp
public static IServiceCollection AddDelgadoMapsMemoryCache(
    this IServiceCollection services,
    Action<MapsCachingOptions> cachingOpts
)
```

### ⚡ Example

```csharp
var services = new ServiceCollection();

services.AddDelgadoMaps(opts => {
    opts.ApiKey = "YOUR_API_KEY";
});

services.AddDelgadoMapsMemoryCache(opts => {
    opts.Prefix = "d3lg4doMaps";
    opts.AbsoluteExpiration = TimeSpan.FromMinutes(30);
    opts.SlidingExpiration = TimeSpan.FromMinutes(10);
});
```

### ⚠️ Validation

Throws:

- [MapsApiException](/docs/core/exceptions.md#-mapsapiexception) when:
    - Core services were not registered
- [MapsCacheException](/docs/core/exceptions.md#-mapscacheexception) when:
    - Another cache layer already exists
    - Cache configuration is invalid

---

## 🌐 Distributed Cache

Registers a distributed caching layer using an existing `IDistributedCache` implementation and SDK configuration via [MapsCachingOptions](/docs/core/configuration.md#-mapscachingoptions).

```csharp
public static IServiceCollection AddDelgadoMapsDistributedCache(
    this IServiceCollection services,
    Action<MapsCachingOptions> cachingOpts
)
```

### ⚡ Example (Redis)

```csharp
services.AddStackExchangeRedisCache(opts => {
    opts.Configuration = "localhost:6379";
});

services.AddDelgadoMaps(opts => {
    opts.ApiKey = "YOUR_API_KEY";
});

services.AddDelgadoMapsDistributedCache(opts => {
    opts.Prefix = "d3lg4doMaps";
    opts.AbsoluteExpiration = TimeSpan.FromMinutes(30);
});
```

### ⚠️ Validation

Throws:

- [MapsApiException](/docs/core/exceptions.md#-mapsapiexception) when:
    - Core services were not registered
- [MapsCacheException](/docs/core/exceptions.md#-mapscacheexception) when:
    - `IDistributedCache` was not registered
    - Another cache layer already exists
    - Cache configuration is invalid

---

## 🧾 JsonExtensions

Provides helper extension methods for safely reading values from `JsonElement`.

These methods simplify JSON parsing by:

- Avoiding repetitive property existence checks  
- Avoiding `JsonValueKind` validation boilerplate  
- Returning safe defaults instead of throwing exceptions  

```csharp
public static class JsonExtensions {
    public static JsonElement? GetObject(this JsonElement element, string prop) { ... }
    public static int? GetInt(this JsonElement element, string prop) { ... }
    public static float? GetFloat(this JsonElement element, string prop) { ... }
    public static double? GetDoubleValue(this JsonElement element, string prop) { ... }
    public static bool? GetBool(this JsonElement element, string prop) { ... }
    public static string? GetStringValue(this JsonElement element, string prop) { ... }
    public static IEnumerable<JsonElement> GetArray(this JsonElement element, string prop) { ... }
    public static List<string> GetArrayStringValues(this JsonElement json, string prop) { ... }
}
```

### ⚙️ Responsibilities

- Safely extract values from JSON responses
- Combine property lookup and type validation
- Provide null-safe access patterns

### ⚡ Example

```csharp
var name = element.GetStringValue("name");
var rating = element.GetDoubleValue("rating");
var reviews = element.GetArray("reviews");
var names = element.GetArrayStringValues("names");
```

### 🧠 Notes

- Designed for **internal SDK usage** (mappers/parsers)
- Prevents exceptions when working with external API responses
- Returns:
    - `null` → when value is missing or invalid
    - empty sequence → for arrays