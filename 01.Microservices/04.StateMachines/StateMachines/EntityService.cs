namespace StateMachines;
public class EntityService
{
    private readonly SagaOrchestrator _sagaOrchestrator;
    public EntityService(SagaOrchestrator sagaOrchestrator) => _sagaOrchestrator = sagaOrchestrator;
    public async Task<(bool, string)> ValidateEntityEvent(EntityEventStateMachine entityEvent) => await _sagaOrchestrator.ValidateEvent(entityEvent);
    public async Task<(bool, string)> HandleEntityEvent(EntityEventStateMachine entityEvent) => await _sagaOrchestrator.HandleEvent(entityEvent);
}

