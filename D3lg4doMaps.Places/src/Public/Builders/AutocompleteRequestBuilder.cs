using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Places.Internal.Factories;
using D3lg4doMaps.Places.Public.Models.Geometry;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Public.Builders;

public sealed class AutocompleteRequestBuilder {
    // -------------------- INIT --------------------
    private string? _input;
    private LocationBias? _location;

    public AutocompleteRequestBuilder() {}

    // -------------------- BUILD --------------------
    public AutocompleteRequest Build() {
        if (string.IsNullOrWhiteSpace(_input))
            throw new MapsInvalidRequestException("Input must be include into request.");
        
        return new AutocompleteRequest() {
            Input = _input, 
            LocationBias = _location
        };
    }

    // -------------------- CONFIG --------------------
    public AutocompleteRequestBuilder WithInput(string input) {
        if (string.IsNullOrWhiteSpace(input)) 
            throw new MapsInvalidRequestException("Empty input is not allowed.");

        _input = input.Trim();
        return this;
    }

    public AutocompleteRequestBuilder WithLocationBias(
        double radius, double latitude, double longitude
    ) {
        _location = new LocationBias() { 
            Circle = GeoFactory.CreateCircle(radius, longitude, latitude) 
        };

        return this;
    }
}