---
title: Configuration
---

# ⚙️ Core Configuration

The SDK is configured through a central `MapsConfiguration` and `MapsCachingOptions` objects.

This configuration controls:

- Authentication (API key)
- Localization (language and region)
- Request behavior (timeouts)
- HTTP-layer caching (in-memory and distributed)
- Logging *(planned)*

---

## 🧾 MapsConfiguration

Represents the configuration settings used by the Maps SDK.

```csharp
public sealed class MapsConfiguration {
    public string ApiKey { get; set; } = default!;
    public string Language { get; set; } = "en";
    public string Region { get; set; } = "US";
    public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromSeconds(30);
    public MapsLoggingOptions Logging { get; set; } = new();
}
```

### ⚙️ Properties

| Property         | Description                                         |
|:---------------- |:--------------------------------------------------- |
| `ApiKey`         | API key utilized to authenticate requests in Google |
| `Language`       | Preferred response language (default: en)           |
| `Region`         | Region bias for results (default: US)               |
| `RequestTimeout` | Request timeout for API calls (default: 30 seconds) |
| `Logging`        | Logging configuration (planned)                     |

---

## 🧾 MapsCachingOptions

Defines HTTP caching configuration for the SDK.

```csharp
public sealed class MapsCachingOptions {
    public string Prefix { get; set; } = "d3lg4doMaps";
    public TimeSpan AbsoluteExpiration { get; set; } = TimeSpan.FromMinutes(30);
    public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromMinutes(30);
}
```

### ⚙️ Properties

| Property            | Description                     |
|:--------------------|:--------------------------------|
| `Prefix`            | Prefix used for cache keys      |
| `AbsoluteExpiration`| Absolute cache expiration time  |
| `SlidingExpiration` | Sliding cache expiration time   |

---

## 🧾 MapsLoggingOptions

Defines logging options for the SDK.

```csharp
public sealed class MapsLoggingOptions {
    public bool Enabled { get; set; } = true;
    public LogLevel Level { get; set; } = LogLevel.Information;
    public string? Prefix { get; set; } = "[D3lg4doMaps]";
}
```

### ⚙️ Properties

| Property  | Description                       |
|:--------- |:--------------------------------- |
| `Enabled` | Enables or disables logging       |
| `Level`   | Minimum log level                 |
| `Prefix`  | Optional prefix for log messages  |

---

## ⚡ Basic Setup

👉 [Dependency Injection](extensions#-dependency-injection)  
👉 [Caching Injection](/docs/core/extensions.md#-http-caching-injection)

```csharp
var services = new ServiceCollection();

services.AddDelgadoMaps(opts => {
    opts.ApiKey = "YOUR_API_KEY";
    opts.Language = "en";
    opts.Region = "US";
    opts.RequestTimeout = TimeSpan.FromSeconds(30);
});

services.AddDelgadoMapsMemoryCache(opts => {
    opts.AbsoluteExpiration = TimeSpan.FromMinutes(30);
});
```

---

## 🧠 Notes

- Configuration is **centralized in Core and shared across all modules**
- **ApiKey is required** for all API calls
- **Default values** are provided for most settings
- Logging configuration is defined but **not yet implemented**
- HTTP **caching** is **optional and configurable**
- Stream endpoints services such as [IDistanceMatrixService](/docs/routes/abstractions.md#-idistancematrixservice) are not cached