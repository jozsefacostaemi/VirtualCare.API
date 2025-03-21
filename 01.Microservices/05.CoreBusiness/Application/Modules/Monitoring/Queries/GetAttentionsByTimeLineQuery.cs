using Domain.Interfaces.Monitoring;
using MediatR;
using Shared.Common.RequestResult;

namespace Application.Modules.Monitoring.Queries
{
    public record GetAttentionsByTimeLineQuery() : IRequest<RequestResult>;
    internal sealed class GetAttentionsByTimeLineQueryHandler : IRequestHandler<GetAttentionsByTimeLineQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetAttentionsByTimeLineQueryHandler(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetAttentionsByTimeLineQuery query, CancellationToken cancellationToken)
        {
            var result = await _IMonitoringRepository.GetAttentionsByTimeLine(null);
            if (result == null)
                return RequestResult.SuccessResultNoRecords();
            return RequestResult.SuccessRecord(result);

        }
    }
}

