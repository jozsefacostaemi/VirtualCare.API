namespace Shared._01.Auth.DTOs;
public class LoginResultDTO
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public DateTime? TokenExpiryDate { get; set; }
    public string? Message { get; set; }
}
