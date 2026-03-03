namespace D3lg4doMaps.Core.Public.Exceptions;

public class MapsRateLimitException : MapsApiException {
    private const string DefaultMessage = "The Google Maps API rate limit has been exceeded.";

    public MapsRateLimitException(
        string? message = null,
        string? status = null,
        string? raw = null,
        Exception? inner = null
    ) : base(message ?? DefaultMessage, status, raw, inner) { }
}