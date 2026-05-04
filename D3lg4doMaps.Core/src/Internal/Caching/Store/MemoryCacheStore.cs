using DelgadoMaps.Core.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace DelgadoMaps.Core.Internal.Caching.Store;

internal sealed class MemoryCacheStore : ICacheStore {
    // -------------------- INIT --------------------
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _entryOpts;

    public MemoryCacheStore(IMemoryCache cache, IOptions<MapsCachingOptions> options) {
        _cache = cache;
        var opts = options.Value;
        _entryOpts = new() {
            AbsoluteExpirationRelativeToNow = opts.AbsoluteExpiration,
            SlidingExpiration = opts.SlidingExpiration
        };
    }

    // -------------------- METHS --------------------
    public Task<HttpCacheResponse?> GetAsync(string cacheKey)
        => Task.FromResult(_cache.Get<HttpCacheResponse>(cacheKey));

    public Task SetAsync(string cacheKey, HttpCacheResponse response) {
        _cache.Set(cacheKey, response, _entryOpts);
        return Task.CompletedTask;
    }

    public Task ClearAsync(string cacheKey) {
        _cache.Remove(cacheKey);
        return Task.CompletedTask;
    }
}