using Domain.Interfaces.Monitoring;
using MediatR;
using Shared.Common.RequestResult;

namespace Application.Modules.Monitoring.Queries
{
    public record GetQueuesActiveQuery() : IRequest<RequestResult>;
    internal sealed class GetQueuesActiveQueryHandle : IRequestHandler<GetQueuesActiveQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetQueuesActiveQueryHandle(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetQueuesActiveQuery query, CancellationToken cancellationToken)
        {
            var result = await _IMonitoringRepository.GetQueuesActive(null);
            if (result == null)
                return RequestResult.SuccessResultNoRecords();
            return RequestResult.SuccessRecord(result);
        }
    }
}

