using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<BusinessLine> BusinessLines { get; set; }
        public DbSet<MedicalRecordHistory> MedicalRecordHistories { get; set; }
        public DbSet<PatientState> PatientStates { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserState> UserStates { get; set; }
        public DbSet<ProcessMessage> ProcessMessages { get; set; }
        public DbSet<ProcessMessageErrorLog> ProcessMessageErrorLogs { get; set; }
        public DbSet<MedicalRecordState> MedicalRecordStates { get; set; }
        public DbSet<GeneratedQueue> GeneratedQueues { get; set; }


        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
