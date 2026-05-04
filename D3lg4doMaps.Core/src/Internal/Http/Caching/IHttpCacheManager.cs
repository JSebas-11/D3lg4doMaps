using DelgadoMaps.Core.Models;

namespace DelgadoMaps.Core.Internal.Http.Caching;

internal interface IHttpCacheManager {
    Task<HttpResponseMessage?> GetCachedResponseAsync(MapsApiRequest request);
    Task CacheResponseAsync(MapsApiRequest request, HttpResponseMessage response);
}