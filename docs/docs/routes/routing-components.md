---
title: Routing Components
---

# 🧩 Routing Components

These components are **shared building blocks** used across routing features such as:

- 📏 Distance Matrix  
- 🧭 Directions  

They represent **locations, constraints, and routing preferences** used when constructing requests.

---

## 📍 Shared Components

### Waypoint

Waypoints represent **locations** used in routing operations.

They can be defined using:

- A **Place ID**
- Geographic **coordinates**

```csharp
public sealed class Waypoint {
    public bool Via { get; internal set; }
    public bool VehicleStopover { get; internal set; }
    public bool SideOfRoad { get; internal set; }

    public string? PlaceId { get; }
    public Location? Location { get; }
}
```

#### 🧠 Notes

- Must define **either** `PlaceId` **or** `Location`
- Supports additional routing hints:
- Via → influences route without stopping
- VehicleStopover → creates a stop
- SideOfRoad → aligns to nearest road segment

### WaypointBuilder

Provides a fluent API for constructing [Waypoint](#waypoint).

```csharp
public sealed class WaypointBuilder {
    public WaypointBuilder FromPlaceId(string placeId);
    public WaypointBuilder FromLocation(double latitude, double longitude, int? heading = null);

    public WaypointBuilder WithVia();
    public WaypointBuilder WithSideOfRoad();
    public WaypointBuilder WithStopover();

    public Waypoint Build();
}
```

##### ⚙️ Configuration

- FromPlaceId(...) → define using Place ID
- FromLocation(...) → define using coordinates (+ optional heading)
- WithVia() → marks as pass-through point
- WithStopover() → marks as stop
- WithSideOfRoad() → aligns waypoint to road

##### ⚠️ Validation

Throws [MapsInvalidRequestException](/docs/core/exceptions.md#️-mapsinvalidrequestexception) if:

- Both or neither `PlaceId` and `Location` are set
- Coordinates or heading are out of range

### Route Modifiers

Defines constraints that influence route calculation.

```csharp
public sealed class RouteModifiers {
    public bool AvoidTolls { get; }
    public bool AvoidHighways { get; }
    public bool AvoidFerries { get; }
    public bool AvoidIndoor { get; }

    public VehicleInfo? VehicleInfo { get; }

    public static RouteModifiers Create();

    public RouteModifiers WithVehicleInfo(VehicleInfo vehicleInfo);
    public RouteModifiers NoTolls();
    public RouteModifiers NoHighways();
    public RouteModifiers NoFerries();
    public RouteModifiers NoIndoor();
}
```

> Built using Create() + fluent methods

### VehicleInfo

Represents vehicle-specific routing data.

```csharp
public sealed class VehicleInfo {
    public VehicleEmissionType EmissionType { get; }
}
```

#### 🧠 Notes

- Used to influence:
    - Emission-based restrictions
    - Environmental routing rules
- See [VehicleEmissionType](/docs/routes/enums.md#vehicleemissiontype)

### TransitPreferences

Defines preferences for transit routing.

```csharp
public sealed class TransitPreferences {
    public IReadOnlyList<TransitTravelMode> AllowedTravelModes { get; }
    public TransitRoutingPreference? RoutingPreference { get; }
}
```

#### ⚙️ Properties

| Property             | Description                         |
|:---------------------|:------------------------------------|
| `AllowedTravelModes` | Allowed transit types (bus, train…) |
| `RoutingPreference`  | Optimization strategy               |

#### 🧠 Notes

- Built via constructor
- Requires at least **one travel mode**
- Only valid when:
    - TravelMode = Transit
- See:
    - [TransitTravelMode](/docs/routes/enums.md#transittravelmode)
    - [TransitRoutingPreference](/docs/routes/enums.md#transitroutingpreference)

#### ⚠️ Validation

Throws [MapsInvalidRequestException](/docs/core/exceptions.md#️-mapsinvalidrequestexception) if:

- No travel modes are provided
- Any enum is `Unknown`

---

## 📍 Matrix-Specific Components

These are used in Distance Matrix requests.

### RouteMatrixOrigin

Represents an origin with optional routing modifiers.

```csharp
public sealed class RouteMatrixOrigin {
    public Waypoint Waypoint { get; } = null!;
    public RouteModifiers? RouteModifiers { get; }
}
```

#### 🧠 Notes

- Wraps a [Waypoint](#waypoint)
- Can include per-origin routing constraints -> see [RouteModifiers](#route-modifiers)
- Used in:
    - [DistanceRequest](/docs/routes/services/distanceMatrix.md#distancerequest)

### RouteMatrixDestination

Represents a destination in matrix calculations.

```csharp
public sealed class RouteMatrixDestination {
    public Waypoint Waypoint { get; } = null!;
}
```

#### 🧠 Notes

- Simpler than origin (no modifiers)
- Wraps a [Waypoint](#waypoint)
- Used in:
    - [DistanceRequest](/docs/routes/services/distanceMatrix.md#distancerequest)

---

## 🧠 Design Notes

- These components are **shared across multiple services**
- Designed to:
    - Promote **reuse**
    - Keep **request** models **clean and focused**
- All complex objects are:
    - **Validated internally**
    - Safe to reuse across requests