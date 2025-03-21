using Shared.Common.RequestResult;

namespace Domain.Interfaces.Users;

public interface IUserRepository
{
    Task<RequestResult> SearchFirstUserAvailable(string ProcessCode);
}