using Shared;

namespace Domain.Interfaces.MedicalRecords;

public interface IProcessMedicalRecordRepository
{
    Task<RequestResult> CreateAttention(string processCode, Guid patientId);
    Task<RequestResult> AssignAttention(Guid UserId);
    Task<RequestResult> StartAttention(Guid MedicalRecordId);
    Task<RequestResult> FinishAttention(Guid MedicalRecordId);
    Task<RequestResult> CancelAttention(Guid MedicalRecordId);
}
