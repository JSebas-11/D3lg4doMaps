using D3lg4doMaps.Core.Internal.Abstractions;
using D3lg4doMaps.Core.Public.Abstractions;
using D3lg4doMaps.Core.Public.Configuration;
using D3lg4doMaps.Core.Public.Models;

namespace D3lg4doMaps.Core.Internal.Factories;

internal class RequestFactory : IRequestFactory {
    // -------------------- INIT --------------------
    private readonly Dictionary<string, string> _defaultConfig = [];
    private readonly MapsConfiguration _config;
    private readonly IRequestBuilder _builder;

    public RequestFactory(MapsConfiguration config, IRequestBuilder builder) {
        _builder = builder;
        _config = config;
        _defaultConfig = new() { 
            { "X-Goog-Api-Key", _config.ApiKey }, 
            { "Accept-Language", $"{_config.Language}-{_config.Region}" }
        };
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

        return _builder.Build();
    }
}