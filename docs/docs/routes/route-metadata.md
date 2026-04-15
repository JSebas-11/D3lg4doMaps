---
title: Route Metadata
---

# 📦 Route Metadata

These models represent the **result of routing computations** returned by services such as:

- 🧭 Directions  
- 📏 Distance Matrix  

They contain **distance, duration, geometry, navigation structure, and additional metadata** describing computed routes.

---

## 🧠 Overview

- These models are **output-only** and returned by routing services
- They are structured hierarchically:  
    RouteResult  
    └── ComputeRoute  
            └── RouteLeg  
                └── RouteStep  
- Designed to provide both:
    - **Machine-friendly data** (meters, durations, indices)
    - **User-friendly data** (localized values, descriptions)

---

## 🚗 ComputeRoute

Represents a fully computed route between an origin and a destination.

```csharp
public sealed class ComputeRoute {
    public int DistanceMeters { get; internal set; }
    public string Duration { get; internal set; } = null!;
    public string? StaticDuration { get; internal set; }

    public string? Description { get; internal set; }
    public IReadOnlyList<string> RouteLabels { get; internal set; } = [];

    public Viewport? Viewport { get; internal set; }
    public Polyline? Polyline { get; internal set; }

    public IReadOnlyList<RouteLeg> Legs { get; internal set; } = [];

    public string? RouteToken { get; internal set; }
    public IReadOnlyList<string> Warnings { get; internal set; } = [];

    public RouteTravelAdvisory? TravelAdvisory { get; internal set; }
    public RouteLocalizedValues? LocalizedValues { get; internal set; }

    public IReadOnlyList<int> OptimizedWaypointOrder { get; internal set; } = [];
}
```

### 🧠 Includes

- **Metrics** → distance and duration
- **Description** → summary and route labels
- **Geometry** → [viewport](#viewport) and [polyline](#polyline)
- **Navigation** → route legs → see [RouteLeg](#-routeleg)
- **Extras**
    - Travel advisory → see [RouteTravelAdvisory](#routetraveladvisory)
    - Localized values → see [RouteLocalizedValues](#routelocalizedvalues)
    - Warnings and route token
    - Waypoint optimization results

---

## 🚧 RouteLeg

Represents a segment of a route between two waypoints.

```csharp
public sealed class RouteLeg {
    public int DistanceMeters { get; internal set; }
    public string Duration { get; internal set; } = null!;

    public Location? StartLocation { get; internal set; }
    public Location? EndLocation { get; internal set; }

    public Polyline? Polyline { get; internal set; }

    public IReadOnlyList<RouteStep> Steps { get; internal set; } = [];

    public RouteLegTravelAdvisory? TravelAdvisory { get; internal set; }
    public RouteLegLocalizedValues? LocalizedValues { get; internal set; }
    public StepsOverview? Overview { get; internal set; }
}
```

### 🧠 Includes

- **Metrics** → distance and duration
- **Location** → start and end coordinates → see [Location](#location)
- **Geometry** → [polyline](#polyline)
- **Steps** → detailed navigation instructions → see [RouteStep](#-routestep) 
- **Extras**
    - Travel advisory (traffic per segment) → see [RouteLegTravelAdvisory](#routelegtraveladvisory)
    - Localized values → see [RouteLegLocalizedValues](#routelegsteplocalizedvalues)
    - Step overview for compact display → see [StepsOverview](#stepsoverview)

---

## 👣 RouteStep

Represents a single navigation step within a route leg.

```csharp
public sealed class RouteStep {
    public int DistanceMeters { get; internal set; }
    public string? StaticDuration { get; internal set; }

    public Location? StartLocation { get; internal set; }
    public Location? EndLocation { get; internal set; }

    public Polyline? Polyline { get; internal set; }

    public NavigationInstruction? NavigationInstruction { get; internal set; }

    public RouteLegTravelAdvisory? TravelAdvisory { get; internal set; }
    public RouteLegStepLocalizedValues? LocalizedValues { get; internal set; }

    public TravelMode? TravelMode { get; internal set; }
}
```

### 🧠 Includes

- **Metrics** → step distance and duration
- **Location** → start and end points → see [Location](#location)
- **Geometry** → polyline segment → see [polyline](#polyline)
- **Navigation** → maneuver + instruction → see [NavigationInstruction](#navigationinstruction)
- **Extras**
    - Traffic info → see [RouteLegTravelAdvisory](#routelegtraveladvisory)
    - Localized values → see [RouteLegStepLocalizedValues](#routeleglocalizedvalues)
    - Travel mode per step → see [TravelMode](/docs/routes/enums.md#travelmode)

---

## 🗺️ Geometry

### Polyline

Represents an encoded polyline describing the route geometry.

```csharp
public sealed class Polyline {
    public string EncodedPolyline { get; internal set; } = null!;
}
```

#### 🧠 Notes

- Encoded using Google's polyline algorithm
- Can be decoded into coordinates for rendering on maps

### Location

Represents a geographic location.

```csharp
public sealed class Location {
    public LatLng LatLng { get; internal set; } = null!;
    public int? Heading { get; internal set; }
}
```

#### 🧠 Notes

- `LatLng` → coordinates → see [LatLng](/docs/core/models.md#-latlng)
- `Heading` → direction (0–360°), where 0 = north

### Viewport

Represents a rectangular geographic bounding box.

```csharp
public sealed class Viewport {
    public LatLng Low { get; internal set; } = null!;
    public LatLng High { get; internal set; } = null!;
}
```

#### 🧠 Notes

- Defines the visible region that contains the entire route
- Useful for map camera positioning
- [LatLng](/docs/core/models.md#-latlng) object

---

## 🚦 Travel Advisory

### RouteTravelAdvisory

Represents traffic conditions and routing restrictions.

```csharp
public sealed class RouteTravelAdvisory {
    public IReadOnlyList<SpeedInterval> SpeedReadingIntervals { get; internal set; } = [];
    public bool? RouteRestrictionsPartiallyIgnored { get; internal set; }
    public Money? TransitFare { get; internal set; }
}
```

#### ⚙️ Properties

| Property                            |	Description                             |
|:------------------------------------|:----------------------------------------|
| `SpeedReadingIntervals`             |	Traffic speed segments along the route  |
| `RouteRestrictionsPartiallyIgnored` |	Indicates if restrictions were bypassed |
| `TransitFare`                       |	Fare information for transit routes     |

### RouteLegTravelAdvisory

```csharp
public sealed class RouteLegTravelAdvisory {
    public IReadOnlyList<SpeedInterval> SpeedReadingIntervals { get; internal set; } = [];
}
```

### SpeedInterval

Represents a segment of the route with a specific traffic condition.

```csharp
public sealed class SpeedInterval {
    public int StartPolylinePointIndex { get; internal set; }
    public int EndPolylinePointIndex { get; internal set; }
    public Speed Speed { get; internal set; }
}
```

#### 🧠 Notes

- Maps directly to segments of the route polyline
- Used to visualize traffic conditions
- [Speed](/docs/routes/enums.md#speed) enum

### Money

Represents a monetary value.

```csharp
public sealed class Money {
    public string CurrencyCode { get; internal set; } = null!;
    public string Units { get; internal set; } = null!;
    public long Nanos { get; internal set; }
}
```

#### 🧠 Notes

- `Units` = whole currency units
- `Nanos` = fractional part (1e-9 precision)

---

## 🌍 Localization

### RouteLocalizedValues

Represents human-readable, formatted route values.

```csharp
public sealed class RouteLocalizedValues {
    public string Distance { get; internal set; } = null!;
    public string Duration { get; internal set; } = null!;
    public string? StaticDuration { get; internal set; }
    public string? TransitFare { get; internal set; }
}
```

### RouteLegLocalizedValues

```csharp
public sealed class RouteLegLocalizedValues {
    public string? Distance { get; internal set; }
    public string? Duration { get; internal set; }
}
```

### RouteLegStepLocalizedValues

```csharp
public sealed class RouteLegStepLocalizedValues {
    public string? Distance { get; internal set; }
    public string? StaticDuration { get; internal set; }
}
```

#### 🧠 Notes

- Values are formatted for display (e.g., `"5.2 km"`, `"12 min"`)
- Should be used for UI, not calculations

---

## 🧩 Supporting Models

### NavigationInstruction

Represents a navigation instruction.

```csharp
public sealed class NavigationInstruction {
    public string? Maneuver { get; internal set; }
    public string? Instruction { get; internal set; }
}
```

#### 🧠 Notes

- `Maneuver` → machine-readable (e.g., `"turn-left"`)
- `Instruction` → human-readable text

### StepsOverview

Provides a summarized representation of navigation steps.

```csharp
public sealed class StepsOverview {
    public IReadOnlyList<Segment> Segments { get; internal set; } = [];
}
```

#### 🧠 Notes

- `Segments` → set of navegation segments → see [Segment](#segment)

### Segment

Represents a grouped navigation segment.

```csharp
public sealed class Segment {
    public NavigationInstruction? NavigationInstruction { get; internal set; }
    public TravelMode? TravelMode { get; internal set; }

    public int StartIndex { get; internal set; }
    public int EndIndex { get; internal set; }
}
```

#### 🧠 Notes

- Groups multiple steps into higher-level instructions
- Useful for simplified navigation UIs
- [TravelMode](/docs/routes/enums.md#travelmode) enum
- [NavigationInstruction](#navigationinstruction) object