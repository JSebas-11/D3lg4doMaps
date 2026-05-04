using System.Net;
using DelgadoMaps.Core.Internal.Caching;

namespace DelgadoMaps.Core.Internal.Mappers;

internal static class ResponseMapper {
    internal static async Task<HttpCacheResponse> ToCacheResponseAsync(HttpResponseMessage httpResponse)
        => new HttpCacheResponse(
            (int)httpResponse.StatusCode,
            await httpResponse.Content.ReadAsStringAsync()
        );
    
    internal static HttpResponseMessage ToHttpResponse(HttpCacheResponse cacheResponse)
        => new HttpResponseMessage() {
            StatusCode = (HttpStatusCode)cacheResponse.StatusCode,
            Content = new StringContent(cacheResponse.Body ?? "")
        };
}