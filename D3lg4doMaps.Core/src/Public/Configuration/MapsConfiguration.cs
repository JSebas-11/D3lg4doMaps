namespace D3lg4doMaps.Core.Public.Configuration;

public sealed class MapsConfiguration {
    public string ApiKey { get; set; } = default!;
    public string Language { get; set; } = "en";
    public string Region { get; set; } = "US";
    public int TimeOutSeconds { get; set; } = 30;

    public MapsLoggingOptions Logging { get; set; } = new();
}