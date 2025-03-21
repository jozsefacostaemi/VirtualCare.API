using Domain.Interfaces.Users;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.Common.RequestResult;

namespace Infraestructure.Repositories.Users;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context) => _context = context;
    /* Función que consulta el primer personal asistencial disponible */
    public async Task<RequestResult> SearchFirstUserAvailable(string ProcessCode)
    {
        var getFirstHealCareStaffAvailable = await _context.Users
            .Where(x => x.UserState != null &&
             x.UserServices.Any(x => x.Service.Code.Equals(ProcessCode)) &&
             x.UserState.Code.Equals(UserStateEnum.AVAILABLE.ToString()) && x.Loggued == true && x.AvailableAt != null).OrderBy(x => x.AvailableAt).FirstOrDefaultAsync();
        if (getFirstHealCareStaffAvailable != null)
            return RequestResult.SuccessResult(data: getFirstHealCareStaffAvailable.Id);
        return RequestResult.SuccessResultNoRecords(message: "No hay médicos disponibles");
    }

}