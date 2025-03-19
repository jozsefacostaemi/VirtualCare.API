using Domain.Interfaces.Monitoring;
using MediatR;
using Shared;

namespace Application.Modules.Monitoring.Queries
{
    public record GetPercentAttentionsFinishQuery() : IRequest<RequestResult>;
    internal sealed class GetPercentAttentionsFinishQueryHanlde : IRequestHandler<GetPercentAttentionsFinishQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetPercentAttentionsFinishQueryHanlde(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetPercentAttentionsFinishQuery query, CancellationToken cancellationToken)
        => await _IMonitoringRepository.GetPercentAttentionsFinish(null);
    }
}

