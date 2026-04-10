using D3lg4doMaps.Core.Public.Models;
using D3lg4doMaps.Core.Public.Models.Http;

namespace D3lg4doMaps.Core.Public.Abstractions;

/// <summary>
/// Represents a client capable of sending requests to a Maps API endpoint.
/// </summary>
/// <remarks>
/// Implementations of this interface are responsible for executing HTTP requests,
/// handling serialization, and returning the deserialized response model.
/// </remarks>
public interface IMapsApiClient {
    /// <summary>
    /// Sends a request to the Maps API and deserializes the response into the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// The expected type of the deserialized response.
    /// </typeparam>
    /// <param name="apiRequest">
    /// The request configuration containing endpoint, headers, payload, HTTP method, etc.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains the
    /// deserialized response of type <typeparamref name="T"/>.
    /// </returns>
    Task<T> SendAsync<T>(MapsApiRequest apiRequest);

    /// <summary>
    /// Sends a request to the Maps API and returns the response as a streamed result.
    /// </summary>
    /// <param name="apiRequest">
    /// The request configuration containing endpoint, headers, payload, HTTP method, etc.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains a
    /// <see cref="StreamResponse"/> that provides access to the response stream and
    /// its underlying HTTP response.
    /// </returns>
    /// <remarks>
    /// This method is intended for endpoints that return streaming responses
    /// (e.g., Distance Matrix), where the payload may consist of multiple JSON objects
    /// rather than a single JSON document.
    /// <para>
    /// The returned <see cref="StreamResponse"/> owns the lifetime of both the response
    /// stream and the underlying <see cref="HttpResponseMessage"/>. The caller must dispose
    /// it after use to ensure all resources are properly released.
    /// </para>
    /// </remarks>
    Task<StreamResponse> SendStreamAsync(MapsApiRequest apiRequest);
}