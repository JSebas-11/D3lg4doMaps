using D3lg4doMaps.Core.Internal.Handlers;
using D3lg4doMaps.Core.Public.Abstractions;
using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Core.Public.Models;
using Microsoft.Extensions.Logging;

namespace D3lg4doMaps.Core.Internal.Http;

internal class MapsApiClient : IMapsApiClient {
    // -------------------- INIT --------------------
    private readonly HttpClient _httpClient;
    private readonly ILogger<MapsApiClient> _logger;
    private readonly IMapsJsonSerializer _serializer;
    private readonly IRequestFactory _reqFactory;

    public MapsApiClient(
        HttpClient httpClient, ILogger<MapsApiClient> logger,
        IMapsJsonSerializer serializer, IRequestFactory requestFactory
    ) {
        _httpClient = httpClient;
        _logger = logger;
        _serializer = serializer;
        _reqFactory = requestFactory;
    }

    // -------------------- METHS --------------------
    public async Task<T> SendAsync<T>(MapsApiRequest apiRequest) {
        var request = _reqFactory.CreateRequest(apiRequest);

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        
        ExceptionHandler.Handle(response.StatusCode);

        return await DeserializeOrExceptionAsync<T>(response).ConfigureAwait(false);
    }

    // -------------------- INNER METHS --------------------
    private async Task<T> DeserializeOrExceptionAsync<T>(HttpResponseMessage response) {
        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var result = _serializer.Deserialize<T>(json)
            ?? throw new MapsApiException($"Response could not be deserialize to type {typeof(T).Name}");

        return result;
    }
}