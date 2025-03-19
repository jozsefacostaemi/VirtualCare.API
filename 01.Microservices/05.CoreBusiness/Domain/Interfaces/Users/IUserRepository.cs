using Shared;

namespace Domain.Interfaces.Users;

public interface IUserRepository
{
    Task<RequestResult> UpdateStateForUser(Guid UserId, string UserCode);
    Task<RequestResult> GetStateByUser(Guid UserId);
    Task<RequestResult> SearchFirstUserAvailable(string ProcessCode);

}