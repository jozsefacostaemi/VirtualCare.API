using Auth.VirtualCare.API;
using Microsoft.EntityFrameworkCore;
namespace Auth.VirtualCare.Domain.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
