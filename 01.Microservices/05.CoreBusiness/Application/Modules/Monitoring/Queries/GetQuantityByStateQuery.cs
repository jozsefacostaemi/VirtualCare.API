using Domain.Interfaces.Monitoring;
using MediatR;
using Shared.Common.RequestResult;

namespace Application.Modules.Monitoring.Queries
{
    public record GetQuantityByStateQuery() : IRequest<RequestResult>;
    internal sealed class GetQuantityByStateQueryHandle : IRequestHandler<GetQuantityByStateQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetQuantityByStateQueryHandle(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetQuantityByStateQuery query, CancellationToken cancellationToken)
        {
            var result = await _IMonitoringRepository.GetQuantityByState(null);
            if (result == null)
                return RequestResult.SuccessResultNoRecords();
            return RequestResult.SuccessRecord(result);
        }
    }
}

