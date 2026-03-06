namespace D3lg4doMaps.Places.Public.Abstractions;

public interface IPlacesService {
    IAutocompleteService Autocomplete { get; }
    IDetailsService Details { get; }
    ISearchService Search { get; }
}