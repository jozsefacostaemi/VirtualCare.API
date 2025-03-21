using Shared;
using Shared._01.Auth.DTOs;

namespace Auth.VirtualCare.Domain.Interfaces.Auth;
public interface IAuthRepository
{
    Task<LoginResultDTO> Login(string username, string? password);
    Task<LogoutResultDTO> LogOut(Guid UserId);

}
