using Domain.Interfaces.Monitoring;
using MediatR;
using Shared;

namespace Application.Modules.Monitoring.Queries
{
    public record GetAttentionsByTimeLineQuery() : IRequest<RequestResult>;
    internal sealed class GetAttentionsByTimeLineQueryHandler : IRequestHandler<GetAttentionsByTimeLineQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetAttentionsByTimeLineQueryHandler(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetAttentionsByTimeLineQuery query, CancellationToken cancellationToken)
        => await _IMonitoringRepository.GetAttentionsByTimeLine(null);
    }
}

