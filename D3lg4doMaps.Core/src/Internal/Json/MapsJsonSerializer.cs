using System.Text.Json;
using DelgadoMaps.Core.Abstractions;
using DelgadoMaps.Core.Models.Http;

namespace DelgadoMaps.Core.Internal.Json;

internal sealed class MapsJsonSerializer : IMapsJsonSerializer {
    public string Serialize(object value) 
        => JsonSerializer.Serialize(value, MapsJsonOptions.Default);

    public T? Deserialize<T>(string json) {
        if (string.IsNullOrWhiteSpace(json)) return default;

        if (typeof(T) == typeof(JsonDocument)) 
            return (T)(object)JsonDocument.Parse(json);

        return JsonSerializer.Deserialize<T>(json, MapsJsonOptions.Default);
    }

    public async IAsyncEnumerable<T> DeserializeStreamAsync<T>(StreamResponse response) {
        await using (response) {
            await foreach (var item in JsonSerializer
                .DeserializeAsyncEnumerable<T>(response.Stream, MapsJsonOptions.Default)
                .ConfigureAwait(false)) {
                if (item is not null)
                    yield return item;
            }
        }
    }
}