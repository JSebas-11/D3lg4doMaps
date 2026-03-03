namespace D3lg4doMaps.Core.Public.Exceptions;

public class MapsApiException : Exception {
    private const string DefaultMessage = "An error occurred while processing a request to Google Maps API.";
    public string? Status { get; }
    public string? RawResponse { get; }

    public MapsApiException(
        string? message = null, 
        string? status = null,
        string? rawResponse = null,
        Exception? inner = null
        ) 
        : base(message ?? DefaultMessage, inner)
    {
        Status = status;
        RawResponse = rawResponse;
    }

    public override string ToString() 
        => $"{GetType().Name}: {Message}\n" + 
            (Status is not null ? $"Status: {Status}\n" : "") +
            (RawResponse is not null ? $"Raw: {RawResponse}\n" : "") +
            base.ToString();
}