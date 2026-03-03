namespace D3lg4doMaps.Core.Public.Exceptions;

public class MapsInvalidRequestException : MapsApiException {
    private const string DefaultMessage = "The request sent to Google Maps API contains invalid or missing parameters.";

    public MapsInvalidRequestException(
        string? message = null,
        string? status = null,
        string? raw = null,
        Exception? inner = null
    ) : base(message ?? DefaultMessage, status, raw, inner) { }
}