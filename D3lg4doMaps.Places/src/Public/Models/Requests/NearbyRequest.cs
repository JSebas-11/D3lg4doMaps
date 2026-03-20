using D3lg4doMaps.Places.Public.Models.Geometry;

namespace D3lg4doMaps.Places.Public.Models.Requests;

/// <summary>
/// Represents a request for searching places near a specific geographic location.
/// </summary>
/// <remarks>
/// This model is constructed using <see cref="Builders.NearbyRequestBuilder"/>
/// to enforce required parameters such as location and included types.
/// </remarks>
public sealed class NearbyRequest {
    /// <summary>
    /// Gets the list of place types to include in the search.
    /// </summary>
    /// <remarks>
    /// Examples include "restaurant", "cafe", or "store".
    /// At least one type must be specified.
    /// </remarks>
    public IReadOnlyList<string> IncludedTypes { get; internal set; } = [];

    /// <summary>
    /// Gets the maximum number of results to return.
    /// </summary>
    /// <remarks>
    /// Defaults to 1 if not explicitly configured.
    /// </remarks>
    public int MaxResultCount { get; internal set; } = 1;
    
    /// <summary>
    /// Gets the geographic restriction applied to the search.
    /// </summary>
    /// <remarks>
    /// This defines the area within which results must be located.
    /// </remarks>
    public LocationRestriction LocationRestriction { get; internal set; } = new();
}