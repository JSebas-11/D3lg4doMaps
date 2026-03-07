using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Places.Internal.Factories;
using D3lg4doMaps.Places.Public.Models.Geometry;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Public.Builders;

public sealed class NearbyRequestBuilder {
    // -------------------- INIT --------------------
    private List<string>? _types;
    private int _maxResults = 1;
    private LocationRestriction? _location;

    public NearbyRequestBuilder() {}

    // -------------------- BUILD --------------------
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

    // -------------------- CONFIG --------------------
    public NearbyRequestBuilder WithTypes(IEnumerable<string> includedTypes) {
        var types = includedTypes?.ToList()
            ?? throw new MapsInvalidRequestException("IncludedTypes cannot be null.");

        if (types.Count == 0)
            throw new MapsInvalidRequestException("IncludedTypes cannot be empty.");

        _types = types;
        return this;
    }
    
    public NearbyRequestBuilder WithMaxResults(int maxResults) {
        _maxResults = maxResults > 0 ? maxResults
            : throw new MapsInvalidRequestException("Maximum Results must be greater than 0.");
        return this;
    }
    
    public NearbyRequestBuilder WithLocationRestriction(
        double radius, double latitude, double longitude
    ) {
        _location = new LocationRestriction() {
            Circle = GeoFactory.CreateCircle(radius, longitude, latitude)
        };

        return this;
    }
}