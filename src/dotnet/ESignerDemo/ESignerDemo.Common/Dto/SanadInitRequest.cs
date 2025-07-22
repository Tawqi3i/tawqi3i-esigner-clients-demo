namespace ESignerDemo.Common.Dto;

public class SanadInitRequest
{
    public string NationalId { get; set; }

    public string RedirectUri { get; set; }

    public string? SigningPage { get; set; }
}