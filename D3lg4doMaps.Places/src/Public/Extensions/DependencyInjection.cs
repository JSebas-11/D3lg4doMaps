using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Places.Internal.Services;
using DelgadoMaps.Places.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using DelgadoMaps.Core.Internal;

namespace DelgadoMaps.Places.Extensions;

/// <summary>
/// Provides extension methods for registering Places services
/// into the dependency injection container.
/// </summary>
/// <remarks>
/// This method registers all services required to interact with the
/// Google Places API, including search, autocomplete, and details services.
///
/// ⚠ <b>Important:</b> Core services must be registered first by calling
/// <c>AddDelgadoMaps()</c>.
/// </remarks>
public static class DependencyInjection {
    /// <summary>
    /// Registers all Places module services into the dependency injection container.
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
    /// services.AddDelgadoMapsPlaces();
    /// </code>
    /// </example>
    /// <remarks>
    /// The following services are registered with <c>Transient</c> lifetime:
    /// <list type="bullet">
    /// <item><description><see cref="IAutocompleteService"/></description></item>
    /// <item><description><see cref="IDetailsService"/></description></item>
    /// <item><description><see cref="ISearchService"/></description></item>
    /// <item><description><see cref="IPlacesService"/></description></item>
    /// </list>
    ///
    /// Transient lifetime ensures a new instance is created per request,
    /// keeping services lightweight and stateless.
    /// </remarks>
    public static IServiceCollection AddDelgadoMapsPlaces(this IServiceCollection services) {
        // CORE VERIFICATION
        if (!services.Any(s => s.ServiceType == typeof(MapsCoreMarker)))
            throw new MapsApiException(
            "D3lg4doMaps.Core services are not registered. Call AddDelgadoMaps() before AddDelgadoMapsPlaces()."
        );

        // SERVICES
        services.AddTransient<IAutocompleteService, AutocompleteService>();
        services.AddTransient<IDetailsService, DetailsService>();
        services.AddTransient<ISearchService, SearchService>();
        services.AddTransient<IPlacesService, PlacesService>();

        return services;
    }
}