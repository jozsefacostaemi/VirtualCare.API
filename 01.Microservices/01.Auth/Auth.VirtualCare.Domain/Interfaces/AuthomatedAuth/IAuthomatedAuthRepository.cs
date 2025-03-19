using Shared;

namespace Auth.VirtualCare.Domain.Interfaces.AuthomatedAuth;
public interface IAuthomatedAuthRepository
{
    Task<RequestResult> AuthomatedLogin(int? number);
    Task<RequestResult> AuthomatedLogOut(int? number);

}
