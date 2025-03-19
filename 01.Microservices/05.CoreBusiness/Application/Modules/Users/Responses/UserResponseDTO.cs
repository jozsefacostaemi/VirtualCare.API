namespace Application.Modules.Users.Responses;
public class UserResponseDTO
{
    public Guid? ActualStateId { get; set; }
    public string? ActualStateDesc { get; set; }
    public string? ActualStateCode { get; set; }
    public Guid? UserId { get; set; }
    public string? UserName { get; set; }
}