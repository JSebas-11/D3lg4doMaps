namespace D3lg4doMaps.Core.Internal.Abstractions;

internal interface IRequestBuilder {
    IRequestBuilder SetMethod(HttpMethod method);
    IRequestBuilder SetPath(string baseUrl, string endpoint);
    IRequestBuilder AddHeaders(IDictionary<string, string> headers);
    IRequestBuilder AddQuery(IDictionary<string, string> query);
    IRequestBuilder SetJsonPayload(object payload);

    HttpRequestMessage Build();
}