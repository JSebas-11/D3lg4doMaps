namespace D3lg4doMaps.Core.Public.Exceptions;

public class MapsNotFoundException : MapsApiException {
    private const string DefaultMessage = "The requested resource was not found in Google Maps API.";

    public MapsNotFoundException(
        string? message = null,
        string? status = null,
        string? raw = null,
        Exception? inner = null
    ) : base(message ?? DefaultMessage, status, raw, inner) { }
}