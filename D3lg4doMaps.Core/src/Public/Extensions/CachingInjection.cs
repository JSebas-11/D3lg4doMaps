using DelgadoMaps.Core.Configuration;
using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Core.Internal;
using DelgadoMaps.Core.Internal.Caching;
using DelgadoMaps.Core.Internal.Caching.KeyStrategy;
using DelgadoMaps.Core.Internal.Caching.Store;
using DelgadoMaps.Core.Internal.Http.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace DelgadoMaps.Core.Extensions;

public static partial class DependencyInjection {
    public static IServiceCollection AddDelgadoMapsMemoryCache(this IServiceCollection services,
        Action<MapsCachingOptions> cachingOpts) 
    {
        // VERIFICATION
        VerifyCoreAndCacheOverlap(services);
        
        // CACHING INJECTION
        services.AddMemoryCache();
        services.AddSingleton<ICacheStore, MemoryCacheStore>();
        AddCacheServices(services, cachingOpts);

        return services;
    }

    public static IServiceCollection AddDelgadoMapsDistributedCache(this IServiceCollection services,
        Action<MapsCachingOptions> cachingOpts) 
    {   
        // VERIFICATION
        VerifyCoreAndCacheOverlap(services);

        if (!services.Any(s => s.ServiceType == typeof(IDistributedCache)))
            throw new MapsCacheException(
                "IDistributedCache services are not registered. Call it before adding distributed caching layer."
            );
        
        // CACHING INJECTION
        services.AddSingleton<ICacheStore, DistributedCacheStore>();
        AddCacheServices(services, cachingOpts);

        return services;
    }

    private static void AddCacheServices(
        IServiceCollection services, Action<MapsCachingOptions> cachingOpts) 
    {
        // CONFIGURATION
        services.Configure(cachingOpts);

        services.AddOptions<MapsCachingOptions>()
            .PostConfigure(options => {
                if (string.IsNullOrWhiteSpace(options.Prefix))
                    throw new MapsCacheException("Caching Prefix must be provided in MapsCachingOptions.");
                }
            );

        services.AddSingleton<MapsCachingMarker>();
        // SERVICES
        services.AddSingleton<ICacheKeyStrategy, RequestFingerprintCacheKeyStrategy>();
        services.AddSingleton<IHttpCacheManager, HttpCacheManager>();
    }
    
    private static void VerifyCoreAndCacheOverlap(IServiceCollection services) {
        if (!services.Any(s => s.ServiceType == typeof(MapsCoreMarker)))
            throw new MapsApiException(
            "D3lg4doMaps.Core services are not registered. Call AddDelgadoMaps() before adding cache-memory layer."
        );
        
        if (services.Any(s => s.ServiceType == typeof(MapsCachingMarker)))
            throw new MapsCacheException(
                "Cache layer was already provided. You can only use either AddMemoryCache() or AddDistributedCache()."
            );
    }
}