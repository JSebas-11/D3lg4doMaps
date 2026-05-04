namespace DelgadoMaps.Core.Exceptions;

public class MapsCacheException : MapsApiException {
    private const string DefaultMessage = "Error during caching-operation.";

    public MapsCacheException(
        string? message = null,
        string? status = null,
        string? raw = null,
        Exception? inner = null
    ) : base(message ?? DefaultMessage, status, raw, inner) { }
}