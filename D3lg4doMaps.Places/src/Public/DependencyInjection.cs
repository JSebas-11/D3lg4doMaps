using D3lg4doMaps.Places.Internal.Services;
using D3lg4doMaps.Places.Public.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace D3lg4doMaps.Places.Public;

public static class DependencyInjection {
    public static IServiceCollection AddD3lg4doMapsPlaces(this IServiceCollection services) {
        // SERVICES
        services.AddTransient<ISearchService, SearchService>();

        return services;
    }
}