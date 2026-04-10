namespace D3lg4doMaps.Routes.Public.Abstractions;

public interface IRoutesService {
    IDirectionsService Directions { get; }
    IDistanceMatrixService DistanceMatrix { get; }
}