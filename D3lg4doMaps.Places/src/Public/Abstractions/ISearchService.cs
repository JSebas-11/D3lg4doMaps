using D3lg4doMaps.Places.Public.Models;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Public.Abstractions;

public interface ISearchService {
    Task<IReadOnlyList<PlaceSearchResult>> SearchByTextAsync(string textQuery);
    Task<IReadOnlyList<PlaceSearchResult>> SearchByNearbyAsync(NearbyRequest nearbyRequest);
}