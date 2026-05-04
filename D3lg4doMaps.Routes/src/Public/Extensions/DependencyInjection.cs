using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Routes.Internal.Services;
using DelgadoMaps.Routes.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using DelgadoMaps.Core.Internal;

namespace DelgadoMaps.Routes.Extensions;

/// <summary>
/// Provides extension methods for registering Routes services
/// into the dependency injection container.
/// </summary>
/// <remarks>
/// This method registers all services required to interact with the
/// Google Routes API, including directions (Compute Routes) and distance matrix services (Compute Matrix Route).
///
/// ⚠ <b>Important:</b> Core services must be registered first by calling
/// <c>AddDelgadoMaps()</c>.
/// </remarks>
public static class DependencyInjection {
    /// <summary>
    /// Registers all Routes module services into the dependency injection container.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> used to configure application services.
    /// </param>
    /// <returns>
    /// The same <see cref="IServiceCollection"/> instance for chaining.
    /// </returns>
    /// <exception cref="MapsApiException">
    /// Thrown when the Core services have not been registered.
    /// </exception>
    /// <example>
    /// <code>
    /// services.AddDelgadoMaps(configuration);
    /// services.AddDelgadoMapsRoutes();
    /// </code>
    /// </example>
    /// <remarks>
    /// The following services are registered with <c>Transient</c> lifetime:
    /// <list type="bullet">
    /// <item><description><see cref="IDirectionsService"/></description></item>
    /// <item><description><see cref="IDistanceMatrixService"/></description></item>
    /// <item><description><see cref="IRoutesService"/></description></item>
    /// </list>
    ///
    /// Transient lifetime ensures a new instance is created per request,
    /// keeping services lightweight and stateless.
    /// </remarks>
    public static IServiceCollection AddDelgadoMapsRoutes(this IServiceCollection services) {
        // CORE VERIFICATION
        if (!services.Any(s => s.ServiceType == typeof(MapsCoreMarker)))
            throw new MapsApiException(
            "D3lg4doMaps.Core services are not registered. Call AddDelgadoMaps() before AddDelgadoMapsRoutes()."
        );

        // SERVICES
        services.AddTransient<IDirectionsService, DirectionsService>();
        services.AddTransient<IDistanceMatrixService, DistanceMatrixService>();
        services.AddTransient<IRoutesService, RoutesService>();

        return services;
    }
}