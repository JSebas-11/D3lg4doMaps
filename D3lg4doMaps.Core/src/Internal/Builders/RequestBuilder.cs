using System.Net.Http.Json;
using D3lg4doMaps.Core.Internal.Abstractions;

namespace D3lg4doMaps.Core.Internal.Http.Builders;

internal class RequestBuilder : IRequestBuilder {
    // -------------------- INIT --------------------
    private readonly IMapsUriBuilder _uriBuilder;

    private readonly HttpRequestMessage _request = new();
    private object? _payload;

    public RequestBuilder(IMapsUriBuilder uriBuilder) 
        => _uriBuilder = uriBuilder;

    // -------------------- METHS --------------------
    public HttpRequestMessage Build() {
        if (_payload is not null)
            _request.Content = JsonContent.Create(_payload);

        _request.RequestUri = _uriBuilder.Build();
        return _request;
    }

    public IRequestBuilder SetMethod(HttpMethod method) {
        _request.Method = method;
        return this;
    }

    public IRequestBuilder SetPath(string baseUrl, string endpoint) {
        _uriBuilder.WithPath(baseUrl, endpoint);
        return this;
    }

    public IRequestBuilder AddHeaders(IDictionary<string, string> headers) {
        foreach (var h in headers)
            _request.Headers.TryAddWithoutValidation(h.Key, h.Value);

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