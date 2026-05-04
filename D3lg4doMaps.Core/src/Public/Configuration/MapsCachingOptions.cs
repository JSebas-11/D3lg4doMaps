namespace DelgadoMaps.Core.Configuration;

public sealed class MapsCachingOptions {
    public string Prefix { get; set; } = "d3lg4doMaps";
    public TimeSpan AbsoluteExpiration { get; set; } = TimeSpan.FromMinutes(30);
    public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromMinutes(30);
}