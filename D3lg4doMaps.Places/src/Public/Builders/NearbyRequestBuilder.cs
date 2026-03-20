using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Places.Internal.Factories;
using D3lg4doMaps.Places.Public.Models.Geometry;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Public.Builders;

/// <summary>
/// Provides a fluent API for constructing <see cref="NearbyRequest"/> instances.
/// </summary>
/// <remarks>
/// This builder is used to configure nearby place searches by specifying
/// location constraints, place types, and result limits.
/// </remarks>
public sealed class NearbyRequestBuilder {
    // -------------------- INIT --------------------
    private List<string>? _types;
    private int _maxResults = 1;
    private LocationRestriction? _location;
    
    public NearbyRequestBuilder() {}

    #region BUILD
    /// <summary>
    /// Builds a new <see cref="NearbyRequest"/> instance.
    /// </summary>
    /// <returns>
    /// A fully configured <see cref="NearbyRequest"/>.
    /// </returns>
    /// <exception cref="MapsInvalidRequestException">
    /// Thrown when required parameters such as types or location are not defined.
    /// </exception>
    public NearbyRequest Build() {
        if (_types is null || _types.Count == 0)
            throw new MapsInvalidRequestException("IncludedTypes are not defined.");
        
        if (_location is null)
            throw new MapsInvalidRequestException("LocationRestriction is not defined.");

        return new NearbyRequest() {
            IncludedTypes = _types.AsReadOnly(),
            MaxResultCount = _maxResults,
            LocationRestriction = _location
        };
    }
    #endregion

    #region CONFIG
    /// <summary>
    /// Specifies the place types to include in the search.
    /// </summary>
    /// <param name="includedTypes">
    /// A collection of place types (e.g., "restaurant", "cafe").
    /// </param>
    /// <returns>
    /// The current <see cref="NearbyRequestBuilder"/> instance.
    /// </returns>
    /// <exception cref="MapsInvalidRequestException">
    /// Thrown when the collection is null or empty.
    /// </exception>
    public NearbyRequestBuilder WithTypes(IEnumerable<string> includedTypes) {
        var types = includedTypes?.ToList()
            ?? throw new MapsInvalidRequestException("IncludedTypes cannot be null.");

        if (types.Count == 0)
            throw new MapsInvalidRequestException("IncludedTypes cannot be empty.");

        _types = types;
        return this;
    }
    
    /// <summary>
    /// Sets the maximum number of results to return.
    /// </summary>
    /// <param name="maxResults">
    /// The maximum number of results. Must be greater than zero.
    /// </param>
    /// <returns>
    /// The current <see cref="NearbyRequestBuilder"/> instance.
    /// </returns>
    /// <exception cref="MapsInvalidRequestException">
    /// Thrown when the value is less than or equal to zero.
    /// </exception>
    public NearbyRequestBuilder WithMaxResults(int maxResults) {
        _maxResults = maxResults > 0 ? maxResults
            : throw new MapsInvalidRequestException("Maximum Results must be greater than 0.");
        return this;
    }
    
    /// <summary>
    /// Restricts the search to a specific geographic area.
    /// </summary>
    /// <param name="radius">
    /// The radius in meters defining the search area.
    /// </param>
    /// <param name="latitude">
    /// The latitude of the center point.
    /// </param>
    /// <param name="longitude">
    /// The longitude of the center point.
    /// </param>
    /// <returns>
    /// The current <see cref="NearbyRequestBuilder"/> instance.
    /// </returns>
    /// <remarks>
    /// Unlike location bias, this restriction strictly limits results
    /// to the defined geographic area.
    /// </remarks>
    public NearbyRequestBuilder WithLocationRestriction(
        double radius, double latitude, double longitude
    ) {
        _location = new LocationRestriction() {
            Circle = GeoFactory.CreateCircle(radius, longitude, latitude)
        };

        return this;
    }
    #endregion
}