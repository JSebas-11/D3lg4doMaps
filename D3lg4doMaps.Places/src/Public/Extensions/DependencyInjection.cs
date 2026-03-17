using D3lg4doMaps.Core.Public.Configuration;
using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Places.Internal.Services;
using D3lg4doMaps.Places.Public.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace D3lg4doMaps.Places.Public.Extensions;

public static class DependencyInjection {
    public static IServiceCollection AddD3lg4doMapsPlaces(this IServiceCollection services) {
        // CORE VERIFICATION
        if (!services.Any(s => s.ServiceType == typeof(MapsConfiguration)))
            throw new MapsApiException(
            "D3lg4doMaps.Core services are not registered. Call AddD3lg4doMaps() before AddD3lg4doMapsPlaces()."
        );

        // SERVICES
        services.AddTransient<IAutocompleteService, AutocompleteService>();
        services.AddTransient<IDetailsService, DetailsService>();
        services.AddTransient<ISearchService, SearchService>();
        services.AddTransient<IPlacesService, PlacesService>();

        return services;
    }
}