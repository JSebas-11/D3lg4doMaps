namespace D3lg4doMaps.Core.Public.Exceptions;

public class MapsApiAuthException : MapsApiException {
    private const string DefaultMessage = "The provided API Key is invalid or lacks required permissions.";

    public MapsApiAuthException(
        string? message = null, 
        string? status = null, 
        string? raw = null, 
        Exception? inner = null
    ) : base(message ?? DefaultMessage, status, raw, inner) { }
}