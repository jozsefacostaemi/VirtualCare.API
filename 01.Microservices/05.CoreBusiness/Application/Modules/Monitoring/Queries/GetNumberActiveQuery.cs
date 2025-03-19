using Domain.Interfaces.Monitoring;
using MediatR;
using Shared;

namespace Application.Modules.Monitoring.Queries
{
    public record GetNumberActiveQuery() : IRequest<RequestResult>;
    internal sealed class GetNumberActiveQueryHandle : IRequestHandler<GetNumberActiveQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetNumberActiveQueryHandle(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetNumberActiveQuery query, CancellationToken cancellationToken)
        => await _IMonitoringRepository.GetNumberActive(null);
    }
}

