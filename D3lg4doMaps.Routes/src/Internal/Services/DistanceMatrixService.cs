using System.Text.Json;
using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Enums;
using DelgadoMaps.Core.Models;
using DelgadoMaps.Routes.Internal.Constants;
using DelgadoMaps.Routes.Internal.Factories;
using DelgadoMaps.Routes.Internal.Mapping.Mappers;
using DelgadoMaps.Routes.Abstractions;
using DelgadoMaps.Routes.Enums;
using DelgadoMaps.Routes.Models.DistanceMatrix;
using DelgadoMaps.Routes.Models.Requests;

namespace DelgadoMaps.Routes.Internal.Services;

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
        
        var streamResponse = await _apiClient.SendStreamAsync(request).ConfigureAwait(false);

        List<RouteMatrixElement> results = [];
        await foreach (var item in _serializer.DeserializeStreamAsync<JsonDocument>(streamResponse).ConfigureAwait(false)) {
            try {
                results.Add(DistanceMatrixMapper.ToRouteMatrixElement(item));
            }
            finally { item.Dispose(); }
        };

        return results;
    }
    // -------------------- INNER METHS --------------------
    private static MapsApiRequest CreateRequest(IDictionary<string, string> headers, DistanceRequest request)
        => new () {
            ApiKeyLocation = ApiKeyLocation.Header,
            Method = HttpMethod.Post,
            BaseUrl = RoutesEndpoints.BaseUrl,
            Endpoint = RoutesEndpoints.DistanceMatrixCompute,
            Headers = headers,
            Payload = DistanceRequestMapper.ToDistanceRequestDto(request)
        };
}