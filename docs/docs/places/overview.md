---
sidebar_position: 1
title: Places Overview
---

:::info Namespace update (v2.0.0)
Namespaces were changed, update your `using` statements accordingly.

👉 See the full migration guide here: [v2 Migration Guide](/docs/migration/v2.md)
:::

# 📍 Places Overview

The **Places** module provides a powerful and developer-friendly way to interact with Google Places APIs.

It allows you to **search**, **autocomplete**, and **retrieve rich details** about places using clean, strongly-typed abstractions.

---

## 🚀 What You Can Do

- 🔎 Search places by text or location  
- ✍️ Autocomplete user input with real-time suggestions  
- 📍 Retrieve detailed place information  
- 📸 Fetch place photos with configurable resolution  
- ⭐ Access user reviews and AI summaries  

---

## 🧩 Architecture

The module is built around a set of focused services:

- `IAutocompleteService` → Suggestions based on partial input  
- `ISearchService` → Text and nearby search capabilities  
- `IDetailsService` → Rich place data (details, photos, reviews)  
- `IPlacesService` → Unified entry point for all services  

> 👉 See: [Abstractions](/docs/places/abstractions.md)

---

## ⚡ Quick Example

```csharp
var services = new ServiceCollection();

services.AddD3lg4doMaps(new MapsConfiguration {
    ApiKey = "YOUR_API_KEY"
});
services.AddD3lg4doMapsPlaces();

var provider = services.BuildServiceProvider();
var places = provider.GetRequiredService<IPlacesService>();

var results = await places.Search.SearchByTextAsync("coffee near Medellin");
```

---

## 🧠 Design Highlights

- Clean separation of concerns across services
- Strongly-typed models for safer development
- Fluent builders for complex request construction
- Extensible and testable architecture
- Async-first for performance and scalability

---

## 🔗 Next Steps

- 👉 Autocomplete → [AutocompleteService](/docs/places/services/autocomplete.md)
- 👉 Search → [SearchService](/docs/places/services/search.md)
- 👉 Details → [DetailsService](/docs/places/services/details.md)
- 👉 Dependency Injection → [Places Injection](/docs/places/extensions.md#-dependency-injection)

---

## 💡 When to Use What?

- Use **Autocomplete** for user input assistance
- Use **Search** for discovering places
- Use **Details** for rich, in-depth information