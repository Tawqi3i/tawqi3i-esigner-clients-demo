namespace ESignerDemo.Common.Dto;

public class SanadInitRequest
{
    public string NationalId { get; set; }

    public string RedirectUrl { get; set; }

    public string? SigningPage { get; set; }
}