
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.AuthomatedProcesses;
using Domain.Interfaces.MedicalRecords;
using Domain.Interfaces.Messages;
using Microsoft.EntityFrameworkCore;
using Shared.Common.RequestResult;
using SharedClasses._02.Core.DTOs;
using SharedClasses._02.Core.Responses;

namespace Infraestructure.Repositories.AuthomatedProcesses;

internal class AuthomatedProcessesRepository : IAuthomatedProcessesRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IProcessMedicalRecordRepository _IProcessMedicalRecordRepository;
    private readonly IMessageService _messageService;
    public AuthomatedProcessesRepository(ApplicationDbContext context, IProcessMedicalRecordRepository IProcessMedicalRecordRepository, IMessageService messageService)
    {
        _context = context;
        _IProcessMedicalRecordRepository = IProcessMedicalRecordRepository;
        _messageService = messageService;
    }
    /* Función que procesa automaticamente la atención */
    public async Task<ResultAuthomaticProcessAttentionDTO> ProcessAttentions(int opcion, int number)
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
                return new ResultAuthomaticProcessAttentionDTO (false, _messageService.GetErrorOperation());
        }
    }
    /* Función que crea automaticamente la atención */
    private async Task<ResultAuthomaticProcessAttentionDTO> CreateAttentions(int number)
    {
        List<ResultProcessAttentionDTO> lstResult = new();
        List<string> LstStates = new List<string> { PatientStateEnum.AVAILABLE.ToString(), PatientStateEnum.ATTENDED.ToString(), PatientStateEnum.CANCELLED.ToString(), PatientStateEnum.EMPTY.ToString() };
        List<Patient> patientsPendingAttention = await _context.Patients.Where(x => LstStates.Contains(x.PatientState.Code)).Take(number).ToListAsync();
        foreach (var dr in patientsPendingAttention)
        {
            string? processCode = await _context.Services
                .Select(x => x.Code)
                    .OrderBy(c => Guid.NewGuid())
                    .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(processCode)) continue;

            var result = await _IProcessMedicalRecordRepository.CreateAttention(processCode, dr.Id);
            lstResult.Add(result);
        }
        if (lstResult.Count > 0) return new ResultAuthomaticProcessAttentionDTO(true,_messageService.GetSucessOperation(),lstResult);
        return new ResultAuthomaticProcessAttentionDTO(false, _messageService.GetInformationNotFound());
    }
    /* Función que asigna automaticamente la atención */
    private async Task<ResultAuthomaticProcessAttentionDTO> AssignAttentions(int number)
    {
        List<ResultProcessAttentionDTO> lstResult = new();
        List<string> LstStates = new List<string> { PatientStateEnum.AVAILABLE.ToString(), PatientStateEnum.ATTENDED.ToString(), PatientStateEnum.CANCELLED.ToString(), PatientStateEnum.EMPTY.ToString() };
        List<User> patientsPendingAttention = await _context.Users.Where(x => LstStates.Contains(x.UserState.Code)).Take(number).ToListAsync();
        List<RequestResult> lstRequestResult = new List<RequestResult>();
        foreach (var dr in patientsPendingAttention)
        {
            string? processCode = await _context.Services
              .Select(x => x.Code)
                  .OrderBy(c => Guid.NewGuid())
                  .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(processCode)) continue;

            var UserAvailable = await SearchFirstUserAvailable(processCode);
            if (UserAvailable.Success == true && UserAvailable.Data != null)
            {
                ResultProcessAttentionDTO resultOperation = await _IProcessMedicalRecordRepository.AssignAttention((Guid)UserAvailable.Data);
                lstResult.Add(resultOperation);
            }
        }
        if (lstResult.Count > 0) return new ResultAuthomaticProcessAttentionDTO (true, _messageService.GetSucessOperation(), lstResult);
        return new ResultAuthomaticProcessAttentionDTO (false, _messageService.GetInformationNotFound());
    }
    /* Función que aplica realiza operación de Asignación, Finalización y Cancelación */
    private async Task<ResultAuthomaticProcessAttentionDTO> ProcessesAttentions(int number, int action)
    {
        List<ResultProcessAttentionDTO> lstResult = new();
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
            ResultProcessAttentionDTO resultInProcess = action switch
            {
                1 => await _IProcessMedicalRecordRepository.StartAttention(dr.Id),
                2 => await _IProcessMedicalRecordRepository.FinishAttention(dr.Id),
                3 => await _IProcessMedicalRecordRepository.CancelAttention(dr.Id),
                _ => throw new ArgumentException("Invalid action")
            };

            lstResult.Add(resultInProcess);
        }
        if (lstResult.Count > 0) return new ResultAuthomaticProcessAttentionDTO (true, _messageService.GetSucessOperation(), lstResult);
        return new ResultAuthomaticProcessAttentionDTO (false, _messageService.GetInformationNotFound());
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
