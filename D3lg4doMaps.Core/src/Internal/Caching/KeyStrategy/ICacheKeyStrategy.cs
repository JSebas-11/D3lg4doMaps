using DelgadoMaps.Core.Models;

namespace DelgadoMaps.Core.Internal.Caching.KeyStrategy;

internal interface ICacheKeyStrategy {
    string GenerateCacheKey(MapsApiRequest request);
}