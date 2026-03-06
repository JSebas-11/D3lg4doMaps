using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Public.Builders;

public sealed class NearbyRequestBuilder {
    // -------------------- INIT --------------------
    private readonly NearbyRequest _request;
    private LocationRestriction? _location;

    public NearbyRequestBuilder() => _request = new();

    // -------------------- BUILD --------------------
    public NearbyRequest Build() {
        if (!_request.IncludedTypes.Any())
            throw new MapsInvalidRequestException("IncludedTypes are not defined.");
        
        if (_location is null)
            throw new MapsInvalidRequestException("LocationRestriction is not defined.");

        _request.LocationRestriction = _location;

        return _request;
    }

    // -------------------- CONFIG --------------------
    public NearbyRequestBuilder WithTypes(IEnumerable<string> includedTypes) {
        _request.IncludedTypes = [.. includedTypes];
        return this;
    }
    
    public NearbyRequestBuilder WithMaxResults(int maxResults) {
        _request.MaxResultCount = maxResults > 0 ? maxResults
            : throw new MapsInvalidRequestException("Maximum Results must be greater than 0.");
        return this;
    }
    
    public NearbyRequestBuilder WithLocationRestriction(
        float radius, double latitude, double longitude
    ) {
        if (radius <= 0) throw new MapsInvalidRequestException("Radius must be greater than 0.");
        if (latitude is < -90 or > 90) throw new MapsInvalidRequestException("Latitude must be between -90 and 90.");
        if (longitude is < -180 or > 180) throw new MapsInvalidRequestException("Latitude must be between -180 and 180.");

        _location = new LocationRestriction() {
            Circle = new () {
                Radius = radius,
                Center = new () {
                    Latitude = latitude, Longitude = longitude
                }
            }
        };

        return this;
    }
}