using Domain.Interfaces.AuthomatedProcesses;
using MediatR;
using Shared.Common.RequestResult;
using SharedClasses._02.Core.Responses;

namespace Application.Modules.AuthomatedProcesses.Commands;
public record CancelAttentionAuthomaticCommand(int number) : IRequest<RequestResult>;

public sealed class CancelAttentionAuthomaticCommandHandler : IRequestHandler<CancelAttentionAuthomaticCommand, RequestResult>
{
    private readonly IAuthomatedProcessesRepository _iauthomatedProcessesRepository;

    public CancelAttentionAuthomaticCommandHandler(IAuthomatedProcessesRepository iauthomatedProcessesRepository) =>
        _iauthomatedProcessesRepository = iauthomatedProcessesRepository ?? throw new ArgumentNullException(nameof(iauthomatedProcessesRepository));

    public async Task<RequestResult> Handle(CancelAttentionAuthomaticCommand command, CancellationToken cancellationToken)
    {
        ResultAuthomaticProcessAttentionDTO result = await _iauthomatedProcessesRepository.ProcessAttentions(5, command.number);
        if (!result.Success)
            return RequestResult.ErrorRecord(result.Message);
        return RequestResult.SuccessUpdate(message: result.Message, data: result.LstResultProcessAttentionsDTO);
    }
}

