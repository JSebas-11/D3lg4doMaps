using System.Text.Json;
using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Enums;
using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Core.Models;
using DelgadoMaps.Routes.Internal.Constants;
using DelgadoMaps.Routes.Internal.Factories;
using DelgadoMaps.Routes.Internal.Mapping.Mappers;
using DelgadoMaps.Routes.Abstractions;
using DelgadoMaps.Routes.Enums;
using DelgadoMaps.Routes.Models.Directions;
using DelgadoMaps.Routes.Models.Requests;

namespace DelgadoMaps.Routes.Internal.Services;

internal class DirectionsService : IDirectionsService {
    // -------------------- INIT --------------------
    private readonly IMapsApiClient _apiClient;
    public DirectionsService(IMapsApiClient apiClient) {
        _apiClient = apiClient;
    }
    
    // -------------------- METHS --------------------
    public async Task<RouteResult> GetRoutesAsync(
        RouteRequest routeRequest, RouteDetailLevel detailLevel = RouteDetailLevel.Standard 
        ) { 
            var fields = FieldMaskFactory.GetFieldMask(routeRequest, detailLevel); 
            using var json = await GetRoutesRawAsync(routeRequest, fields).ConfigureAwait(false); 
            return DirectionsMapper.ToRouteResult(json); 
        } 
    
    public async Task<JsonDocument> GetRoutesRawAsync(RouteRequest routeRequest, params string[] fields) {
        if (fields.Length == 0) 
            throw new MapsInvalidRequestException("Fields must not be empty (FieldMask is a required parameter)."); 
        
        var headers = new Dictionary<string, string>() { 
            { "X-Goog-FieldMask", string.Join(',', fields) } 
        }; 
        var request = CreateRequest(headers, routeRequest); 
        return await _apiClient.SendAsync<JsonDocument>(request).ConfigureAwait(false); 
    }

    // -------------------- INNER METHS --------------------
    private static MapsApiRequest CreateRequest(IDictionary<string, string> headers, RouteRequest request)
        => new () {
            ApiKeyLocation = ApiKeyLocation.Header,
            Method = HttpMethod.Post,
            BaseUrl = RoutesEndpoints.BaseUrl,
            Endpoint = RoutesEndpoints.DirectionsComputeRoutes,
            Headers = headers,
            Payload = RouteRequestMapper.ToRouteRequestDto(request)
        };
}