using D3lg4doMaps.Routes.Public.Enums;
using D3lg4doMaps.Routes.Public.Models.DistanceMatrix;
using D3lg4doMaps.Routes.Public.Models.Requests;

namespace D3lg4doMaps.Routes.Public.Abstractions;

public interface IDistanceMatrixService {
    Task<IReadOnlyList<RouteMatrixElement>> GetDistancesAsync(
        DistanceRequest distanceRequest, RouteDetailLevel detailLevel = RouteDetailLevel.Standard
    );
}