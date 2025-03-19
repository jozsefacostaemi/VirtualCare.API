using Application.Modules.MedicalRecord.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MedicalRecordMappingProfile : Profile
    {
        public MedicalRecordMappingProfile()
        {
            CreateMap<MedicalRecord, MedicalRecordResponsesDTO>()
                .ConstructUsing(src => new MedicalRecordResponsesDTO(
                    src.Id,
                    src.PatientId
            ));
        }
    }
}
