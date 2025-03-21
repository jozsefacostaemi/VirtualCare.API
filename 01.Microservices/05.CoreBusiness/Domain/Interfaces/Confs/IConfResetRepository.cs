using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Common.RequestResult;

namespace Domain.Interfaces.Confs
{
    public interface IConfResetRepository
    {
        Task<bool> ResetAttentionsAndPersonStatus();
    }
}
