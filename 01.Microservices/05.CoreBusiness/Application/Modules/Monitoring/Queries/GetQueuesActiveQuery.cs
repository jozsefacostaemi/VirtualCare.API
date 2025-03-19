using Domain.Interfaces.Monitoring;
using MediatR;
using Shared;

namespace Application.Modules.Monitoring.Queries
{
    public record GetQueuesActiveQuery() : IRequest<RequestResult>;
    internal sealed class GetQueuesActiveQueryHandle : IRequestHandler<GetQueuesActiveQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetQueuesActiveQueryHandle(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetQueuesActiveQuery query, CancellationToken cancellationToken)
        => await _IMonitoringRepository.GetQueuesActive(null);
    }
}

