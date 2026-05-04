using System.Text;
using System.Text.Json;
using DelgadoMaps.Core.Exceptions;

namespace DelgadoMaps.Core.Internal.Caching.KeyStrategy;

internal sealed partial class RequestFingerprintCacheKeyStrategy {
    private static readonly JsonWriterOptions _writerOpts = new () {
        Indented = false
    };

    // QUERY - HEADERS NORMALIZATION
    private static IReadOnlyDictionary<string, string>? NormalizeDictionary(
        IDictionary<string, string>? dict, 
        Func<KeyValuePair<string, string>, bool>? filter = null) 
    {   
        if (dict is null || dict.Count == 0) return null;

        IEnumerable<KeyValuePair<string, string>> query = dict;

        if (filter is not null) query = query.Where(filter);

        return query
            .OrderBy(x => x.Key, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                x => x.Key.Trim().ToLowerInvariant(),
                x => x.Value.Trim(), StringComparer.OrdinalIgnoreCase
            );
    }
    
    // PAYLOAD NORMALIZATION
    private string? NormalizePayload(object? payload) {   
        if (payload is null) return null;

        var json = _serializer.Serialize(payload);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, _writerOpts);
        
        WriteCanonicalElement(writer, root);
        writer.Flush();

        return Encoding.UTF8.GetString(stream.ToArray());
    }

    private static void WriteCanonicalElement(Utf8JsonWriter writer, JsonElement element) {
        switch (element.ValueKind) {
            case JsonValueKind.Object:
                writer.WriteStartObject();
                foreach (var item in element.EnumerateObject()
                    .OrderBy(p => p.Name, StringComparer.Ordinal)) 
                {
                    writer.WritePropertyName(item.Name);
                    WriteCanonicalElement(writer, item.Value);
                }
                writer.WriteEndObject();
                break;
            
            case JsonValueKind.Array:
                writer.WriteStartArray();
                foreach (var item in element.EnumerateArray()) {
                    WriteCanonicalElement(writer, item);
                }
                writer.WriteEndArray();
                break;
            
            case JsonValueKind.String:
                writer.WriteStringValue(element.GetString());
                break;

            case JsonValueKind.Number:
                writer.WriteRawValue(element.GetRawText());
                break;
            
            
            case JsonValueKind.False or JsonValueKind.True:
                writer.WriteBooleanValue(element.GetBoolean());
                break;
            
            case JsonValueKind.Null:
                writer.WriteNullValue();
                break;

            default: 
                throw new MapsCacheException("Unsupported JSON token encountered during payload normalization.");
        }
    }
}