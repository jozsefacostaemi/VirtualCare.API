using Domain.Interfaces.Monitoring;
using MediatR;
using Shared;

namespace Application.Modules.Monitoring.Queries
{
    public record GetAttentionsFinishByHealthCareStaffQuery() : IRequest<RequestResult>;
    internal sealed class GetAttentionsFinishByHealthCareStaffQueryHandle : IRequestHandler<GetAttentionsFinishByHealthCareStaffQuery, RequestResult>
    {
        private readonly IMonitoringRepository _IMonitoringRepository;

        public GetAttentionsFinishByHealthCareStaffQueryHandle(IMonitoringRepository IMonitoringRepository) =>
            _IMonitoringRepository = IMonitoringRepository ?? throw new ArgumentNullException(nameof(IMonitoringRepository));

        public async Task<RequestResult> Handle(GetAttentionsFinishByHealthCareStaffQuery query, CancellationToken cancellationToken)
        => await _IMonitoringRepository.GetAttentionsFinishByHealthCareStaff(null);

    }
}

