using AutoMapper;
using Domain.Entities;
using SharedClasses._02.Core.Responses;

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
