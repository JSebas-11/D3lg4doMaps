using DelgadoMaps.Core.Internal.Abstractions;
using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Configuration;
using DelgadoMaps.Core.Enums;
using DelgadoMaps.Core.Models;

namespace DelgadoMaps.Core.Internal.Factories;

internal class RequestFactory : IRequestFactory {
    // -------------------- INIT --------------------
    private readonly Dictionary<string, string> _defaultConfig = [];
    private readonly Dictionary<string, string> _keyInHeader = [];
    private readonly Dictionary<string, string> _keyInQuery = [];
    private readonly MapsConfiguration _config;
    private readonly IRequestBuilder _builder;

    public RequestFactory(MapsConfiguration config, IRequestBuilder builder) {
        _builder = builder;
        _config = config;
        _defaultConfig = new() {
            { "Accept-Language", $"{_config.Language}-{_config.Region}" }
        };
        _keyInHeader = new() { { "X-Goog-Api-Key", _config.ApiKey } };
        _keyInQuery = new() { { "key", _config.ApiKey } };
    }

    // -------------------- METHS --------------------
    public HttpRequestMessage CreateRequest(MapsApiRequest request) {
        _builder.SetMethod(request.Method);
        _builder.SetPath(request.BaseUrl, request.Endpoint);
        
        // Default config, it will be overwritten if it is defined in request
        _builder.AddHeaders(_defaultConfig);

        if (request?.Headers?.Count > 0) 
            _builder.AddHeaders(request.Headers);

        if (request?.Query?.Count > 0) 
            _builder.AddQuery(request.Query);

        if (request?.Payload is not null) 
            _builder.SetJsonPayload(request.Payload);

        // Key inscription
        switch (request?.ApiKeyLocation) {
            case ApiKeyLocation.Query: _builder.AddQuery(_keyInQuery); break;
            
            case ApiKeyLocation.Header: 
            default:
                _builder.AddHeaders(_keyInHeader); break;
        }

        return _builder.Build();
    }
}