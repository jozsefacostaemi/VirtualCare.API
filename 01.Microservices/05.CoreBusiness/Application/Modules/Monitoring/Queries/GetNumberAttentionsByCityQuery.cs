using Domain.Interfaces.Monitoring;
using MediatR;
using Shared.Common.RequestResult;

namespace Application.Modules.Monitoring.Queries
{
    public record GetNumberAttentionsByCityQuery() : IRequest<RequestResult>;
    internal sealed class GetNumberAttentionsByCityQueryHandle : IRequestHandler<GetNumberAttentionsByCityQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetNumberAttentionsByCityQueryHandle(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetNumberAttentionsByCityQuery query, CancellationToken cancellationToken)
        {
            var result = await _IMonitoringRepository.GetNumberAttentionsByCity(null);
            if (result == null)
                return RequestResult.SuccessResultNoRecords();
            return RequestResult.SuccessRecord(result);
        }
    }
}

