using System.Text.Json;

namespace D3lg4doMaps.Places.Internal.Helpers;

internal static class JsonHelpers {
    public static JsonElement? GetObject(this JsonElement element, string prop) {
        if (element.TryGetProperty(prop, out var value) && 
            value.ValueKind == JsonValueKind.Object)
            return value;

        return null;
    }
    
    public static int? GetInt(this JsonElement element, string prop) {
        if (element.TryGetProperty(prop, out var value) &&
            value.ValueKind == JsonValueKind.Number)
            return value.GetInt32();

        return null;
    }
    
    public static float? GetFloat(this JsonElement element, string prop) {
        if (element.TryGetProperty(prop, out var value) &&
            value.ValueKind == JsonValueKind.Number)
            return value.GetSingle();

        return null;
    }
    
    public static double? GetDoubleValue(this JsonElement element, string prop) {
        if (element.TryGetProperty(prop, out var value) &&
            value.ValueKind == JsonValueKind.Number)
            return value.GetDouble();

        return null;
    }
    
    public static bool? GetBool(this JsonElement element, string prop) {
        if (element.TryGetProperty(prop, out var value) &&
            (value.ValueKind == JsonValueKind.False || value.ValueKind == JsonValueKind.True))
            return value.GetBoolean();

        return null;
    }
    
    public static string? GetStringValue(this JsonElement element, string prop) {
        if (element.TryGetProperty(prop, out var value) && 
            value.ValueKind == JsonValueKind.String)
            return value.GetString();

        return null;
    }
    
    public static IEnumerable<JsonElement> GetArray(this JsonElement element, string prop) {
        if (!element.TryGetProperty(prop, out var value) || 
            value.ValueKind != JsonValueKind.Array)    
            yield break;

        foreach (var item in value.EnumerateArray())
            yield return item;
    }
}