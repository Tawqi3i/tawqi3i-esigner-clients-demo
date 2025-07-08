using System.Text.Json;

namespace ESignerDemoWasmApp.Client;

public static class Extensions
{
    private static readonly JsonSerializerOptions DefaultOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public static async Task<T> PostFormAsync<T>(this HttpClient httpClient, string uri, IList<KeyValuePair<string, string>> parameters)
        where T : class
    {
        using (var content = new FormUrlEncodedContent(parameters))
        {
            var httpResponse = await httpClient.PostAsync(uri, content);
            httpResponse.EnsureSuccessStatusCode();

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(jsonResponse);
        }
    }

    public static IList<KeyValuePair<string, string>> ToFormParameters(this object theObj)
    {
        var list = new List<KeyValuePair<string, string>>();

        var rootElement = JsonElementFromObject(theObj);

        foreach (var jsonProp in rootElement.EnumerateObject())
        {
            if (jsonProp.Value.ValueKind == JsonValueKind.Array || jsonProp.Value.ValueKind == JsonValueKind.Object)
            {
                continue;
            }

            list.Add(new KeyValuePair<string, string>(jsonProp.Name, jsonProp.Value.ToString()));
        }

        return list;
    }

    public static JsonElement JsonElementFromObject<TValue>(TValue value, JsonSerializerOptions options = default) 
        => JsonElementFromObject(value, typeof(TValue), options ?? DefaultOptions);

    public static JsonElement JsonElementFromObject(object value, Type type, JsonSerializerOptions options = default)
    {
        using var doc = JsonDocumentFromObject(value, type, options ?? DefaultOptions);
        return doc.RootElement.Clone();
    }

    public static JsonDocument JsonDocumentFromObject(object value, Type type, JsonSerializerOptions options = default)
    {
        var bytes = JsonSerializer.SerializeToUtf8Bytes(value, options ?? DefaultOptions);
        return JsonDocument.Parse(bytes);
    }
}