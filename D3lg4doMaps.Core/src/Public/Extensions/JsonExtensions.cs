using System.Text.Json;

namespace D3lg4doMaps.Core.Public.Extensions;

/// <summary>
/// Provides helper extension methods for safely reading values from a <see cref="JsonElement"/>.
/// </summary>
/// <remarks>
/// These methods simplify navigation through JSON structures by combining property lookup
/// and type validation. If the property does not exist or does not match the expected JSON type,
/// the methods return <c>null</c> or an empty sequence instead of throwing an exception.
///
/// This allows mappers and parsers to safely extract values from external API responses
/// without repetitive checks for property existence or <see cref="JsonValueKind"/> validation.
/// </remarks>
public static class JsonExtensions {
    /// <summary>
    /// Attempts to retrieve a JSON object property from the specified element.
    /// </summary>
    /// <param name="element">The JSON element containing the property.</param>
    /// <param name="prop">The name of the property to retrieve.</param>
    /// <returns>
    /// The <see cref="JsonElement"/> representing the object if the property exists and is of type
    /// <see cref="JsonValueKind.Object"/>; otherwise <c>null</c>.
    /// </returns>
    public static JsonElement? GetObject(this JsonElement element, string prop) {
        if (element.TryGetProperty(prop, out var value) && value.ValueKind == JsonValueKind.Object)
            return value;

        return null;
    }
    
    /// <summary>
    /// Attempts to retrieve an integer property from the specified JSON element.
    /// </summary>
    /// <param name="element">The JSON element containing the property.</param>
    /// <param name="prop">The name of the property to retrieve.</param>
    /// <returns>
    /// The integer value if the property exists and is a number; otherwise <c>null</c>.
    /// </returns>
    public static int? GetInt(this JsonElement element, string prop) {
        if (element.TryGetProperty(prop, out var value) && value.ValueKind == JsonValueKind.Number)
            return value.GetInt32();

        return null;
    }
    
    /// <summary>
    /// Attempts to retrieve a single-precision floating-point value from the specified JSON element.
    /// </summary>
    /// <param name="element">The JSON element containing the property.</param>
    /// <param name="prop">The name of the property to retrieve.</param>
    /// <returns>
    /// The <see cref="float"/> value if the property exists and is a number; otherwise <c>null</c>.
    /// </returns>
    public static float? GetFloat(this JsonElement element, string prop) {
        if (element.TryGetProperty(prop, out var value) && value.ValueKind == JsonValueKind.Number)
            return value.GetSingle();

        return null;
    }
    
    /// <summary>
    /// Attempts to retrieve a double-precision floating-point value from the specified JSON element.
    /// </summary>
    /// <param name="element">The JSON element containing the property.</param>
    /// <param name="prop">The name of the property to retrieve.</param>
    /// <returns>
    /// The <see cref="double"/> value if the property exists and is a number; otherwise <c>null</c>.
    /// </returns>
    public static double? GetDoubleValue(this JsonElement element, string prop) {
        if (element.TryGetProperty(prop, out var value) && value.ValueKind == JsonValueKind.Number)
            return value.GetDouble();

        return null;
    }
    
    /// <summary>
    /// Attempts to retrieve a boolean property from the specified JSON element.
    /// </summary>
    /// <param name="element">The JSON element containing the property.</param>
    /// <param name="prop">The name of the property to retrieve.</param>
    /// <returns>
    /// The boolean value if the property exists and is either <see cref="JsonValueKind.True"/>
    /// or <see cref="JsonValueKind.False"/>; otherwise <c>null</c>.
    /// </returns>
    public static bool? GetBool(this JsonElement element, string prop) {
        if (element.TryGetProperty(prop, out var value) &&
            (value.ValueKind == JsonValueKind.False || value.ValueKind == JsonValueKind.True))
            return value.GetBoolean();

        return null;
    }
    
    /// <summary>
    /// Attempts to retrieve a string property from the specified JSON element.
    /// </summary>
    /// <param name="element">The JSON element containing the property.</param>
    /// <param name="prop">The name of the property to retrieve.</param>
    /// <returns>
    /// The string value if the property exists and is of type <see cref="JsonValueKind.String"/>;
    /// otherwise <c>null</c>.
    /// </returns>
    public static string? GetStringValue(this JsonElement element, string prop) {
        if (element.TryGetProperty(prop, out var value) && 
            value.ValueKind == JsonValueKind.String)
            return value.GetString();

        return null;
    }
    
    /// <summary>
    /// Retrieves a JSON array property and returns its elements.
    /// </summary>
    /// <param name="element">The JSON element containing the property.</param>
    /// <param name="prop">The name of the array property.</param>
    /// <returns>
    /// An <see cref="IEnumerable{JsonElement}"/> containing the array items if the property exists
    /// and is of type <see cref="JsonValueKind.Array"/>; otherwise an empty sequence.
    /// </returns>
    public static IEnumerable<JsonElement> GetArray(this JsonElement element, string prop) {
        if (!element.TryGetProperty(prop, out var value) || 
            value.ValueKind != JsonValueKind.Array)    
            yield break;

        foreach (var item in value.EnumerateArray())
            yield return item;
    }
}