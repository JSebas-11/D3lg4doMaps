using D3lg4doMaps.Core.Public.Models;

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
}