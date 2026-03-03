using D3lg4doMaps.Core.Public.Models;

namespace D3lg4doMaps.Core.Public.Abstractions;

public interface IRequestFactory {
    HttpRequestMessage CreateRequest(MapsApiRequest request);
}