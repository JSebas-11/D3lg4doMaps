using D3lg4doMaps.Core.Internal.Abstractions;
using D3lg4doMaps.Core.Internal.Factories;
using D3lg4doMaps.Core.Internal.Http.Builders;
using D3lg4doMaps.Core.Internal.Http;
using D3lg4doMaps.Core.Internal.Json;
using D3lg4doMaps.Core.Public.Abstractions;
using D3lg4doMaps.Core.Public.Configuration;
using Microsoft.Extensions.DependencyInjection;
using D3lg4doMaps.Core.Public.Exceptions;

namespace D3lg4doMaps.Core.Public;

public static class DependencyInjection {
    public static IServiceCollection AddD3lg4doMaps(
        this IServiceCollection services, MapsConfiguration config) 
    {
        // CONFIGURATION
        if (string.IsNullOrWhiteSpace(config.ApiKey))
            throw new MapsApiAuthException("ApiKey must be provided in MapsConfiguration.");
            
        services.AddSingleton(config);

        // HTTP
        services.AddHttpClient<IMapsApiClient, MapsApiClient>(
            client => { client.Timeout = TimeSpan.FromSeconds(config.TimeOutSeconds); }
        );

        // UTILS
        services.AddSingleton<IMapsJsonSerializer, MapsJsonSerializer>();

        // BUILDERS
        services.AddTransient<IMapsUriBuilder, MapsUriBuilder>();
        services.AddTransient<IRequestBuilder, RequestBuilder>();

        // FACTORIES
        services.AddScoped<IRequestFactory, RequestFactory>();
        
        return services;
    }
}