using Domain.Interfaces.Monitoring;
using MediatR;
using Shared.Common.RequestResult;

namespace Application.Modules.Monitoring.Queries
{
    public record GetUsageCPUQuery() : IRequest<RequestResult>;
    internal sealed class GetUsageCPUQueryHandle : IRequestHandler<GetUsageCPUQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetUsageCPUQueryHandle(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetUsageCPUQuery query, CancellationToken cancellationToken)
        {
            var result = await _IMonitoringRepository.GetUsageCPU();
            if (result == null)
                return RequestResult.SuccessResultNoRecords();
            return RequestResult.SuccessRecord(result);
        }
    }
}

