using D3lg4doMaps.Places.Public.Abstractions;

namespace D3lg4doMaps.Places.Internal.Services;

internal class PlacesService : IPlacesService {
    // -------------------- INIT --------------------
    public IAutocompleteService Autocomplete { get; }
    public IDetailsService Details { get; }
    public ISearchService Search { get; }

    public PlacesService(
        IAutocompleteService autocompleteService, 
        IDetailsService detailsService, 
        ISearchService searchService
    ) {
        Autocomplete = autocompleteService;
        Details = detailsService;
        Search = searchService;
    }
}