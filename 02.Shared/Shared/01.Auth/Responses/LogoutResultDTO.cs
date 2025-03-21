namespace Shared._01.Auth.DTOs;
public class LogoutResultDTO
{
    public bool Success{ get; set; }
    public string? Message { get; set; }
    public Guid UserId { get; set; }
}
