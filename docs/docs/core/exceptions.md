---
title: Exceptions
---

# 🚨 Core Exceptions

The SDK provides a consistent exception model for handling errors returned by the Google Maps API.

Instead of dealing with raw HTTP errors or JSON responses, errors are mapped into
strongly-typed .NET exceptions.

---

## 🧾 MapsApiException

Base exception for all errors produced by the SDK.

```csharp
public class MapsApiException : Exception {
    public string? Status { get; }
    public string? RawResponse { get; }
}
```

### ⚙️ Properties

- `Status` → API status code (if available)
- `RawResponse` → raw API response (useful for debugging)

### 📢 When it happens

- General SDK errors

### 🧠 Notes

- All SDK exceptions inherit from this class
- Contains additional API context beyond standard exceptions
- Useful for logging and debugging

---

## 🔐 MapsApiAuthException

Represents authentication errors.

```csharp
public class MapsApiAuthException : MapsApiException
```

### 📢 When it happens

- Invalid API key
- Missing API key
- Insufficient permissions

---

## 🧊 MapsCacheException

Represents an error during a caching operation.

```csharp
public class MapsCacheException : MapsApiException
```

### 📢 When it happens

- Cache key generation failures
- Invalid cache configuration
- Missing cache provider registration
- Duplicate cache layer registration
- Invalid caching options

---

## ⚠️ MapsInvalidRequestException

Represents invalid request errors.

```csharp
public class MapsInvalidRequestException : MapsApiException
```

### 📢 When it happens

- Missing required parameters
- Invalid parameter values

---

## 🔍 MapsNotFoundException

Represents resource-not-found errors.
```csharp
public class MapsNotFoundException : MapsApiException
```

### 📢 When it happens

- Requested resource does not exist
- Invalid place ID, route, etc.

---

## 🚦 MapsRateLimitException

Represents rate limit errors.

```csharp
public class MapsRateLimitException : MapsApiException
```

### 📢 When it happens

- Too many requests in a short period
- API quota exceeded

---

## ⚡ Example

```csharp
try {
    var result = await places.Search.SearchByTextAsync("coffee");
}
catch (MapsApiAuthException ex) {
    // Handle invalid API key
}
catch (MapsRateLimitException ex) {
    // Retry or backoff
}
catch (MapsApiException ex) {
    // Generic API error
}
```

---

## 🧠 Design Notes

- Exceptions are **specific and predictable**
- Encourages **granular error handling**
- Avoids exposing raw API responses directly
- Still allows access to raw data when needed