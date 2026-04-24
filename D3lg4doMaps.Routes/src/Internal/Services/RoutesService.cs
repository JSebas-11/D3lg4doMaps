using DelgadoMaps.Routes.Abstractions;

namespace DelgadoMaps.Routes.Internal.Services;

internal class RoutesService : IRoutesService {
    // -------------------- INIT --------------------
    public IDirectionsService Directions { get; }
    public IDistanceMatrixService DistanceMatrix { get; }

    public RoutesService(
        IDirectionsService directions, 
        IDistanceMatrixService distanceMatrix) 
    {
        Directions = directions;
        DistanceMatrix = distanceMatrix;
    }
}