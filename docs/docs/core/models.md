---
title: Models
---

# 📦 Core Models

This section contains the data models used across the SDK.

These models represent:

- API requests
- Shared value objects
- Low-level response handling

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

## 📁 Namespace: D3lg4doMaps.Core.Public.Models.Http

### 🌊 StreamResponse

Represents a **streamed HTTP response**, providing access to both the response stream and the underlying HTTP message.

```csharp
public sealed class StreamResponse : IDisposable, IAsyncDisposable {
    public Stream Stream { get; }
    public HttpResponseMessage ResponseMessage { get; }
}
```

#### ⚙️ Responsabilities

- Provide access to **streaming response data**
- Expose the underlying `HttpResponseMessage` for advanced scenarios
- Manage the lifecycle of both the stream and HTTP response

#### 🧠 Notes

- Designed for **large or streaming payloads**
- Enables **incremental processing** without loading the entire response into memory
- Implements both `IDisposable` and `IAsyncDisposable`

#### ⚠️ Important

- This type **owns its resources**
- You must dispose it after use:
```csharp
await using var response = await client.SendStreamAsync(request);
```
- If passed to a higher-level API (e.g., serializer), **do not dispose it manually**
- Accessing `Stream` or `ResponseMessage` after disposal is unsafe

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
- Streaming support is built-in for **performance and scalability**