using System.Net.Http.Json;
using DelgadoMaps.Core.Internal.Abstractions;

namespace DelgadoMaps.Core.Internal.Builders;

internal class RequestBuilder : IRequestBuilder {
    // -------------------- INIT --------------------
    private readonly IMapsUriBuilder _uriBuilder;

    private HttpMethod _method = HttpMethod.Get;
    private IDictionary<string, string> _headers = new Dictionary<string, string> ();
    private object? _payload;

    public RequestBuilder(IMapsUriBuilder uriBuilder) 
        => _uriBuilder = uriBuilder;

    // -------------------- BUILD --------------------
    public HttpRequestMessage Build() {
        var request = new HttpRequestMessage() {
            Method = _method,
            RequestUri = _uriBuilder.Build()
        };

        foreach (var item in _headers)
            request.Headers.TryAddWithoutValidation(item.Key, item.Value);

        if (_payload is not null)
            request.Content = JsonContent.Create(_payload);

        Reset();
        return request;
    }

    private void Reset() {
        _headers.Clear();
        _method = HttpMethod.Get;
        _payload = null;
    }

    // -------------------- CONFIG --------------------
    public IRequestBuilder SetMethod(HttpMethod method) {
        _method = method;
        return this;
    }

    public IRequestBuilder SetPath(string baseUrl, string endpoint) {
        _uriBuilder.WithPath(baseUrl, endpoint);
        return this;
    }

    public IRequestBuilder AddHeaders(IDictionary<string, string> headers) {
        foreach (var h in headers)
            _headers[h.Key] = h.Value;

        return this;
    }

    public IRequestBuilder AddQuery(IDictionary<string, string> query) {
        _uriBuilder.AddQuery(query);
        return this;
    }

    public IRequestBuilder SetJsonPayload(object payload) {
        _payload = payload;
        return this;
    }
}