using System.Security.Cryptography;
using System.Text;
using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Configuration;
using DelgadoMaps.Core.Exceptions;
using DelgadoMaps.Core.Models;
using Microsoft.Extensions.Options;

namespace DelgadoMaps.Core.Internal.Caching.KeyStrategy;

internal sealed partial class RequestFingerprintCacheKeyStrategy : ICacheKeyStrategy {
    // -------------------- INIT --------------------
    private readonly IMapsJsonSerializer _serializer;
    private readonly MapsCachingOptions _opts;
    private static readonly HashSet<string> _allowedHeaders = 
        new(StringComparer.OrdinalIgnoreCase) {"Accept-Language", "Content-Type", "X-Goog-FieldMask"};
    private static readonly HashSet<string> _excludedQueryParameters = 
        new(StringComparer.OrdinalIgnoreCase) {"key"};

    public RequestFingerprintCacheKeyStrategy(
        IMapsJsonSerializer serializer,
        IOptions<MapsCachingOptions> opts)
    {
        _serializer = serializer;
        _opts = opts.Value;
    } 

    // -------------------- METHS --------------------
    public string GenerateCacheKey(MapsApiRequest request) {
        try {
            var normalizedRequest = new {
                BaseUrl = request.BaseUrl.Trim().ToLowerInvariant(),
                Endpoint = request.Endpoint.Trim().ToLowerInvariant(),
                Method = request.Method.Method.ToUpperInvariant(),
                Headers = NormalizeDictionary(
                    request.Headers, x => _allowedHeaders.Contains(x.Key)
                ),
                Query = NormalizeDictionary(
                    request.Query, x => !_excludedQueryParameters.Contains(x.Key)
                ),
                Payload = NormalizePayload(request.Payload)
            };
            
            return $"{_opts.Prefix}:{ComputeSha256(_serializer.Serialize(normalizedRequest))}";
        }
        catch (MapsCacheException) { throw; }
        catch (Exception ex) {
            throw new MapsCacheException($"Unexpected error during cache key generation. {ex.Message}", inner: ex);
        }
    }

    // -------------------- INNER METHS --------------------
    private static string ComputeSha256(string input) {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(hash);
    }
}