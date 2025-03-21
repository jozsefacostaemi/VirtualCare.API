using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Common.RequestResult;

namespace Domain.Interfaces.BuildObjects
{
    public interface IBuildObjectsRepository
    {
        Task<bool> CreatePatients(int number);
        Task<bool> CreateUsers(int number);
    }
}
