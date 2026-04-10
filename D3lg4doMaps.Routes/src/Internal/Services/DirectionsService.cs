using System.Text.Json;
using D3lg4doMaps.Core.Public.Abstractions;
using D3lg4doMaps.Core.Public.Enums;
using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Core.Public.Models;
using D3lg4doMaps.Routes.Internal.Constants;
using D3lg4doMaps.Routes.Internal.Factories;
using D3lg4doMaps.Routes.Internal.Mapping.Mappers;
using D3lg4doMaps.Routes.Public.Abstractions;
using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.Directions;
using D3lg4doMaps.Routes.Public.Models.Requests;

namespace D3lg4doMaps.Routes.Internal.Services;

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
            using var json = await GetRoutesRawAsync(routeRequest, fields); 
            return DirectionsMapper.ToRouteResult(json); 
        } 
    
    public async Task<JsonDocument> GetRoutesRawAsync(RouteRequest routeRequest, params string[] fields) {
        if (fields.Length == 0) 
            throw new MapsInvalidRequestException("Fields must not be empty (FieldMask is a required parameter)."); 
        
        var headers = new Dictionary<string, string>() { 
            { "X-Goog-FieldMask", string.Join(',', fields) } 
        }; 
        var request = CreateRequest(headers, routeRequest); 
        return await _apiClient.SendAsync<JsonDocument>(request); 
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