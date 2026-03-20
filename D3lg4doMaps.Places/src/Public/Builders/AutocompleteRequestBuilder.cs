using D3lg4doMaps.Core.Public.Exceptions;
using D3lg4doMaps.Places.Internal.Factories;
using D3lg4doMaps.Places.Public.Models.Geometry;
using D3lg4doMaps.Places.Public.Models.Requests;

namespace D3lg4doMaps.Places.Public.Builders;

/// <summary>
/// Provides a fluent API for constructing <see cref="AutocompleteRequest"/> instances.
/// </summary>
/// <remarks>
/// This builder simplifies the creation of autocomplete requests by enforcing
/// required parameters and providing optional configuration such as location bias.
/// </remarks>
public sealed class AutocompleteRequestBuilder {
    // -------------------- INIT --------------------
    private string? _input;
    private LocationBias? _location;

    public AutocompleteRequestBuilder() {}

    # region BUILD
    /// <summary>
    /// Builds a new <see cref="AutocompleteRequest"/> instance.
    /// </summary>
    /// <returns>
    /// A fully configured <see cref="AutocompleteRequest"/>.
    /// </returns>
    /// <exception cref="MapsInvalidRequestException">
    /// Thrown when the input is not specified.
    /// </exception>
    public AutocompleteRequest Build() {
        if (string.IsNullOrWhiteSpace(_input))
            throw new MapsInvalidRequestException("Input must be include into request.");
        
        return new AutocompleteRequest() {
            Input = _input, 
            LocationBias = _location
        };
    }
    #endregion

    #region CONFIG
    /// <summary>
    /// Sets the input text used to generate autocomplete suggestions.
    /// </summary>
    /// <param name="input">
    /// The partial user input (e.g., "coffee", "pizza near").
    /// </param>
    /// <returns>
    /// The current <see cref="AutocompleteRequestBuilder"/> instance.
    /// </returns>
    /// <exception cref="MapsInvalidRequestException">
    /// Thrown when the input is null, empty, or whitespace.
    /// </exception>
    public AutocompleteRequestBuilder WithInput(string input) {
        if (string.IsNullOrWhiteSpace(input)) 
            throw new MapsInvalidRequestException("Empty input is not allowed.");

        _input = input.Trim();
        return this;
    }

    /// <summary>
    /// Applies a geographic bias to the autocomplete results.
    /// </summary>
    /// <param name="radius">
    /// The radius in meters used to bias results.
    /// </param>
    /// <param name="latitude">
    /// The latitude of the center point.
    /// </param>
    /// <param name="longitude">
    /// The longitude of the center point.
    /// </param>
    /// <returns>
    /// The current <see cref="AutocompleteRequestBuilder"/> instance.
    /// </returns>
    /// <remarks>
    /// This does not strictly restrict results but influences ranking
    /// toward the specified area.
    /// </remarks>
    public AutocompleteRequestBuilder WithLocationBias(
        double radius, double latitude, double longitude
    ) {
        _location = new LocationBias() { 
            Circle = GeoFactory.CreateCircle(radius, longitude, latitude) 
        };

        return this;
    }
    #endregion
}