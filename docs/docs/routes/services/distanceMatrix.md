---
title: Distance Matrix
---

# 📏 Distance Matrix

The **Distance Matrix feature** computes travel distance, duration and more metadata between multiple origins and destinations.

It is commonly used for:

- 🚚 Logistics and delivery systems  
- 📊 Travel time estimation  
- 🔄 Route comparison and optimization  

## 🔗 Related Abstractions

This feature is exposed through:

👉 See: [IDistanceMatrixService](/docs/routes/abstractions.md#-idistancematrixservice)

---

## 🧩 Service

```csharp
public interface IDistanceMatrixService {
    Task<IReadOnlyList<RouteMatrixElement>> GetDistancesAsync(
        DistanceRequest distanceRequest, 
        RouteDetailLevel detailLevel = RouteDetailLevel.Standard
    );
}
```

### ⚙️ Methods

- GetDistancesAsync → computes **distance, duration, etc** between **origin-destination** pairs

### 🧠 Notes

- Returns a **flattened list** of results
- Each element represents a **single origin → destination pair**
- Supports configurable detail levels:
    - Summary → lightweight
    - Standard → balanced (default)
    - Full → includes extended metadata

---

## ⚡ Example

```csharp
var origin = new WaypointBuilder()
    .FromPlaceId("place_id_1")
    .Build();

var destination = new WaypointBuilder()
    .FromPlaceId("place_id_2")
    .Build();

var request = new DistanceRequestBuilder()
    .AddOrigin(origin)
    .AddDestination(destination)
    .WithTravelMode(TravelMode.Drive)
    .Build();

var results = await distanceMatrixService.GetDistancesAsync(request);

foreach (var item in results) {
    Console.WriteLine(item.DistanceMeters);
    Console.WriteLine(item.Duration);
}
```

---

## 📦 Models

### RouteMatrixElement

Represents a single origin → destination result.

```csharp
public sealed class RouteMatrixElement {
    public Status? Status { get; internal set; }
    public RouteElementCondition Condition { get; internal set; }

    public int? DistanceMeters { get; internal set; }
    public string? Duration { get; internal set; }
    public string? StaticDuration { get; internal set; }

    public int? OriginIndex { get; internal set; }
    public int? DestinationIndex { get; internal set; }

    public RouteTravelAdvisory? TravelAdvisory { get; internal set; }
    public FallbackInfo? FallbackInfo { get; internal set; }
    public RouteLocalizedValues? LocalizedValues { get; internal set; }
}
```

##### 🧠 Includes

- **Metrics** → distance, duration
- **Indexing** → origin/destination mapping
- **Condition** → route availability [Status](#status) / [Condition](/docs/routes/enums.md#routeelementcondition)
- **Extras**
    - Travel Advisory → see [TravelAdvisory](/docs/routes/route-metadata.md#-travel-advisory)
    - Fallback behavior → see [FallbackInfo](#fallbackinfo)
    - Localized values → see [RouteLocalizedValues](/docs/routes/route-metadata.md#routelocalizedvalues)

---

## 🧰 Components

These models provide additional structured information used within `RouteMatrixElement`.

- These models are **optional and context-dependent**
- Values **may be null** depending on API response
- Designed to keep both `RouteMatrixElement` clean and modular

---

### Status

Represents the execution status of a matrix element. Used in [RouteMatrixElement](#routematrixelement)

#### ⚙️ Properties

| Property  | Description    |
|:--------- |:---------------|
| `Code`    | Status code    |
| `Message` | Status message |

### FallbackInfo

Represents fallback routing information for a matrix element. Used in [RouteMatrixElement](#routematrixelement)

#### ⚙️ Properties

| Property      | Description                        |
|:------------- |:-----------------------------------|
| `RoutingMode` | Mode used for fallback computation -> [mode](/docs/routes/enums.md#routingmode)  |
| `Reason`      | Reason why fallback was applied -> [reason](/docs/routes/enums.md#reason)   |

---

## 🧾 Request

### DistanceRequest

Represents a request for computing distances between multiple locations.

```csharp
public sealed class DistanceRequest {
    public IReadOnlyList<RouteMatrixOrigin> Origins { get; internal set; } = [];
    public IReadOnlyList<RouteMatrixDestination> Destinations { get; internal set; } = [];
    public TravelMode? TravelMode { get; internal set; } 
    public RoutingPreference? RoutingPreference { get; internal set; } 
    public DateTimeOffset? DepartureTime { get; internal set; }
    public DateTimeOffset? ArrivalTime { get; internal set; }
    public Units? Units { get; internal set; }
    public TrafficModel? TrafficModel { get; internal set; }
    public TransitPreferences? TransitPreferences { get; internal set; }
}
```

#### ⚙️ Properties

| Property             | Description                                       |
|:---------------------|:--------------------------------------------------|
| `Origins`            | List of origin waypoints                          |
| `Destinations`       | List of destination waypoints                     |
| `TravelMode`         | Travel mode (drive, walk, transit, etc.)          |
| `RoutingPreference`  | Traffic and routing strategy                      |
| `DepartureTime`      | Departure time for route calculation              |
| `ArrivalTime`        | Desired arrival time                              |
| `Units`              | Measurement system (metric/imperial)              |
| `TrafficModel`       | Traffic prediction model                          |
| `TransitPreferences` | Transit-specific configuration                    |

#### 🧠 Notes

- Must be constructed using `DistanceRequestBuilder`

### DistanceRequestBuilder

Provides a fluent API to construct `DistanceRequest`.

```csharp
public sealed class DistanceRequestBuilder {
    public DistanceRequestBuilder WithOrigins(IEnumerable<RouteMatrixOrigin> origins);
    public DistanceRequestBuilder AddOrigin(RouteMatrixOrigin origin);
    public DistanceRequestBuilder AddOrigin(Waypoint originWaypoint, RouteModifiers? modifiers = null);

    public DistanceRequestBuilder WithDestinations(IEnumerable<RouteMatrixDestination> destinations);
    public DistanceRequestBuilder AddDestination(RouteMatrixDestination destination);
    public DistanceRequestBuilder AddDestination(Waypoint destinationWaypoint);

    public DistanceRequestBuilder WithTravelMode(TravelMode travelMode);
    public DistanceRequestBuilder WithRoutingPreference(RoutingPreference routingPreference);

    public DistanceRequestBuilder WithDepartureTime(DateTimeOffset departureTime);
    public DistanceRequestBuilder WithArrivalTime(DateTimeOffset arrivalTime);

    public DistanceRequestBuilder WithUnits(Units units);
    public DistanceRequestBuilder WithTrafficModel(TrafficModel trafficModel);

    public DistanceRequestBuilder WithTransitPreferences(TransitPreferences transitPreferences);

    public DistanceRequest Build();
}
```

#### ⚙️ Configuration

- **Origins & Destinations**
    > Each **origin is paired** with each **destination**  
    > Total elements = origins × destinations (max: 100)
    
    - **Adding Origins**
        - [RouteMatrixOrigin](/docs/routes/routing-components.md#routematrixorigin) -> Full control (location + optional modifiers)
        - [Waypoint](/docs/routes/routing-components.md#waypoint) -> Simplified overload (modifiers optional)
        - [RouteModifiers](/docs/routes/routing-components.md#route-modifiers) -> Optional constraints (avoid tolls, vehicle info, etc.)

    ```csharp
    AddOrigin(RouteMatrixOrigin origin)
    AddOrigin(Waypoint waypoint, RouteModifiers? modifiers = null)
    WithOrigins(IEnumerable<RouteMatrixOrigin> origins)
    ```

    - **Adding Destinations**
        - [RouteMatrixDestination](/docs/routes/routing-components.md#routematrixdestination) -> Explicit destinatio wrapper
        - [Waypoint](/docs/routes/routing-components.md#waypoint) -> Simplified overload

    ```csharp
    AddDestination(RouteMatrixDestination destination)
    AddDestination(Waypoint waypoint)
    WithDestinations(IEnumerable<RouteMatrixDestination> destinations)
    ```

- **Routing** (Optional)
    - WithTravelMode([travelMode](/docs/routes/enums.md#travelmode))
    - WithRoutingPreference([routingPreference](/docs/routes/enums.md#routingpreference))
- **Time** (Optional)
    > There are **mutually exclusive**
    - WithDepartureTime(...)
    - WithArrivalTime(...) 
- **Measures** (Optional)
    - WithUnits([unit](/docs/routes/enums.md#units))
- **Traffic** (Optional)
    - WithTrafficModel([trafficModel](/docs/routes/enums.md#trafficmodel))
- **Transit** (Optional)
    > Requires `TravelMode.Transit`
    - WithTransitPreferences([transitPreferences](/docs/routes/routing-components.md#transitpreferences)) 

#### ⚠️ Validation

Throws [MapsInvalidRequestException](/docs/core/exceptions.md#️-mapsinvalidrequestexception)
if:

- Origins or destinations are missing
- origins × destinations > 100
- Both arrival and departure times are set
- Transit preferences are used with non-transit mode
- Any enum is set to `Unknown`

#### 🧠 Notes

- Uses a **builder pattern** to prevent invalid states
- Ensures API constraints are respected before execution
- Automatically validates nested objects (e.g., `RouteModifiers`, `TransitPreferences`)