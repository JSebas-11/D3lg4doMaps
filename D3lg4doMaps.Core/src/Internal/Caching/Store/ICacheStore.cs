namespace DelgadoMaps.Core.Internal.Caching.Store;

internal interface ICacheStore {
    Task<HttpCacheResponse?> GetAsync(string cacheKey);
    Task SetAsync(string cacheKey, HttpCacheResponse response);
    Task ClearAsync(string cacheKey);
}