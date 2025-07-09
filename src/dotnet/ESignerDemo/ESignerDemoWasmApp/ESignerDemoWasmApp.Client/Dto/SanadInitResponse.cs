namespace ESignerDemoWasmApp.Client.Dto;

public class SanadInitResponse
{
    public string SessionId { get; set; }

    public string AuthUrl { get; set; }

    public string SignVerifyUrl { get; set; }
}