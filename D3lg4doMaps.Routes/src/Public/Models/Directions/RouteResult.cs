namespace D3lg4doMaps.Routes.Public.Models.Directions;

public sealed class RouteResult {
    public IReadOnlyList<ComputeRoute> Routes { get; internal set; } = [];
    public ComputeRoute? BestRoute { get; internal set; }
}