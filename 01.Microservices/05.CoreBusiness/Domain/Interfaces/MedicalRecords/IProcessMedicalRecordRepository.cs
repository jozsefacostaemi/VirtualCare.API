using Shared.Common.RequestResult;
using SharedClasses._02.Core.DTOs;

namespace Domain.Interfaces.MedicalRecords;

public interface IProcessMedicalRecordRepository
{
    Task<ResultProcessAttentionDTO> CreateAttention(string processCode, Guid patientId);
    Task<ResultProcessAttentionDTO> AssignAttention(Guid UserId);
    Task<ResultProcessAttentionDTO> StartAttention(Guid MedicalRecordId);
    Task<ResultProcessAttentionDTO> FinishAttention(Guid MedicalRecordId);
    Task<ResultProcessAttentionDTO> CancelAttention(Guid MedicalRecordId);
}
