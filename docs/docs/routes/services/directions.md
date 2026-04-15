---
title: Directions
---

# 🧭 Directions

The **Directions feature** calculates routes between locations with flexible routing options and detailed metadata.

It is commonly used for:

- 🚗 Turn-by-turn navigation  
- 📍 Route planning with intermediate stops  
- 🔄 Alternative route comparison  
- 📊 Travel optimization  

---

## 🔗 Related Abstractions

This feature is exposed through:

👉 See: [IDirectionsService](/docs/routes/abstractions.md#-idirectionsservice)

---

## 🧩 Service

```csharp
public interface IDirectionsService {
    Task<RouteResult> GetRoutesAsync(
        RouteRequest routeRequest, 
        RouteDetailLevel detailLevel = RouteDetailLevel.Standard
    );

    Task<JsonDocument> GetRoutesRawAsync(
        RouteRequest routeRequest, 
        params string[] fields
    );
}
```

### ⚙️ Methods

- GetRoutesAsync → returns **structured route data**
- GetRoutesRawAsync → returns **raw JSON response**

### 🧠 Notes

- `GetRoutesAsync` → preferred for most scenarios
- `GetRoutesRawAsync` → useful for (remember to dispose):
    - Unmapped fields
    - Debugging
    - Advanced integrations
- Supports configurable detail levels:
    - Summary → lightweight
    - Standard → balanced (default)
    - Full → full metadata

---

## ⚡ Examples

### 🧭 Basic Route

```csharp
var origin = new WaypointBuilder()
    .FromPlaceId("place_id_1")
    .Build();

var destination = new WaypointBuilder()
    .FromPlaceId("place_id_2")
    .Build();

var request = new RouteRequestBuilder()
    .From(origin)
    .To(destination)
    .WithTravelMode(TravelMode.Drive)
    .Build();

var result = await directionsService.GetRoutesAsync(request);

foreach (var route in result.Routes) {
    Console.WriteLine(route.DistanceMeters);
    Console.WriteLine(route.Duration);
}
```

### 🔄 With Intermediates & Optimization

```csharp
var request = new RouteRequestBuilder()
    .From(origin)
    .To(destination)
    .AddIntermediate(stop1)
    .AddIntermediate(stop2)
    .OptimizeWaypointOrder()
    .Build();
```

### 🧾 Raw JSON (Advanced)

```csharp
using var json = await directionsService.GetRoutesRawAsync(
    request,
    "routes.distanceMeters",
    "routes.duration"
);

var root = json.RootElement;
```

---

## 📦 Models

### RouteResult

Represents the result of a directions request.

```csharp
public sealed class RouteResult {
    public IReadOnlyList<ComputeRoute> Routes { get; internal set; } = [];
    public ComputeRoute? BestRoute { get; internal set; }
}
```

#### ⚙️ Properties

| Property	  | Description                                            |
|:------------|:-------------------------------------------------------|
| `Routes`	  | List of computed routes between origin and destination |
| `BestRoute` | The optimal route suggested by the API (if available)  |

#### 🧠 Includes

- **Routes**
    - Collection of all computed routes → see [Route](/docs/routes/route-metadata.md#-computeroute)
    - Each route contains:
        - Legs
        - Steps
        - Metadata
- **Best Route**
- A convenience property pointing to the **most optimal route**
- Based on:
    - Duration
    - Distance
    - Traffic conditions
- May be `null` depending on API response

#### 🧠 Notes

- `Routes` may contain:
    - A single route (default)
    - Multiple routes when:
        - `WithAlternativeRoutes()` is enabled
- `BestRoute` is typically:
    - The **first** or **recommended** route defined by Google Routes API
    - Provided directly for convenience (no need to manually evaluate)
- Prefer `BestRoute` when:
    - You only need the optimal route
- Use `Routes` when:
    - Comparing multiple route options

#### 🔗 Related

👉 Full route structure → [click](/docs/routes/route-metadata.md)  
👉 Request model → [click](#routerequest)

--- 

## 🧾 Request

### RouteRequest

Represents a request for calculating routes.

```csharp
public sealed class RouteRequest {
    public Waypoint Origin { get; internal set; } = null!;
    public Waypoint Destination { get; internal set; } = null!;
    public IReadOnlyList<Waypoint> Intermediates { get; internal set; } = [];

    public TravelMode? TravelMode { get; internal set; }
    public RoutingPreference? RoutingPreference { get; internal set; }

    public PolylineQuality? PolylineQuality { get; internal set; }
    public PolylineEncoding? PolylineEncoding { get; internal set; }

    public DateTimeOffset? DepartureTime { get; internal set; }
    public DateTimeOffset? ArrivalTime { get; internal set; }

    public bool? ComputeAlternativeRoutes { get; internal set; }
    public bool? OptimizeWaypointOrder { get; internal set; }

    public RouteModifiers? RouteModifiers { get; internal set; }
    public Units? Units { get; internal set; }
}
```

#### ⚙️ Properties

| Property                   | Description                                          |
|:---------------------------|:-----------------------------------------------------|
| `Origin`                   | Origin of the route                                  |
| `Destination`              | Destination of the route                             |
| `Intermediate`             | Stops between origin and destination                 |
| `TravelMode`               | Travel mode used for routing                         |
| `RoutingPreference`        | Preferences to compute route                         |
| `PolylineQuality`          | Quality preference for polyline                      |
| `PolylineEncoding`         | Preferred encoding for polyline                      |
| `DepartureTime`            | Departure time for route calculation                 |
| `ArrivalTime`              | Desired arrival time                                 |
| `ComputeAlternativeRoutes` | Calculate alternate routes in addition to the route  |
| `OptimizeWaypointOrder`    | Minimize the overall cost of the route by re-ordering the specified intermediate waypoints|
| `RouteModifiers`           | Conditions that affect they way routes are calculate |
| `Units`                    | Measurement system (metric/imperial)                 |

#### 🧠 Notes

- Must be constructed using `RouteRequestBuilder`


### RouteRequestBuilder

Provides a fluent API for constructing [RouteRequest](#routerequest).

```csharp
public sealed class RouteRequestBuilder {
    public RouteRequestBuilder From(Waypoint origin);
    public RouteRequestBuilder To(Waypoint destination);

    public RouteRequestBuilder WithIntermediates(IEnumerable<Waypoint> intermediates);
    public RouteRequestBuilder AddIntermediate(Waypoint intermediate);

    public RouteRequestBuilder WithTravelMode(TravelMode travelMode);
    public RouteRequestBuilder WithRoutingPreference(RoutingPreference routingPreference);

    public RouteRequestBuilder WithPolyline(PolylineQuality quality, PolylineEncoding encoding);

    public RouteRequestBuilder WithDepartureTime(DateTimeOffset departureTime);
    public RouteRequestBuilder WithArrivalTime(DateTimeOffset arrivalTime);

    public RouteRequestBuilder WithRouteModifiers(RouteModifiers routeModifiers);
    public RouteRequestBuilder WithUnits(Units units);

    public RouteRequestBuilder WithAlternativeRoutes();
    public RouteRequestBuilder OptimizeWaypointOrder();

    public RouteRequest Build();
}
```

#### ⚙️ Configuration

- **Waypoints**
    - From([waypoint](/docs/routes/routing-components.md#waypoint)) → sets origin
    - To([waypoint](/docs/routes/routing-components.md#waypoint)) → sets destination
    - AddIntermediate([waypoint](/docs/routes/routing-components.md#waypoint)) → adds intermediate stops
    - Max **25 intermediates**
- **Routing** (Optional)
    - WithTravelMode([travelMode](/docs/routes/enums.md#travelmode))
    - WithRoutingPreference([routingPreference](/docs/routes/enums.md#routingpreference))
    - WithRouteModifiers([routeModifiers](/docs/routes/routing-components.md#route-modifiers))
- **Geometry** (Optional)
    - WithPolyline([quality](/docs/routes/enums.md#polylinequality), [encoding](/docs/routes/enums.md#polylineencoding))
- **Time** (Optional)
    > ❗ Only one can be set
    - WithDepartureTime(...)
    - WithArrivalTime(...)
- **Options** (Optional)
    - WithAlternativeRoutes() → returns multiple route options
    - OptimizeWaypointOrder() → reorders intermediates for efficiency
- **Measures** (Optional)
    WithUnits([units](/docs/routes/enums.md#units))

#### ⚠️ Validation

Throws [MapsInvalidRequestException](/docs/core/exceptions.md#️-mapsinvalidrequestexception) if:

- Origin or destination is missing
- Both arrival and departure times are set
- Optimization is enabled without intermediates
- More than 25 intermediates are provided
- Waypoint heading is used with unsupported travel modes
- Any enum is set to `Unknown`

#### 🧠 Notes

- Uses a **builder pattern** to prevent invalid states
- Ensures API constraints before execution
- Automatically validates nested objects
- Heading is only supported for:
    - `Drive`
    - `TwoWheeler`