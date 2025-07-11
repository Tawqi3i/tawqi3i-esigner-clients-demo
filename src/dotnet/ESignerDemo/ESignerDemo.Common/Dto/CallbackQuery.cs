namespace ESignerDemo.Common.Dto;

public class CallbackQuery
{
    public string SessionId { get; set; }

    public string NationalId { get; set; }

    public string? PinVerifyUrl { get; set; }

    public string? Error { get; set; }

    public bool? CanSign { get; set; }
}