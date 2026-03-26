---
title: Autocomplete
---

# 🔎 Autocomplete

The **Autocomplete feature** provides real-time place suggestions based on partial user input.

It is commonly used to power:

- Search bars
- Location inputs
- Address forms

---

## 🔗 Related Abstractions

This feature is exposed through:

👉 [IAutocompleteService](/docs/places/Abstractions.md#-iautocompleteservice)

---


## 🧩 Service

```csharp
public interface IAutocompleteService {
    Task<IReadOnlyList<PlaceSuggestion>> SuggestPlacesAsync(
        AutocompleteRequest autocompleteRequest
    );
}
```

### ⚙️ Methods

- SuggestPlacesAsync → suggest possible places according to **user text input and geograpich bias**

---

## ⚡ Example

```csharp
var request = new AutocompleteRequestBuilder()
    .WithInput("coffee")
    .Build();

var suggestions = await autocompleteService.SuggestPlacesAsync(request);
```

---

## 📦 Models

### PlaceSuggestion

Represents a predicted place returned from an autocomplete query.

```csharp
public sealed class PlaceSuggestion {
    public string PlaceId { get; internal set; } = null!;
    public string Text { get; internal set; } = null!;
    public int EndOffset { get; internal set; } = -1;
    public IReadOnlyList<string> Types { get; internal set; } = [];
}
```

#### ⚙️ Properties

| Property    | Description                                          |
|:----------- |:---------------------------------------------------- |
| `PlaceId`   | Unique identifier of the place                       |
| `Text`      | Full suggestion text                                 |
| `EndOffset` | Index where the match ends (useful for highlighting) |
| `Types`     | Associated place types                               |

#### 🧠 Notes

- Lightweight model optimized for UI scenarios
- Typically used before fetching full place details

---

## 🧾 Request

### AutocompleteRequest

Represents a request for retrieving place suggestions.

```csharp
public sealed class AutocompleteRequest {
    public string Input { get; internal set; } = null!;
    public LocationBias? LocationBias { get; internal set; }
}
```

#### ⚙️ Properties

| Property       | Description                                           |
|:-------------- |:----------------------------------------------------- |
| `Input`        | Partial user input (e.g., `"coffee"`, `"pizza near"`) |
| `LocationBias` | Optional geographic bias for results                  |

#### 🧠 Notes

- Must be constructed using `AutocompleteRequestBuilder`
- Location bias influences ranking but does not strictly filter results

### AutocompleteRequestBuilder

Provides a fluent API to construct `AutocompleteRequest`.

```csharp
public sealed class AutocompleteRequestBuilder {
    public AutocompleteRequestBuilder WithInput(string input);
    public AutocompleteRequestBuilder WithLocationBias(double radius, double latitude, double longitude);

    public AutocompleteRequest Build();
}
```

#### ⚙️ Configuration

- `WithInput(...)` → **required**, partial user input
- `WithLocationBias(...)` → **optional**, geographic bias that influences ranking

#### ⚡ Example

```csharp
var request = new AutocompleteRequestBuilder()
    .WithInput("pizza")
    .WithLocationBias(1000, 40.7128, -74.0060)
    .Build();
```

#### ⚠️ Validation

- Throws [MapsInvalidRequestException](/docs/core/Exceptions.md#️-mapsinvalidrequestexception) if:
    - Input is null, empty, or whitespace
    - Build is called without setting input

#### 🧠 Notes

- Enforces valid request construction
- Improves readability with fluent API
- Prevents invalid states
