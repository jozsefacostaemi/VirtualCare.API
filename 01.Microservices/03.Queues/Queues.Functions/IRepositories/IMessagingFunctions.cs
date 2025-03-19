namespace Lib.MessageQueues.Functions.IRepositories
{
    public interface IMessagingFunctions
    {
        Task CreateQueueAsync(string queueName, bool? durable, bool? exclusive, bool? autoDelete, int? MaxPriority, int? MessageLifeTime, int? QueueExpireTime, string? QueueMode, string? QueueDeadLetterExchange, string? QueueDeadLetterExchangeRoutingKey);
        Task DeleteQueues();
    }
}
