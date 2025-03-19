using Application.Data;
using Domain.Enums;
using Domain.Interfaces.Messages;
using Microsoft.EntityFrameworkCore;

namespace StateMachines;

public class StateMachine
{
    #region Variables
    private readonly IMessageService _IMessageService;
    private readonly GetMachineStateValidator _GetMachineStateValidator;
    private readonly IApplicationDbContext _context;
    #endregion

    #region Ctor 
    public StateMachine(IMessageService IMessageService, GetMachineStateValidator GetMachineStateValidator, IApplicationDbContext context)
    {
        _IMessageService = IMessageService;
        _GetMachineStateValidator = GetMachineStateValidator;
        _context = context;
    }
    #endregion

    #region Public Methods 
    /* Función que valida si se puede hacer la transición de una estado a otro con base a los estados actuales de cada entidad */
    public (bool, string) CanTransitionTo(PatientStateEnum? patientState, UserStateEnum? userState, MedicalRecordStateEnum? medicalRecordState, StateEventProcessEnum? eventType)
    {
        return eventType switch
        {
            StateEventProcessEnum.CREATED =>
                patientState == PatientStateEnum.ATTENDED || patientState == PatientStateEnum.CANCELLED || patientState == PatientStateEnum.EMPTY
                ? (true, _IMessageService.GetTransitionValidMessage())
                : (false, _IMessageService.GetInvalidPatientStateForCreationMessage()),

            StateEventProcessEnum.ASSIGNED =>
                userState == UserStateEnum.AVAILABLE ? (true, _IMessageService.GetTransitionValidMessage())
                : (false, _IMessageService.GetInvalidStateForAssignmentMessage()),

            StateEventProcessEnum.INPROCESS =>
                patientState == PatientStateEnum.ASSIGNED && userState == UserStateEnum.ASSIGNED && medicalRecordState == MedicalRecordStateEnum.ASSIGNED
                ? (true, _IMessageService.GetTransitionValidMessage())
                : (false, _IMessageService.GetInvalidStateForProcessStartMessage()),

            StateEventProcessEnum.FINALIZED =>
                patientState == PatientStateEnum.INPROCESS && userState == UserStateEnum.INPROCESS && medicalRecordState == MedicalRecordStateEnum.INPROCESS
                ? (true, _IMessageService.GetTransitionValidMessage())
                : (false, _IMessageService.GetInvalidStateForFinalizationMessage()),

            StateEventProcessEnum.CANCELLED =>
                (patientState == PatientStateEnum.ATTENWAIT || patientState == PatientStateEnum.ASSIGNED || patientState == PatientStateEnum.INPROCESS) &&
                (userState == UserStateEnum.ASSIGNED || userState == UserStateEnum.INPROCESS) &&
                (medicalRecordState == MedicalRecordStateEnum.ASSIGNED || medicalRecordState == MedicalRecordStateEnum.INPROCESS)
                ? (true, _IMessageService.GetTransitionValidMessage())
                : (false, _IMessageService.GetInvalidStateForCancellationMessage()),

            _ => (false, _IMessageService.GetInvalidEventTypeMessage())
        };
    }
    /* Función que consulta los nuevos IDs de estados de cada entidad */
    public async Task<(bool, string)> TransitionTo(EntityEventStateMachine entityEvent)
    {
        GetStateFromEventType(entityEvent);
        var patientStateTask = await _context.PatientStates.Where(x => x.Code.Equals(entityEvent.NewPatientStateCode.ToString())).Select(x => x.Id).SingleOrDefaultAsync();
        var userStateTask = await _context.UserStates.Where(x => x.Code.Equals(entityEvent.NewUserStateCode.ToString())).Select(x => x.Id).SingleOrDefaultAsync();
        var medicalRecordStateTask = await _context.MedicalRecordStates.Where(x => x.Code.Equals(entityEvent.NewMedicalRecordStateCode.ToString())).Select(x => x.Id).SingleOrDefaultAsync();
        entityEvent.NewPatientStateId = patientStateTask;
        entityEvent.NewUserStateId = userStateTask;
        entityEvent.NewMedicalRecordStateId = medicalRecordStateTask;
        return (true, _IMessageService.GetSucessOperation());
    }
    /* Función que valida la información de la atención según el estado de la misma */
    public async Task<(bool, string)> ValidateData(EntityEventStateMachine entityEvent)
    {
        switch (entityEvent.EventType)
        {
            case StateEventProcessEnum.CREATED:
                return await _GetMachineStateValidator.CreationPreconditions(entityEvent);
            case StateEventProcessEnum.ASSIGNED:
                return await _GetMachineStateValidator.AsignationPreconditions(entityEvent);
            case StateEventProcessEnum.INPROCESS:
                return await _GetMachineStateValidator.InProcessPreconditions(entityEvent);
            case StateEventProcessEnum.INDOCUMENTATION:
                return await _GetMachineStateValidator.InProcessPreconditions(entityEvent);
            case StateEventProcessEnum.FINALIZED:
                return await _GetMachineStateValidator.FinalizationPreconditions(entityEvent);
            case StateEventProcessEnum.CANCELLED:
                return await _GetMachineStateValidator.CancellationPreconditions(entityEvent);
            default:
                break;
        }
        return (false, $"{_IMessageService.GetInformationNotFound()} : {entityEvent.EventType.ToString()}");
    }
    #endregion

    #region Private Methods
    /* Función que con base a un evento de estado, consulta los nuevos códigos de estados de cada entidad */
    private (bool, string) GetStateFromEventType(EntityEventStateMachine entityEvent)
    {
        var newState = entityEvent.EventType switch
        {
            StateEventProcessEnum.CREATED => (PatientStateEnum.ATTENWAIT, UserStateEnum.EMPTY, MedicalRecordStateEnum.PENDING),
            StateEventProcessEnum.ASSIGNED => (PatientStateEnum.ASSIGNED, UserStateEnum.ASSIGNED, MedicalRecordStateEnum.ASSIGNED),
            StateEventProcessEnum.INPROCESS => (PatientStateEnum.INPROCESS, UserStateEnum.INPROCESS, MedicalRecordStateEnum.INPROCESS),
            StateEventProcessEnum.FINALIZED => (PatientStateEnum.ATTENDED, UserStateEnum.AVAILABLE, MedicalRecordStateEnum.ATTENDED),
            StateEventProcessEnum.CANCELLED => (PatientStateEnum.CANCELLED, UserStateEnum.AVAILABLE, MedicalRecordStateEnum.CANCELLED),
            _ => (default, default, default)
        };
        if (newState == (default, default, default))
            return (false, _IMessageService.GetInformationNotFound());
        entityEvent.NewPatientStateCode = newState.Item1;
        entityEvent.NewUserStateCode = newState.Item2;
        entityEvent.NewMedicalRecordStateCode = newState.Item3;
        return (true, _IMessageService.GetSucessOperation());
    }
    #endregion
}
