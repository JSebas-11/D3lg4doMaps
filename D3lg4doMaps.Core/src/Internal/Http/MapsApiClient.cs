using DelgadoMaps.Core.Internal.Handlers;
using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Core.Models;
using DelgadoMaps.Core.Models.Http;
using Microsoft.Extensions.Logging;
using DelgadoMaps.Core.Internal.Http.Caching;
using Microsoft.Extensions.DependencyInjection;

namespace DelgadoMaps.Core.Internal.Http;

internal class MapsApiClient : IMapsApiClient {
    // -------------------- INIT --------------------
    private readonly HttpClient _httpClient;
    private readonly ILogger<MapsApiClient> _logger;
    private readonly IMapsJsonSerializer _serializer;
    private readonly IRequestFactory _reqFactory;
    private readonly IHttpCacheManager? _cache;

    public MapsApiClient(
        HttpClient httpClient, ILogger<MapsApiClient> logger,
        IMapsJsonSerializer serializer, IRequestFactory requestFactory,
        IServiceProvider service
    ) {
        _httpClient = httpClient;
        _logger = logger;
        _serializer = serializer;
        _reqFactory = requestFactory;
        
        _cache = service.GetService<IHttpCacheManager>();
    }

    // -------------------- METHS --------------------
    public async Task<T> SendAsync<T>(MapsApiRequest apiRequest) {
        // Try get cached value if is activated and it exists
        if (_cache is not null) {
            using var cachedResponse = await _cache.GetCachedResponseAsync(apiRequest)
                .ConfigureAwait(false);
                
            if (cachedResponse is not null)
                return await DeserializeOrExceptionAsync<T>(cachedResponse).ConfigureAwait(false);
        }

        // This client fully owns response (consumes & disposes) 
        using var response = await CreateAndSendAsync(
            apiRequest, HttpCompletionOption.ResponseContentRead)
            .ConfigureAwait(false);

        ExceptionHandler.Handle(response.StatusCode);
        
        // Cache it is activated
        if (_cache is not null)
            await _cache.CacheResponseAsync(apiRequest, response).ConfigureAwait(false);

        return await DeserializeOrExceptionAsync<T>(response).ConfigureAwait(false);
    }

    public async Task<StreamResponse> SendStreamAsync(MapsApiRequest apiRequest) {
        // HttpResponseMessage is not disposed here, since ownership is transferred to StreamResponse
        // Caller is responsible for disposing it
        var response = await CreateAndSendAsync(apiRequest, HttpCompletionOption.ResponseHeadersRead)
            .ConfigureAwait(false);

        ExceptionHandler.Handle(response.StatusCode);

        var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

        return new StreamResponse(stream, response);
    }

    // -------------------- INNER METHS --------------------
    private async Task<HttpResponseMessage> CreateAndSendAsync(
        MapsApiRequest apiRequest, HttpCompletionOption completionOption
    ) {
        var request = _reqFactory.CreateRequest(apiRequest);
        var response = await _httpClient.SendAsync(request, completionOption).ConfigureAwait(false);
        
        return response;
    }

    private async Task<T> DeserializeOrExceptionAsync<T>(HttpResponseMessage response) {
        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var result = _serializer.Deserialize<T>(json)
            ?? throw new MapsApiException($"Response could not be deserialize to type {typeof(T).Name}");

        return result;
    }
}