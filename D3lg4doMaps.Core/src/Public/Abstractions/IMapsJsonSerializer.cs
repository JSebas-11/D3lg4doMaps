namespace D3lg4doMaps.Core.Public.Abstractions;

/// <summary>
/// Provides JSON serialization and deserialization functionality used by the Maps client.
/// </summary>
/// <remarks>
/// This abstraction allows customizing the JSON serialization strategy used internally
/// by the SDK.
/// </remarks>
public interface IMapsJsonSerializer {
    /// <summary>
    /// Serializes the specified object into a JSON string.
    /// </summary>
    /// <param name="value">
    /// The object to serialize.
    /// </param>
    /// <returns>
    /// A JSON string representation of the provided object.
    /// </returns>
    string Serialize(object value);
        
    /// <summary>
    /// Deserializes the specified JSON string into the provided type.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object to deserialize.
    /// </typeparam>
    /// <param name="json">
    /// The JSON string to deserialize.
    /// </param>
    /// <returns>
    /// The deserialized object instance, or <c>null</c> if the JSON content is empty
    /// or cannot be converted.
    /// </returns>
    T? Deserialize<T>(string json);
}