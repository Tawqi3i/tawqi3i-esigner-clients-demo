using ESignerDemoWasmApp.Client.Dto;
using System.Net.Http.Json;

namespace ESignerDemoWasmApp.Client.Services;

public class ApiService
{
    private const string BackendBaseUrl = "https://localhost:7104/api";

    private const string FrontEndUrl = "http://localhost:5016";

    private readonly HttpClient httpClient = new();

    public bool IsLoggedIn { get; set; }

    public async Task<bool> Login()
    {
        var resp = await this.httpClient.PostAsJsonAsync($"{BackendBaseUrl}/esigner/login", new object());

        this.IsLoggedIn = resp.IsSuccessStatusCode;

        return this.IsLoggedIn;
    }

    public async Task<SanadInitResponse?> SanadInit(string nationalId)
    {
        var resp = await this.httpClient.PostAsJsonAsync($"{BackendBaseUrl}/esigner/sanad/init", new {NationalId = nationalId, RedirectUri = FrontEndUrl });

        if (resp.IsSuccessStatusCode)
        {
            return await resp.Content.ReadFromJsonAsync<SanadInitResponse>();
        }

        return null;
    }
}