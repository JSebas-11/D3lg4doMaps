using D3lg4doMaps.Core.Public.Models;

namespace D3lg4doMaps.Core.Public.Abstractions;

/// <summary>
/// Creates HTTP requests based on a <see cref="MapsApiRequest"/> definition.
/// </summary>
/// <remarks>
/// Implementations are responsible for translating the SDK request model into
/// an <see cref="HttpRequestMessage"/> ready to be sent through an HTTP client.
/// </remarks>
public interface IRequestFactory {
    /// <summary>
    /// Creates an <see cref="HttpRequestMessage"/> from the provided request definition.
    /// </summary>
    /// <param name="request">
    /// The request configuration containing endpoint, headers, payload, HTTP method, etc.
    /// </param>
    /// <returns>
    /// A fully configured <see cref="HttpRequestMessage"/> instance.
    /// </returns>
    HttpRequestMessage CreateRequest(MapsApiRequest request);
}