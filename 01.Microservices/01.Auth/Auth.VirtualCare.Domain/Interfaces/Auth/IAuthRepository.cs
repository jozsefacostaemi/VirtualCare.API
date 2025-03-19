using Shared;

namespace Auth.VirtualCare.Domain.Interfaces.Auth;
public interface IAuthRepository
{
    Task<RequestResult> Login(string username, string? password);
    Task<RequestResult> LogOut(Guid UserId);

}
