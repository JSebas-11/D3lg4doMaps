---
sidebar_position: 1
title: Introduction
---

# D3lg4doMaps

**D3lg4doMaps** is a modern, modular .NET SDK that simplifies interaction with Google Maps APIs.

It provides clean, strongly-typed abstractions over complex HTTP endpoints, allowing you to focus on building features instead of handling low-level API details.

---

## 🚀 Why D3lg4doMaps?

Working directly with Google Maps APIs can be:

- Verbose
- Error-prone
- Hard to maintain

D3lg4doMaps solves this by:

- ✅ Centralizing configuration in a Core module  
- ✅ Providing high-level services (Places, Routing, etc.)  
- ✅ Enforcing clean architecture and best practices  
- ✅ Offering a fluent and developer-friendly API  

---

## 🧩 Architecture Overview

D3lg4doMaps is built around a **Core-first architecture**:

- **Core** → Configuration, HTTP handling, shared infrastructure  
- **Places** → Search, Autocomplete, Details, Reviews, Photos  
- **Routing** → Directions, DistanceMatrix *(coming soon)*  

All feature modules depend on Core, ensuring consistency and simplicity.

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

var result = await places.Search.SearchByTextAsync("coffee near me");
```

---

## 📦 Installation

Install via NuGet:

```bash
dotnet add package D3lg4doMaps.Core
dotnet add package D3lg4doMaps.Places
```

---

## 🛠️ Next Steps

- 👉 Configure Core → [Core Overview](core/overview)
- 👉 Start using Places → [Places Overview](places/overview)

---

## 💡 Design Philosophy

D3lg4doMaps follows a few key principles:

- **Simplicity first** → minimal boilerplate
- **Strong typing** → fewer runtime errors
- **Modularity** → use only what you need
- **Clean abstractions** → no API noise

---

## 🔗 Links

- GitHub: https://github.com/JSebas-11/D3lg4doMaps
- NuGet: https://www.nuget.org/packages?q=D3lg4doMaps