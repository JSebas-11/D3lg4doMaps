using DelgadoMaps.Core.Internal.Abstractions;
using DelgadoMaps.Core.Exceptions;

namespace DelgadoMaps.Core.Internal.Builders;

internal class MapsUriBuilder : IMapsUriBuilder {
    // -------------------- INIT --------------------
    private string? _path;
    private readonly Dictionary<string, string> _query = [];

    // -------------------- BUILD --------------------
    public Uri Build() {
        if (string.IsNullOrWhiteSpace(_path))
            throw new MapsInvalidRequestException("URL path was not set into the request.");

        var builder = new UriBuilder(_path);
        string queryStr = string.Join("&",
            _query.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}")
        );
        builder.Query = queryStr;

        Reset();
        return new Uri(builder.ToString(), UriKind.Absolute);
    }

    private void Reset() {
        _path = null;
        _query.Clear();    
    }

    // -------------------- CONFIG --------------------
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