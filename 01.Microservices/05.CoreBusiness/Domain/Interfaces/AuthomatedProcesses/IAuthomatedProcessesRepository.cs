using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Domain.Interfaces.AuthomatedProcesses
{
    public interface IAuthomatedProcessesRepository
    {
        Task<RequestResult> ProcessAttentions(int option, int number);
    }
}
