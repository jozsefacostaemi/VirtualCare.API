using Domain.Entities;
using Domain.Interfaces.MedicalRecords;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.Users
{
    internal class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<MedicalRecord>> GetAllMedicalRecords()
        {
            return await _context.MedicalRecords.ToListAsync();
        }
    }
}
