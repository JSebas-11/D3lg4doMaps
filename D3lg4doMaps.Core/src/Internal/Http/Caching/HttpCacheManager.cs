using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Core.Internal.Caching.KeyStrategy;
using DelgadoMaps.Core.Internal.Caching.Store;
using DelgadoMaps.Core.Internal.Mappers;
using DelgadoMaps.Core.Models;

namespace DelgadoMaps.Core.Internal.Http.Caching;

internal sealed class HttpCacheManager : IHttpCacheManager {
    // -------------------- INIT --------------------
    private readonly ICacheKeyStrategy _keyStrategy;
    private readonly ICacheStore _cache;

    public HttpCacheManager(ICacheKeyStrategy keyStrategy, ICacheStore cache) {
        _keyStrategy = keyStrategy;
        _cache = cache;
    }

    // -------------------- METHS --------------------
    public async Task CacheResponseAsync(MapsApiRequest request, HttpResponseMessage response) {
        if (!response.IsSuccessStatusCode) return;

        var key = _keyStrategy.GenerateCacheKey(request);
        
        try {
            var mappedResponse = await ResponseMapper.ToCacheResponseAsync(response);

            await _cache.SetAsync(key, mappedResponse);
        }
        catch (MapsCacheException) { throw; }
        catch (Exception ex) {
            throw new MapsCacheException($"There has been an error caching response: {ex.Message}", inner: ex);
        }
    }

    public async Task<HttpResponseMessage?> GetCachedResponseAsync(MapsApiRequest request) {
        var key = _keyStrategy.GenerateCacheKey(request);

        try {
            var cacheResponse = await _cache.GetAsync(key);

            return cacheResponse is null 
                ? null : ResponseMapper.ToHttpResponse(cacheResponse);
        }
        catch (MapsCacheException) { throw; }
        catch (Exception ex) {
            throw new MapsCacheException($"There has been an error retrieving cached response: {ex.Message}", inner: ex);
        }
    }
}