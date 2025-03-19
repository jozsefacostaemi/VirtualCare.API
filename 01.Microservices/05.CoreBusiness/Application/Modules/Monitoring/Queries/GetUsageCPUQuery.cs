using Domain.Interfaces.Monitoring;
using MediatR;
using Shared;

namespace Application.Modules.Monitoring.Queries
{
    public record GetUsageCPUQuery() : IRequest<RequestResult>;
    internal sealed class GetUsageCPUQueryHandle : IRequestHandler<GetUsageCPUQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetUsageCPUQueryHandle(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetUsageCPUQuery query, CancellationToken cancellationToken)
        => await _IMonitoringRepository.GetUsageCPU();
    }
}

