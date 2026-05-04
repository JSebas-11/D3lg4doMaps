using DelgadoMaps.Core.Configuration;
using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Core.Internal;
using DelgadoMaps.Core.Internal.Caching;
using DelgadoMaps.Core.Internal.Caching.KeyStrategy;
using DelgadoMaps.Core.Internal.Caching.Store;
using DelgadoMaps.Core.Internal.Http.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace DelgadoMaps.Core.Extensions;

public static partial class DependencyInjection {

    /// <summary>
    /// Registers an in-memory HTTP caching layer for DelgadoMaps using <see cref="IMemoryCache"/>.
    ///
    /// Cached responses are shared across all DelgadoMaps modules (Core, Places, Routes).
    ///
    /// This caching layer only applies to standard HTTP request/response endpoints and
    /// does not apply to streaming endpoints such as Compute Route Matrix
    /// (<c>DistanceMatrixService</c>).
    ///
    /// Requires <c>AddDelgadoMaps()</c> to be registered beforehand.
    /// </summary>
    /// <param name="services">
    /// The application service collection.
    /// </param>
    /// <param name="cachingOpts">
    /// Configuration delegate for <see cref="MapsCachingOptions"/>.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/>.
    /// </returns>
    /// <exception cref="MapsApiException">
    /// Thrown when DelgadoMaps core services were not registered.
    /// </exception>
    /// <exception cref="MapsCacheException">
    /// Thrown when another cache layer was already registered.
    /// </exception>
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

    /// <summary>
    /// Registers a distributed HTTP caching layer for DelgadoMaps using
    /// an existing <see cref="IDistributedCache"/> implementation
    /// (Redis, SQL Server, NCache, etc.).
    ///
    /// Cached responses are shared across all DelgadoMaps modules (Core, Places, Routes).
    ///
    /// This caching layer only applies to standard HTTP request/response endpoints and
    /// does not apply to streaming endpoints such as Compute Route Matrix
    /// (<c>DistanceMatrixService</c>).
    ///
    /// Requires:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// <c>AddDelgadoMaps()</c> to be registered beforehand.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// An <see cref="IDistributedCache"/> implementation to already exist
    /// in the service collection.
    /// </description>
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="services">
    /// The application service collection.
    /// </param>
    /// <param name="cachingOpts">
    /// Configuration delegate for <see cref="MapsCachingOptions"/>.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/>.
    /// </returns>
    /// <exception cref="MapsApiException">
    /// Thrown when DelgadoMaps core services were not registered.
    /// </exception>
    /// <exception cref="MapsCacheException">
    /// Thrown when:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// No <see cref="IDistributedCache"/> implementation was registered.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Another cache layer was already registered.
    /// </description>
    /// </item>
    /// </list>
    /// </exception>
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