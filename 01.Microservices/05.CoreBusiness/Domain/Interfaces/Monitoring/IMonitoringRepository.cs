using System;
using Shared.Common.RequestResult;

namespace Domain.Interfaces.Monitoring
{
    public interface IMonitoringRepository
    {
        Task<dynamic> GetUsageCPU();
        Task<dynamic> GetQuantityByState(Guid? BusinessLineId);
        Task<dynamic> GetAttentionsFinishByHealthCareStaff(Guid? BusinessLineId);
        Task<dynamic> GetLogguedHealthCareStaff(Guid? BusinessLineId);
        Task<dynamic?> GetAttentionsByTimeLine(Guid? BusinessLineId);
        Task<dynamic> GetPercentAttentionsFinish(Guid? BusinessLineId);
        Task<dynamic?> GetNumberAttentionsByCity(Guid? BusinessLineId);
        Task<dynamic?> GetQueuesActive(Guid? BusinessLineId);
        Task<dynamic?> GetNumberActive(Guid? BusinessLineId);

    }
}
