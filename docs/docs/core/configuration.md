---
title: Configuration
---

# ⚙️ Core Configuration

The SDK is configured through a central `MapsConfiguration` object.

This configuration controls:

- Authentication (API key)
- Localization (language and region)
- Request behavior (timeouts)
- Logging *(planned)*
- MemoryCache *(planned)*

---

## 🧾 MapsConfiguration

Represents the configuration settings used by the Maps SDK.

```csharp
public sealed class MapsConfiguration {
    public string ApiKey { get; set; } = default!;
    public string Language { get; set; } = "en";
    public string Region { get; set; } = "US";
    public int TimeOutSeconds { get; set; } = 30;
    public MapsLoggingOptions Logging { get; set; } = new();
}
```

### ⚙️ Properties

| Property         | Description                                         |
|:---------------- |:--------------------------------------------------- |
| `ApiKey`         | API key utilized to authenticate requests in Google |
| `Language`       | Preferred response language (default: en)           |
| `Region`         | Region bias for results (default: US)               |
| `TimeOutSeconds` | Request timeout in seconds (default: 30)            |
| `Logging`        | Logging configuration (planned)                     |

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

```csharp
var services = new ServiceCollection();

services.AddD3lg4doMaps(new MapsConfiguration() {
    ApiKey = "YOUR_API_KEY",
    Language = "en",
    Region = "US",
    TimeOutSeconds = 30
});
```

---

## 🧠 Notes

- Configuration is **centralized in Core and shared across all modules**
- **ApiKey is required** for all API calls
- **Default values** are provided for most settings
- Logging configuration is defined but **not yet implemented**
- Memory caching support is **planned but not yet available**