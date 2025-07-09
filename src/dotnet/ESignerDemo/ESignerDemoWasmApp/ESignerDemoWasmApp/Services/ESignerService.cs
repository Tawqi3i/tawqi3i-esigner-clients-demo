using ESignerDemoWasmApp.Client;
using ESignerDemoWasmApp.Client.Dto;
using ESignerDemoWasmApp.Model;
using Microsoft.Net.Http.Headers;

namespace ESignerDemoWasmApp.Services;

public class ESignerService(Settings settings)
{
    /// <summary>
    /// ESigner API Address.
    /// </summary>
    private const string ESignerBaseUrl = "https://localhost:7057/api/v1";

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

        this.tokenResponse = await this.httpClient.PostFormAsync<ApiTokenResponse>($"{ESignerBaseUrl}/oauth20/token", parameters);

        if (this.IsLoggedIn)
        {
            this.httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            this.httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + this.tokenResponse.AccessToken);
        }

        return this.IsLoggedIn;
    }

    public async Task<SanadInitResponse> SanadInit(SanadInitRequest request)
    {
        var resp = await this.httpClient.PostAsJsonAsync($"{ESignerBaseUrl}/sanad/init", request);

        if (!resp.IsSuccessStatusCode)
        {
            return null;
        }

        var json = await resp.Content.ReadAsStringAsync();

        return json.Deserialise<SanadInitResponse>();
    }
}