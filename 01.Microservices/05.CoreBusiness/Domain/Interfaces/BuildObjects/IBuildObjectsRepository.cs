using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Domain.Interfaces.BuildObjects
{
    public interface IBuildObjectsRepository
    {
        Task<RequestResult> CreatePatients(int number);
        Task<RequestResult> CreateUsers(int number);
    }
}
