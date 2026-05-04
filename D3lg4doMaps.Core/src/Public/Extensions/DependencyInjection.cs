using DelgadoMaps.Core.Internal.Abstractions;
using DelgadoMaps.Core.Internal.Factories;
using DelgadoMaps.Core.Internal.Builders;
using DelgadoMaps.Core.Internal.Http;
using DelgadoMaps.Core.Internal.Json;
using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Core.Internal;
using Microsoft.Extensions.Options;

namespace DelgadoMaps.Core.Extensions;

/// <summary>
/// Provides extension methods for registering the D3lg4doMaps SDK services
/// into the dependency injection container.
/// </summary>
public static partial class DependencyInjection {
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
    public static IServiceCollection AddDelgadoMaps(
        this IServiceCollection services, Action<MapsConfiguration> config
    ) {
        // CONFIGURATION
        services.Configure(config);

        services.AddOptions<MapsConfiguration>()
            .PostConfigure(options => {
                if (string.IsNullOrWhiteSpace(options.ApiKey))
                    throw new MapsApiAuthException("ApiKey must be provided in MapsConfiguration.");
                }
            );

        // HTTP
        services.AddHttpClient<IMapsApiClient, MapsApiClient>()
            .ConfigureHttpClient((sp, client) => {
                var opts = sp.GetRequiredService<IOptions<MapsConfiguration>>().Value;
                client.Timeout = opts.RequestTimeout;
        });

        // UTILS
        services.AddSingleton<IMapsJsonSerializer, MapsJsonSerializer>();
        services.AddSingleton<MapsCoreMarker>();

        // BUILDERS
        services.AddTransient<IMapsUriBuilder, MapsUriBuilder>();
        services.AddTransient<IRequestBuilder, RequestBuilder>();

        // FACTORIES
        services.AddTransient<IRequestFactory, RequestFactory>();

        return services;
    }
}