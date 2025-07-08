using ESignerDemoWasmApp.Client.Dto;

namespace ESignerDemoWasmApp.Client.Services;

public class ESignerService(Settings settings)
{
    private const string BaseUrl = "https://localhost:7057/api/v1";

    private readonly HttpClient httpClient = new();

    private ApiTokenResponse tokenResponse = null;

    public bool IsLoggedIn => !string.IsNullOrEmpty(this.tokenResponse?.AccessToken);

    public async Task<bool> Login()
    {
        var req = new OAuthTokenRequest
        {
            ClientId = settings.ClientId,
            ClientSecret = settings.ClientSecret,
            GrantType = "client_credentials",
        };

        var parameters = new List<KeyValuePair<string, string>>
        {
            new("grant_type", "client_credentials"),
            new ("client_id", req.ClientId),
            new ("client_secret", req.ClientSecret)
        };

        this.tokenResponse = await this.httpClient.PostFormAsync<ApiTokenResponse>($"{BaseUrl}/oauth20/token", parameters);

        return this.IsLoggedIn;
    }

    public async Task Init()
    {

    }
}