using Domain.Interfaces.Monitoring;
using MediatR;
using Shared.Common.RequestResult;

namespace Application.Modules.Monitoring.Queries
{
    public record GetLogguedByHealthCareStaffQuery() : IRequest<RequestResult>;
    internal sealed class GetLogguedByHealthCareStaffQueryHandle : IRequestHandler<GetLogguedByHealthCareStaffQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;
        public GetLogguedByHealthCareStaffQueryHandle(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));
        public async Task<RequestResult> Handle(GetLogguedByHealthCareStaffQuery query, CancellationToken cancellationToken)
        {
            var result = await _IMonitoringRepository.GetLogguedHealthCareStaff(null);
            if (result == null)
                return RequestResult.SuccessResultNoRecords();
            return RequestResult.SuccessRecord(result);
        }
    }
}

