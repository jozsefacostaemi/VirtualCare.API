using Domain.Interfaces.Monitoring;
using MediatR;
using Shared;

namespace Application.Modules.Monitoring.Queries
{
    public record GetNumberAttentionsByCityQuery() : IRequest<RequestResult>;
    internal sealed class GetNumberAttentionsByCityQueryHandle : IRequestHandler<GetNumberAttentionsByCityQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetNumberAttentionsByCityQueryHandle(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetNumberAttentionsByCityQuery query, CancellationToken cancellationToken)
        => await _IMonitoringRepository.GetNumberAttentionsByCity(null);
    }
}

