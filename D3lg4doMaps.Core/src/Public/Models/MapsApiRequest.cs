namespace D3lg4doMaps.Core.Public.Models;

public sealed class MapsApiRequest {
    public HttpMethod Method { get; init; } = HttpMethod.Get;
    public string BaseUrl { get; init; } = null!;
    public string Endpoint { get; init; } = null!;
    public IDictionary<string, string>? Headers { get; init; }
    public IDictionary<string, string>? Query { get; init; }
    public object? Payload { get; init; }
}