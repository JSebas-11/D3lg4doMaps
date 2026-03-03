namespace D3lg4doMaps.Core.Internal.Abstractions;

internal interface IMapsUriBuilder {
    IMapsUriBuilder WithPath(string baseUrl, string endpoint);
    IMapsUriBuilder AddQuery(IDictionary<string, string> query);
    
    Uri Build();
}