using ESignerDemo.Common.Dto;
using System.Net.Http.Json;

namespace ESignerDemo.Common;

public class ClientApiService(Settings settings)
{
    private readonly HttpClient httpClient = new();

    public bool IsLoggedIn { get; set; }

    public async Task<bool> Login()
    {
        var resp = await this.httpClient.PostAsJsonAsync($"{settings.BackendBaseUrl}/esigner/login", new object());

        this.IsLoggedIn = resp.IsSuccessStatusCode;

        return this.IsLoggedIn;
    }

    public async Task<SanadInitResponse?> SanadInit(string nationalId, string? signingPage = null)
    {
        var resp = await this.httpClient.PostAsJsonAsync($"{settings.BackendBaseUrl}/esigner/sanad/init", new { NationalId = nationalId, RedirectUrl = settings.RedirectUrl, SigningPage =signingPage });
        if (resp.IsSuccessStatusCode)
        {
            return await resp.Content.ReadFromJsonAsync<SanadInitResponse>();
        }

        return null;
    }

    public async Task<EnvelopResponse> AdvancedSign(string sessionId, string base64)
    {
        var resp = await this.httpClient.PostAsJsonAsync($"{settings.BackendBaseUrl}/esigner/sign/advanced", new EnvelopRequest { SessionId = sessionId, Data = base64 });
        if (resp.IsSuccessStatusCode)
        {
            return await resp.Content.ReadFromJsonAsync<EnvelopResponse>();
        }

        return null;
    }
}