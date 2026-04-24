using System.Text.Json;
using DelgadoMaps.Core.Models.Http;

namespace DelgadoMaps.Core.Abstractions;

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

    /// <summary>
    /// Deserializes a streamed JSON response into an asynchronous sequence of objects of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the objects to deserialize from the stream.
    /// </typeparam>
    /// <param name="response">
    /// The <see cref="StreamResponse"/> containing the response stream and its associated HTTP response.
    /// </param>
    /// <returns>
    /// An <see cref="IAsyncEnumerable{T}"/> that yields deserialized objects as they are read from the stream.
    /// </returns>
    /// <remarks>
    /// This method is designed for streaming scenarios where the response contains
    /// multiple JSON objects (e.g., newline-delimited JSON).
    /// <para>
    /// The method consumes the underlying stream from the provided <see cref="StreamResponse"/>
    /// and processes objects incrementally as they are read.
    /// </para>
    /// <para>
    /// The <see cref="StreamResponse"/> is disposed internally once the asynchronous enumeration
    /// completes, ensuring proper release of all underlying resources.
    /// The caller must not dispose the <see cref="StreamResponse"/> when using this method.
    /// </para>
    /// <para>
    /// Failure to fully enumerate the returned <see cref="IAsyncEnumerable{T}"/> may result in
    /// delayed disposal of resources.
    /// </para>
    /// <para>
    /// <b>Important:</b> When <typeparamref name="T"/> is <see cref="JsonDocument"/>, each returned
    /// instance must be explicitly disposed by the caller to avoid memory leaks, as <see cref="JsonDocument"/>
    /// allocates unmanaged resources.
    /// </para>
    /// </remarks>
    IAsyncEnumerable<T> DeserializeStreamAsync<T>(StreamResponse response);
}