using Domain.Interfaces.Monitoring;
using MediatR;
using Shared.Common.RequestResult;

namespace Application.Modules.Monitoring.Queries
{
    public record GetPercentAttentionsFinishQuery() : IRequest<RequestResult>;
    internal sealed class GetPercentAttentionsFinishQueryHanlde : IRequestHandler<GetPercentAttentionsFinishQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetPercentAttentionsFinishQueryHanlde(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetPercentAttentionsFinishQuery query, CancellationToken cancellationToken)
        {
            var result = await _IMonitoringRepository.GetPercentAttentionsFinish(null);
            if (result == null)
                return RequestResult.SuccessResultNoRecords();
            return RequestResult.SuccessRecord(result);
        }
    }
}

