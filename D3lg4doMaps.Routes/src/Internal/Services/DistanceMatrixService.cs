using System.Text.Json;
using D3lg4doMaps.Core.Public.Abstractions;
using D3lg4doMaps.Core.Public.Enums;
using D3lg4doMaps.Core.Public.Models;
using D3lg4doMaps.Routes.Internal.Constants;
using D3lg4doMaps.Routes.Internal.Factories;
using D3lg4doMaps.Routes.Internal.Mapping.Mappers;
using D3lg4doMaps.Routes.Public.Abstractions;
using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.DistanceMatrix;
using D3lg4doMaps.Routes.Public.Models.Requests;

namespace D3lg4doMaps.Routes.Internal.Services;

internal class DistanceMatrixService : IDistanceMatrixService {
    // -------------------- INIT --------------------
    private readonly IMapsApiClient _apiClient;
    private readonly IMapsJsonSerializer _serializer;
    public DistanceMatrixService(IMapsApiClient apiClient, IMapsJsonSerializer serializer) {
        _apiClient = apiClient;
        _serializer = serializer;
    }

    // -------------------- METHS --------------------
    public async Task<IReadOnlyList<RouteMatrixElement>> GetDistancesAsync(
        DistanceRequest distanceRequest, RouteDetailLevel detailLevel = RouteDetailLevel.Standard
        ) 
    {
        var fields = FieldMaskFactory.GetDistanceMatrixFieldMask(detailLevel);
        var headers = new Dictionary<string, string>() { 
            { "X-Goog-FieldMask", fields } 
        }; 
        var request = CreateRequest(headers, distanceRequest);
        
        var streamResponse = await _apiClient.SendStreamAsync(request);

        List<RouteMatrixElement> results = [];
        await foreach (var item in _serializer.DeserializeStreamAsync<JsonDocument>(streamResponse)) {
            try {
                results.Add(DistanceMatrixMapper.ToRouteMatrixElement(item));
            }
            finally { item.Dispose(); }
        };

        return results;
    }
    // -------------------- INNER METHS --------------------
    private MapsApiRequest CreateRequest(IDictionary<string, string> headers, DistanceRequest request)
        => new () {
            ApiKeyLocation = ApiKeyLocation.Header,
            Method = HttpMethod.Post,
            BaseUrl = RoutesEndpoints.BaseUrl,
            Endpoint = RoutesEndpoints.DistanceMatrixCompute,
            Headers = headers,
            Payload = DistanceRequestMapper.ToDistanceRequestDto(request)
        };
}