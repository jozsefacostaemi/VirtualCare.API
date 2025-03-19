namespace StateMachines;
public class SagaOrchestrator
{
    private readonly StateMachine _stateMachine;
    public SagaOrchestrator(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }
    /* Función que valida la información de la atención y la transición de esta para aplicar el cambio de estados de las entidades */
    public async Task<(bool, string)> ValidateEvent(EntityEventStateMachine entityEvent)
    {
        (bool success, string message) = await _stateMachine.ValidateData(entityEvent);
        if (!success)
            return (false, message);
        return (_stateMachine.CanTransitionTo(entityEvent.ActualPatientStateCode, entityEvent.ActualUserStateCode, entityEvent.ActualMedicalRecordStateCode, entityEvent.EventType));
    }
    /* Función que consulta la transición de estados de las entidades con base a un evento */
    public async Task<(bool, string)> HandleEvent(EntityEventStateMachine entityEvent) => await _stateMachine.TransitionTo(entityEvent);
}


