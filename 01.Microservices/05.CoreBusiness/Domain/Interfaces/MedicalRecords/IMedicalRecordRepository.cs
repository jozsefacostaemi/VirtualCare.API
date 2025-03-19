using Domain.Entities;
namespace Domain.Interfaces.MedicalRecords
{
    public interface IMedicalRecordRepository
    {
        Task<List<MedicalRecord>> GetAllMedicalRecords();
    }
}
