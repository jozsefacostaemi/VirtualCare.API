using Domain.Interfaces.Monitoring;
using MediatR;
using Shared;

namespace Application.Modules.Monitoring.Queries
{
    public record GetQuantityByStateQuery() : IRequest<RequestResult>;
    internal sealed class GetQuantityByStateQueryHandle : IRequestHandler<GetQuantityByStateQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetQuantityByStateQueryHandle(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetQuantityByStateQuery query, CancellationToken cancellationToken)
        => await _IMonitoringRepository.GetQuantityByState(null);
    }
}

