using Shared._01.Auth.DTOs;
using Shared.Common.RequestResult;

namespace Auth.VirtualCare.Domain.Interfaces.AuthomatedAuth;
public interface IAuthomatedAuthRepository
{
    Task<List<LoginResultDTO>> AuthomatedLogin(int? number);
    Task<List<LogoutResultDTO>> AuthomatedLogOut(int? number);

}
