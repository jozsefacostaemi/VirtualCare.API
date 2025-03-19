namespace Domain.Interfaces.Queues
{
    public interface IQueueRepository
    {
        Task<bool> GeneratedConfigQueues();
    }
}
