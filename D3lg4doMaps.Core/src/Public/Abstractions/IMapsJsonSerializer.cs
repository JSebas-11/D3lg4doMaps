namespace D3lg4doMaps.Core.Public.Abstractions;

public interface IMapsJsonSerializer {
    string Serialize(object value);
    T? Deserialize<T>(string json);
}