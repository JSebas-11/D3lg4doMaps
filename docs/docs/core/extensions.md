---
title: Extensions
---

# 🧩 Core Extensions

The Core package provides a set of extension methods to integrate the SDK
into your application and simplify common tasks.

These extensions cover:

- Dependency injection setup
- JSON parsing helpers

They are designed to improve developer experience by reducing boilerplate
and providing safe, reusable patterns.

---

## 💉 Dependency Injection 

The SDK integrates with .NET dependency injection through extension methods.

The `AddD3lg4doMaps` method registers all required Core services, including:

- HTTP client
- Serialization
- Request builders and factories

Registers the core services required to use the SDK.

```csharp
public static IServiceCollection AddD3lg4doMaps(
    this IServiceCollection services, MapsConfiguration config
)
```

### ⚡ Basic Usage

```csharp
var services = new ServiceCollection();

services.AddD3lg4doMaps(new MapsConfiguration {
    ApiKey = "YOUR_API_KEY"
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
```

### 🧠 Notes

- Designed for **internal SDK usage** (mappers/parsers)
- Prevents exceptions when working with external API responses
- Returns:
    - `null` → when value is missing or invalid
    - empty sequence → for arrays