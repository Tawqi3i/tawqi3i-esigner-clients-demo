using ESignerDemo.Common.Dto;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Json;

namespace ESignerDemo.Common;

public class ESignerService(Settings settings)
{
    private readonly HttpClient httpClient = new();

    private ApiTokenResponse? tokenResponse = null;

    public bool IsLoggedIn => !string.IsNullOrEmpty(this.tokenResponse?.AccessToken);

    public async Task<bool> Login()
    {
        var parameters = new List<KeyValuePair<string, string>>
        {
            new("grant_type", "client_credentials"),
            new ("client_id", settings.ClientId),
            new ("client_secret",  settings.ClientSecret)
        };

        this.httpClient.DefaultRequestHeaders.Clear();

        this.tokenResponse = await this.httpClient.PostFormAsync<ApiTokenResponse>($"{settings.ESignerBaseUrl}/oauth20/token", parameters);

        if (this.IsLoggedIn)
        {
            this.httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            this.httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + this.tokenResponse.AccessToken);
        }

        return this.IsLoggedIn;
    }

    public async Task<SanadInitResponse> SanadInit(SanadInitRequest request)
    {
        var resp = await this.httpClient.PostAsJsonAsync($"{settings.ESignerBaseUrl}/sanad/init", request);

        if (!resp.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await resp.Content.ReadAsStringAsync();

        return json.Deserialise<SanadInitResponse>();
    }

    public async Task<EnvelopResponse> AdvancedSign(EnvelopRequest request)
    {
        var resp = await this.httpClient.PostAsJsonAsync($"{settings.ESignerBaseUrl}/envelopes/sign", request);

        if (!resp.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await resp.Content.ReadAsStringAsync();

        return json.Deserialise<EnvelopResponse>();
    }
}