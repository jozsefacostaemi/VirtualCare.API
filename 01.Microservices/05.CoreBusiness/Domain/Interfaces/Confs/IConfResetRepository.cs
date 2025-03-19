using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Domain.Interfaces.Confs
{
    public interface IConfResetRepository
    {
        Task<RequestResult> ResetAttentionsAndPersonStatus();
    }
}
