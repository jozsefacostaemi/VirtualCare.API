
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.AuthomatedProcesses;
using Domain.Interfaces.MedicalRecords;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infraestructure.Repositories.AuthomatedProcesses;

internal class AuthomatedProcessesRepository : IAuthomatedProcessesRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IProcessMedicalRecordRepository _IProcessMedicalRecordRepository;
    public AuthomatedProcessesRepository(ApplicationDbContext context, IProcessMedicalRecordRepository IProcessMedicalRecordRepository)
    {
        _context = context;
        _IProcessMedicalRecordRepository = IProcessMedicalRecordRepository;
    }
    /* Función que procesa automaticamente la atención */
    public async Task<RequestResult> ProcessAttentions(int opcion, int number)
    {
        switch (opcion)
        {
            case 1:
                return await CreateAttentions(number);
            case 2:
                return await AssignAttentions(number);
            case 3:
                return await ProcessesAttentions(number, 1);
            case 4:
                return await ProcessesAttentions(number, 2);
            case 5:
                return await ProcessesAttentions(number, 3);
            default:
                return RequestResult.ErrorRecord();
        }
    }
    /* Función que crea automaticamente la atención */
    private async Task<RequestResult> CreateAttentions(int number)
    {
        List<RequestResult> lstResult = new();
        List<string> LstStates = new List<string> { PatientStateEnum.AVAILABLE.ToString(), PatientStateEnum.ATTENDED.ToString(), PatientStateEnum.CANCELLED.ToString(), PatientStateEnum.EMPTY.ToString() };
        List<Patient> patientsPendingAttention = await _context.Patients.Where(x => LstStates.Contains(x.PatientState.Code)).Take(number).ToListAsync();
        List<RequestResult> lstRequestResult = new List<RequestResult>();
        foreach (var dr in patientsPendingAttention)
        {
            var processCode = await _context.Services
                    .OrderBy(c => Guid.NewGuid())
                    .FirstOrDefaultAsync();

            var result = await _IProcessMedicalRecordRepository.CreateAttention(processCode.Code, dr.Id);
            lstRequestResult.Add(result);
        }
        return RequestResult.SuccessOperation(data: lstRequestResult);
    }
    /* Función que asigna automaticamente la atención */
    private async Task<RequestResult> AssignAttentions(int number)
    {
        List<RequestResult> lstResult = new();
        List<string> LstStates = new List<string> { PatientStateEnum.AVAILABLE.ToString(), PatientStateEnum.ATTENDED.ToString(), PatientStateEnum.CANCELLED.ToString(), PatientStateEnum.EMPTY.ToString() };
        List<User> patientsPendingAttention = await _context.Users.Where(x => LstStates.Contains(x.UserState.Code)).Take(number).ToListAsync();
        List<RequestResult> lstRequestResult = new List<RequestResult>();
        foreach (var dr in patientsPendingAttention)
        {
            var processCode = await _context.Services
                         .OrderBy(c => Guid.NewGuid())
                         .FirstOrDefaultAsync();

            if (processCode == null) continue;

            var UserAvailable = await SearchFirstUserAvailable(processCode.Code);
            if (UserAvailable.Success == true && UserAvailable.Data != null)
            {
                var resultOperation = await _IProcessMedicalRecordRepository.AssignAttention((Guid)UserAvailable.Data);
                lstResult.Add(resultOperation);
            }
        }
        return RequestResult.SuccessOperation(data: lstRequestResult);
    }
    /* Función que aplica realiza operación de Asignación, Finalización y Cancelación */
    private async Task<RequestResult> ProcessesAttentions(int number, int action)
    {
        List<RequestResult> lstResult = new();
        List<MedicalRecord> patientsPendingAttention = new();

        // Determinar el estado de los registros médicos según la acción
        string stateCode = action switch
        {
            1 => MedicalRecordStateEnum.ASSIGNED.ToString(),
            2 => MedicalRecordStateEnum.INPROCESS.ToString(),
            3 => MedicalRecordStateEnum.INPROCESS.ToString(),
            _ => throw new ArgumentException("Invalid action")
        };

        // Obtener los registros médicos pendientes de atención
        patientsPendingAttention = await _context.MedicalRecords
            .Where(x => x.MedicalRecordState.Code.Equals(stateCode))
            .Take(number)
            .ToListAsync();

        // Procesar los registros médicos según la acción
        foreach (var dr in patientsPendingAttention)
        {
            RequestResult resultInProcess = action switch
            {
                1 => await _IProcessMedicalRecordRepository.StartAttention(dr.Id),
                2 => await _IProcessMedicalRecordRepository.FinishAttention(dr.Id),
                3 => await _IProcessMedicalRecordRepository.CancelAttention(dr.Id),
                _ => throw new ArgumentException("Invalid action")
            };

            lstResult.Add(resultInProcess);
        }

        return RequestResult.SuccessOperation(data: lstResult);
    }
    /* Función que consulta el primer personal asistencial disponible */
    public async Task<RequestResult> SearchFirstUserAvailable(string ProcessCode)
    {
        var getFirstHealCareStaffAvailable = await _context.Users
            .Where(x => x.UserState != null && x.UserServices.Any(x => x.Service.Code.Equals(ProcessCode) && x.ServicePriority) && x.UserState.Code.Equals(UserStateEnum.AVAILABLE.ToString()) && x.Loggued == true && x.AvailableAt != null).OrderBy(x => x.AvailableAt).FirstOrDefaultAsync();
        if (getFirstHealCareStaffAvailable != null)
            return RequestResult.SuccessResult(data: getFirstHealCareStaffAvailable.Id);
        return RequestResult.SuccessResultNoRecords(message: "No hay médicos disponibles");
    }
}
