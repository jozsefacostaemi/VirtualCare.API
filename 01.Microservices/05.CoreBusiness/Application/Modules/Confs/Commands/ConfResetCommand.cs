using Domain.Interfaces.Confs;
using Domain.Interfaces.Queues;
using MediatR;
using Shared.Common.RequestResult;

namespace Application.Modules.Queues.Commands
{
    public record ConfsResetCommand() : IRequest<RequestResult>;

    public sealed class ConfsResetCommandHandle : IRequestHandler<ConfsResetCommand, RequestResult>
    {
        private readonly IConfResetRepository _IConfResetRepository;

        public ConfsResetCommandHandle(IConfResetRepository IConfResetRepository)
        {
            _IConfResetRepository = IConfResetRepository ?? throw new ArgumentNullException(nameof(IConfResetRepository));
        }

        public async Task<RequestResult> Handle(ConfsResetCommand command, CancellationToken cancellationToken)
        {
            bool result = await _IConfResetRepository.ResetAttentionsAndPersonStatus();
            if (!result)
                return RequestResult.ErrorRecord();
            return RequestResult.SuccessUpdate();
        }


    }


}
