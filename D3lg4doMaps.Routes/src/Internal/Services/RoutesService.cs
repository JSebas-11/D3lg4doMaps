using D3lg4doMaps.Routes.Public.Abstractions;

namespace D3lg4doMaps.Routes.Internal.Services;

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