using Domain.Interfaces.Queues;
using MediatR;
using Shared;

namespace Application.Modules.Queues.Commands
{
    public record QueueConfCommand() : IRequest<RequestResult>;

    public sealed class QueueConfCommandHandle : IRequestHandler<QueueConfCommand, RequestResult>
    {
        private readonly IQueueRepository _IQueueRepository;

        public QueueConfCommandHandle(IQueueRepository IQueueRepository)
        {
            _IQueueRepository = IQueueRepository ?? throw new ArgumentNullException(nameof(IQueueRepository));
        }

        public async Task<RequestResult> Handle(QueueConfCommand command, CancellationToken cancellationToken)
        {
            var success = await _IQueueRepository.GeneratedConfigQueues();
            if (!success)
                return RequestResult.ErrorRecord();
            return RequestResult.SuccessOperation();
        }
    }


}
