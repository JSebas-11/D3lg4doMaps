using D3lg4doMaps.Core.Internal.Abstractions;
using D3lg4doMaps.Core.Public.Exceptions;

internal class MapsUriBuilder : IMapsUriBuilder {
    // -------------------- INIT --------------------
    private string? _path;
    private readonly Dictionary<string, string> _query = [];

    // -------------------- METHS --------------------
    public Uri Build() {
        if (string.IsNullOrWhiteSpace(_path))
            throw new MapsInvalidRequestException("URL path was not set into the request.");

        var builder = new UriBuilder(_path);
        string queryStr = string.Join("&",
            _query.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}")
        );
        builder.Query = queryStr;

        return new Uri(builder.ToString(), UriKind.Absolute);
    }

    public IMapsUriBuilder WithPath(string baseUrl, string endpoint) {
        _path = $"{ClearSlash(baseUrl, false)}/{ClearSlash(endpoint)}";
        return this;
    }

    public IMapsUriBuilder AddQuery(IDictionary<string, string> queryParams) {
        foreach (var param in queryParams)
            _query[param.Key] = param.Value;

        return this;
    }

    // -------------------- INNER METHS --------------------
    private static string ClearSlash(string path, bool start = true)
        => start ? path.TrimStart('/') : path.TrimEnd('/') ;
}