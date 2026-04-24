using System.Text.Json;
using DelgadoMaps.Core.Extensions;

namespace DelgadoMaps.Routes.Internal.Extensions;

internal static class JsonExtensions {
    public static string? GetLocalizedValueText(this JsonElement json, string prop)
        => json.GetObject(prop)?.GetStringValue("text");
}