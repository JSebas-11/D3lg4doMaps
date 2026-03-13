using Microsoft.Extensions.Logging;

namespace D3lg4doMaps.Core.Public.Configuration;

/// <summary>
/// Defines logging options for the Maps SDK.
/// </summary>
public sealed class MapsLoggingOptions {
    /// <summary>
    /// Indicates whether SDK logging is enabled.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// The minimum logging level used by the SDK.
    /// </summary>
    public LogLevel Level { get; set; } = LogLevel.Information;

    /// <summary>
    /// Optional prefix added to all log messages produced by the SDK.
    /// </summary>
    public string? Prefix { get; set; } = "[D3lg4doMaps]";
}