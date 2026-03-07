using System.Text.Json;
using D3lg4doMaps.Core.Public.Abstractions;

namespace D3lg4doMaps.Core.Internal.Json;

internal sealed class MapsJsonSerializer : IMapsJsonSerializer {
    public string Serialize(object value) 
        => JsonSerializer.Serialize(value, MapsJsonOptions.Default);

    public T? Deserialize<T>(string json) 
        => JsonSerializer.Deserialize<T>(json, MapsJsonOptions.Default);
}