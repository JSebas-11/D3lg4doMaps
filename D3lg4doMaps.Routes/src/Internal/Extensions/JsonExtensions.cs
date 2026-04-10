using System.Text.Json;
using D3lg4doMaps.Core.Public.Extensions;

namespace D3lg4doMaps.Routes.Internal.Extensions;

internal static class JsonExtensions {
    public static string? GetLocalizedValueText(this JsonElement json, string prop)
        => json.GetObject(prop)?.GetStringValue("text");
}