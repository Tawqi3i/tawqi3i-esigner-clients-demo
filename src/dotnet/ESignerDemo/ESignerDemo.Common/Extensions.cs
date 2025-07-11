using System.Text.Json;

namespace ESignerDemo.Common;

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

    public static T Deserialise<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultOptions);
    }
}