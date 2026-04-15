---
title: Enums
---

# 📦 Routes Enums

This section contains the enum types used across the Routes module.

These enums define routing behavior, preferences, formatting, and response/request metadata.

---

## 🚗 Routing

### RouteDetailLevel

Controls the level of detail returned in route responses.

```csharp
public enum RouteDetailLevel {
    Summary,
    Standard,
    Full
}
```

### TravelMode

Specifies the mode of transportation used for a route.

```csharp
public enum TravelMode {
    Drive,
    Bicycle,
    Walk,
    TwoWheeler,
    Transit,
    Unknown
}
```

### RoutingPreference

Defines how routes are optimized.

```csharp
public enum RoutingPreference {
    TrafficUnaware,
    TrafficAware,
    TrafficAwareOptimal,
    Unknown
}
```

---

## 📏 Output & Formatting

### Units

Specifies the unit system used for distances.

```csharp
public enum Units {
    Metric,
    Imperial,
    Unknown
}
```

### PolylineQuality

Controls the level of detail for route geometry.

```csharp
public enum PolylineQuality {
    HighQuality,
    Overview,
    Unknown
}
```

### PolylineEncoding

Specifies the encoding format used for polylines.

```csharp
public enum PolylineEncoding {
    EncodedPolyline,
    Unknown
}
```

---

## 🚦 Traffic & Conditions

### TrafficModel

Defines how traffic conditions are considered.

```csharp
public enum TrafficModel {
    BestGuess,
    Pessimistic,
    Optimistic,
    Unknown
}
```

### RouteElementCondition

Represents whether a valid route exists.

```csharp
public enum RouteElementCondition {
    RouteExists,
    RouteNotFound,
    Unknown
}
```

### Speed

Represents traffic conditions for a route segment.

```csharp
public enum Speed {
    Normal,
    Slow,
    TrafficJam,
    Unknown
}
```

---

## 🚌 Transit

### TransitTravelMode

Specifies allowed transit types.

```csharp
public enum TransitTravelMode {
    Bus,
    Subway,
    Train,
    LightRail,
    Rail,
    Unknown
}
```

### TransitRoutingPreference

Defines preferences for transit routes.

```csharp
public enum TransitRoutingPreference {
    LessWalking,
    FewerTransfers,
    Unknown
}
```

---

## 🚨 Fallback

### RoutingMode

Represents the routing mode used when fallback occurs.

```csharp
public enum RoutingMode {
    FallbackTrafficUnaware,
    FallbackTrafficAware,
    Unknown
}
```

### Reason

Specifies why fallback routing was used.

```csharp
public enum Reason {
    ServerError,
    LatencyExceeded,
    Unknown
}
```

---

## 🚗 Vehicle

### VehicleEmissionType

Specifies the emission type of a vehicle.

```csharp
public enum VehicleEmissionType {
    Gasoline,
    Electric,
    Hybrid,
    Diesel,
    Unknown
}
```

---

## 🧠 Notes

- Enums marked as `Unknown` are **internal values** used by the SDK
- They should **not be manually** set in requests
- Validation is enforced in builders to prevent invalid usage