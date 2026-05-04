using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace DelgadoMaps.Core.Internal.Caching.Store;

internal sealed class DistributedCacheStore : ICacheStore {
    // -------------------- INIT --------------------
    private readonly IDistributedCache _cache;
    private readonly IMapsJsonSerializer _serializer;
    private readonly DistributedCacheEntryOptions _entryOpts;

    public DistributedCacheStore(
        IDistributedCache cache, 
        IMapsJsonSerializer serializer,
        IOptions<MapsCachingOptions> options
    ) {
        _cache = cache;
        _serializer = serializer;
        var opts = options.Value;
        _entryOpts = new() {
            AbsoluteExpirationRelativeToNow = opts.AbsoluteExpiration,
            SlidingExpiration = opts.SlidingExpiration
        };
    }

    // -------------------- METHS --------------------
    public async Task<HttpCacheResponse?> GetAsync(string cacheKey) {
        var objStr = await _cache.GetStringAsync(cacheKey, default).ConfigureAwait(false);

        return string.IsNullOrWhiteSpace(objStr) 
            ? null : _serializer.Deserialize<HttpCacheResponse>(objStr);
    }

    public async Task SetAsync(string cacheKey, HttpCacheResponse response) {
        var str = _serializer.Serialize(response);
        await _cache.SetStringAsync(cacheKey, str, _entryOpts, default).ConfigureAwait(false);
    }

    public Task ClearAsync(string cacheKey) 
        => _cache.RemoveAsync(cacheKey, default);
}