namespace D3lg4doMaps.Core.Public.Models;

/// <summary>
/// Represents a request definition used to call a Maps API endpoint.
/// </summary>
/// <remarks>
/// This model describes all the information required to construct an HTTP request,
/// including the HTTP method, base URL, endpoint path, headers, query parameters,
/// and an optional request payload.
/// </remarks>
public sealed class MapsApiRequest {
    /// <summary>
    /// Gets the HTTP method used to send the request.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="HttpMethod.Get"/>.
    /// </remarks>
    public HttpMethod Method { get; init; } = HttpMethod.Get;

    /// <summary>
    /// Gets the base URL of the API endpoint.
    /// </summary>
    /// <example>
    /// https://places.googleapis.com/v1/
    /// </example>
    public string BaseUrl { get; init; } = null!;

    /// <summary>
    /// Gets the relative endpoint path for the request.
    /// </summary>
    /// <example>
    /// places:autocomplete
    /// </example>
    public string Endpoint { get; init; } = null!;

    /// <summary>
    /// Gets the HTTP headers that should be included in the request.
    /// </summary>
    public IDictionary<string, string>? Headers { get; init; }

    /// <summary>
    /// Gets the query parameters appended to the request URL.
    /// </summary>
    public IDictionary<string, string>? Query { get; init; }

    /// <summary>
    /// Gets the request payload that will be serialized as JSON.
    /// </summary>
    /// <remarks>
    /// This value is typically used for POST requests where the API expects
    /// a JSON body.
    /// </remarks>
    public object? Payload { get; init; }
}