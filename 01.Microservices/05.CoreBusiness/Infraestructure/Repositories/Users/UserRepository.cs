using Domain.Interfaces.Users;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Shared;
using Application.Modules.Users.Responses;

namespace Infraestructure.Repositories.Users;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly List<string> ValidChangeStates = new List<string> { UserStateEnum.AVAILABLE.ToString() };

    public UserRepository(ApplicationDbContext context) => _context = context;
    public async Task<RequestResult> UpdateStateForUser(Guid UserId, string codeUser)
    {
        if (!ValidChangeStates.Contains(codeUser))
            return RequestResult.SuccessResultNoRecords(message: $"Por favor indique un código de estado valido (DISP)");
        Guid? stateForUser = await _context.UserStates
            .Where(x => x.Code.Equals(codeUser))
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        if (stateForUser == null || stateForUser == Guid.Empty)
            return RequestResult.SuccessResultNoRecords(message: $"No existe un estado de atención con código: {codeUser}");
        var User = await _context.Users
            .Where(x => x.Id.Equals(UserId))
            .FirstOrDefaultAsync();
        if (User == null)

            return RequestResult    .SuccessResultNoRecords(message: $"No existe un personal asistencial con el id indicado");
        User.UserStateId = stateForUser.Value;
        await _context.SaveChangesAsync();
        return RequestResult.SuccessOperation(message: "Estado del personal médico actualizado correctamente");
    }

    public async Task<RequestResult> GetStateByUser(Guid UserId)
    {
        UserResponseDTO? User = await _context.Users
            .Where(x => x.Id.Equals(UserId))
            .Select(x => new UserResponseDTO { ActualStateDesc = x.UserState != null ? x.UserState.Name : "N/A", ActualStateId = x.UserStateId, ActualStateCode = x.UserState != null ? x.UserState.Code : "Vacio", UserId = x.Id, UserName = x.Name })
            .FirstOrDefaultAsync();
        if (User == null)
            return RequestResult.SuccessResultNoRecords(message: "No existe el medico con el id indicado");
        return RequestResult.SuccessResult(data: User);
    }


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