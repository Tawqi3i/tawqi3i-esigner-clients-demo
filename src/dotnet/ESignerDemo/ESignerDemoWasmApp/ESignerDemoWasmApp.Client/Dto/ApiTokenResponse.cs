using System.Text.Json.Serialization;

namespace ESignerDemoWasmApp.Client.Dto;

public class ApiTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("refresh_token")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string RefreshToken { get; set; }

    [JsonPropertyName("token_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string TokenType { get; set; } = "Bearer";
}