---
title: Models
---

# 📦 Core Models

This section contains the data models used across the SDK.

These models represent:

- API requests
- Shared value objects

---

## 📁 Namespace: D3lg4doMaps.Core.Public.Models

### 🧾 MapsApiRequest

Represents a request definition used to call a Maps API endpoint.

```csharp
public sealed class MapsApiRequest {
    public HttpMethod Method { get; init; } = HttpMethod.Get;
    public ApiKeyLocation ApiKeyLocation { get; init; } = ApiKeyLocation.Header;
    public string BaseUrl { get; init; } = null!;
    public string Endpoint { get; init; } = null!;
    public IDictionary<string, string>? Headers { get; init; }
    public IDictionary<string, string>? Query { get; init; }
    public object? Payload { get; init; }
}
```

#### ⚙️ Properties

| Property         | Description                                    |
|:---------------- |:---------------------------------------------- |
| `Method`         | HTTP method (default: GET)                     |
| `ApiKeyLocation` | Where the API key is sent -> see [LocationEnum](enums#-apikeylocation) |
| `BaseUrl`        | Base API URL                                   |
| `Endpoint`       | Relative endpoint path                         |
| `Headers`        | Optional request headers                       |
| `Query`          | Optional query parameters                      |
| `Payload`        | Optional request body                          |

---

## 📁 Namespace: D3lg4doMaps.Core.Public.Models.Geometry

### 📍 LatLng

Represents a geographic coordinate defined by latitude and longitude.

```csharp
public sealed class LatLng {
    public double Latitude { get; }
    public double Longitude { get; }
}
```

#### ⚙️ Properties

| Property    | Description                  |
|:----------- |:---------------------------- |
| `Latitude`  | Latitude in decimal degrees  |
| `Longitude` | Longitude in decimal degrees |

---

## 🧠 Notes

- Models are **immutable where possible**
- Instances are typically **created internally by the SDK**
- Designed for **consistency across modules**