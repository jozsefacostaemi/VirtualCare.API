using SharedClasses._02.Core.Responses;

namespace Domain.Interfaces.AuthomatedProcesses
{
    public interface IAuthomatedProcessesRepository
    {
        Task<ResultAuthomaticProcessAttentionDTO> ProcessAttentions(int option, int number);
    }
}
