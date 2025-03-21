using Domain.Interfaces.Monitoring;
using MediatR;
using Shared.Common.RequestResult;

namespace Application.Modules.Monitoring.Queries
{
    public record GetNumberActiveQuery() : IRequest<RequestResult>;
    internal sealed class GetNumberActiveQueryHandle : IRequestHandler<GetNumberActiveQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetNumberActiveQueryHandle(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetNumberActiveQuery query, CancellationToken cancellationToken)
        {
            var result = await _IMonitoringRepository.GetNumberActive(null);
            if (result == null)
                return RequestResult.SuccessResultNoRecords();
            return RequestResult.SuccessRecord(result);
        }
    }
}

