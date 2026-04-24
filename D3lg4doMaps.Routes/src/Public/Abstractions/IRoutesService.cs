namespace DelgadoMaps.Routes.Abstractions;

/// <summary>
/// Provides a unified entry point for accessing all routing-related services.
/// </summary>
/// <remarks>
/// This interface aggregates the different domain services of the Routes module,
/// allowing consumers to access directions and distance matrix functionality
/// through a single abstraction.
/// </remarks>
public interface IRoutesService {
    /// <summary>
    /// Gets the directions service used to calculate routes between locations.
    /// </summary>
    IDirectionsService Directions { get; }

    /// <summary>
    /// Gets the distance matrix service used to compute distances
    /// between multiple origins and destinations.
    /// </summary>
    IDistanceMatrixService DistanceMatrix { get; }
}