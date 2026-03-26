# 📦 D3lg4doMaps SDK

A modern, strongly-typed C# SDK for Google Maps Platform (Places, Directions, Distance Matrix, and more).

---

## 🚀 Why D3lg4doMaps?

Working with Google Maps APIs directly can be verbose and error-prone.

D3lg4doMaps provides:

- Strongly-typed models
- Clean abstractions
- Fluent request builders
- Modular architecture (Core, Places, Routing)

---

## 📚 Documentation

👉 Full documentation: https://jsebas-11.github.io/D3lg4doMaps/

Includes:

- Getting Started
- Configuration
- Places (Search, Autocomplete, Details)
- Routing (coming soon)
- Advanced usage

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

var results = await places.Search.SearchByTextAsync("coffee near me");
```


---

## 📦 Installation

```bash
dotnet add package D3lg4doMaps.Core
dotnet add package D3lg4doMaps.Places
```

--- 

## 🧩 Modules

- Core → Configuration, HTTP, infrastructure  
- Places → Search, Autocomplete, Details  
- Routing → Directions, DistanceMatrix *(coming soon)*  

---

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

---

## 📄 License
This project is licensed under the MIT License. See the `LICENSE` file for details.

---

## 🙌 Credits
Created & maintained by JSebas-11 (Sebastian Delgado)
Designed for clean architecture, developer ergonomics, and real-world Maps integrations.

