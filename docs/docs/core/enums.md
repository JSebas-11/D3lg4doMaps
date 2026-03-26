---
title: Enums
---

# 📦 Core Enums

This section contains the enum types used across the SDK.

These represent shared constants that control SDK behavior.

---

## 🧾 ApiKeyLocation

Specifies where the API key should be included in an HTTP request.

```csharp
public enum ApiKeyLocation {
    Header,
    Query
}
```

### ⚙️ Values

- `Header` → Indicates that the API key is sent in the request headers using the **X-Goog-Api-Key header**.
- `Query` → Indicates that the API key is sent as a query parameter using the **key parameter**.

### 🧠 Notes

- Header is the **default and recommended** option
- Some endpoints (e.g., media/photo endpoints) require Query