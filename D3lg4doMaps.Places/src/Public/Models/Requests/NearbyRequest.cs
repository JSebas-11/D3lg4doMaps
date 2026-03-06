namespace D3lg4doMaps.Places.Public.Models.Requests;

// MAIN MODEL
public sealed class NearbyRequest {
    public IReadOnlyList<string> IncludedTypes { get; internal set; } = [];
    public int MaxResultCount { get; internal set; } = 1;
    public LocationRestriction LocationRestriction { get; internal set; } = new();
}

// ASIDE MODELS
public sealed class LocationRestriction {
    public Circle Circle { get; internal set; } = new();
}

public sealed class Circle {
    public Center Center { get; internal set; } = new();
    public float Radius { get; internal set; }
}

public sealed class Center {
    public double Latitude { get; internal set; }
    public double Longitude { get; internal set; }
}