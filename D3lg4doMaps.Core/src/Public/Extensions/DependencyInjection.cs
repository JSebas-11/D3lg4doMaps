using D3lg4doMaps.Core.Internal.Abstractions;
using D3lg4doMaps.Core.Internal.Factories;
using D3lg4doMaps.Core.Internal.Http.Builders;
using D3lg4doMaps.Core.Internal.Http;
using D3lg4doMaps.Core.Internal.Json;
using D3lg4doMaps.Core.Public.Abstractions;
using D3lg4doMaps.Core.Public.Configuration;
using Microsoft.Extensions.DependencyInjection;
using D3lg4doMaps.Core.Public.Exceptions;

namespace D3lg4doMaps.Core.Public.Extensions;

/// <summary>
/// Provides extension methods for registering the D3lg4doMaps SDK services
/// into the dependency injection container.
/// </summary>
public static class DependencyInjection {
    /// <summary>
    /// Registers the core services required to use the D3lg4doMaps SDK.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> used to configure application services.
    /// </param>
    /// <param name="config">
    /// The <see cref="MapsConfiguration"/> used to configure authentication,
    /// localization, timeouts, and logging behavior.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> instance.
    /// </returns>
    /// <exception cref="MapsApiAuthException">
    /// Thrown when the <see cref="MapsConfiguration.ApiKey"/> is null or empty.
    /// </exception>
    /// <remarks>
    /// This method registers all required services such as the API client,
    /// serializers, request builders, and factories required to communicate
    /// with the Google Maps API.
    /// </remarks>
    public static IServiceCollection AddD3lg4doMaps(
        this IServiceCollection services, MapsConfiguration config
    ) {
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