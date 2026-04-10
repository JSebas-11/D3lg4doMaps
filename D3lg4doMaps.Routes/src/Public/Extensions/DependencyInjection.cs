using D3lg4doMaps.Core.Public.Configuration;
using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Routes.Internal.Services;
using D3lg4doMaps.Routes.Public.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace D3lg4doMaps.Routes.Public.Extensions;

public static class DependencyInjection {
    public static IServiceCollection AddD3lg4doMapsRoutes(this IServiceCollection services) {
        // CORE VERIFICATION
        if (!services.Any(s => s.ServiceType == typeof(MapsConfiguration)))
            throw new MapsApiException(
            "D3lg4doMaps.Core services are not registered. Call AddD3lg4doMaps() before AddD3lg4doMapsRoutes()."
        );

        // SERVICES
        services.AddTransient<IDirectionsService, DirectionsService>();
        services.AddTransient<IDistanceMatrixService, DistanceMatrixService>();
        services.AddTransient<IRoutesService, RoutesService>();

        return services;
    }
}