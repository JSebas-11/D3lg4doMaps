using System.Net;
using D3lg4doMaps.Core.Public.Exceptions;

namespace D3lg4doMaps.Core.Internal.Handlers;

internal static class ExceptionHandler {
    public static void Handle(HttpStatusCode statusCode) {
        switch (statusCode) {
            case HttpStatusCode.Unauthorized:
            case HttpStatusCode.Forbidden:
                throw new MapsApiAuthException();
            
            case HttpStatusCode.TooManyRequests:
                throw new MapsRateLimitException();

            case HttpStatusCode.NotFound:
                throw new MapsNotFoundException();

            case HttpStatusCode.BadRequest:
                throw new MapsInvalidRequestException();

            default:
                if ((int)statusCode >= 400)
                    throw new MapsApiException($"Unexpected HTTP error: {(int)statusCode} {statusCode}");
                break;
        }
    }
}