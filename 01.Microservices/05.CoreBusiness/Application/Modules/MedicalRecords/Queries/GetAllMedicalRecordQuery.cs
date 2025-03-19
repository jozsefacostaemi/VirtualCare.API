using Application.Modules.MedicalRecord.Responses;
using AutoMapper;
using Domain.Interfaces.MedicalRecords;
using MediatR;
using Shared;

namespace Application.Modules.MedicalRecord.Queries
{
    public record GetAllMedicalRecordQuery() : IRequest<RequestResult>;
    internal sealed class GetAllEmployeesQueryHandler : IRequestHandler<GetAllMedicalRecordQuery, RequestResult>
    {
        private readonly IMedicalRecordRepository _IMedicalRecordRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IMedicalRecordRepository IMedicalRecordRepository, IMapper mapper)
        {
            _IMedicalRecordRepository = IMedicalRecordRepository ?? throw new ArgumentNullException(nameof(IMedicalRecordRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<RequestResult> Handle(GetAllMedicalRecordQuery query, CancellationToken cancellationToken)
        {
            var result = await _IMedicalRecordRepository.GetAllMedicalRecords();
            if (result?.Count() == 0) return RequestResult.SuccessResultNoRecords(this.GetType().Name);
            var MedicalRecordResponse = _mapper.Map<List<MedicalRecordResponsesDTO>>(result);
            return RequestResult.SuccessResult(MedicalRecordResponse);
        }
    }
}

