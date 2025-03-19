using Domain.Interfaces.AuthomatedProcesses;
using MediatR;
using Shared;

namespace Application.Modules.MedicalRecords.Commands;
public record FinishAttentionAuthomaticCommand(int number) : IRequest<RequestResult>;

public sealed class FinishAttentionAuthomaticCommandHandler : IRequestHandler<FinishAttentionAuthomaticCommand, RequestResult>
{
    private readonly IAuthomatedProcessesRepository _iauthomatedProcessesRepository;

    public FinishAttentionAuthomaticCommandHandler(IAuthomatedProcessesRepository iauthomatedProcessesRepository) =>
        _iauthomatedProcessesRepository = iauthomatedProcessesRepository ?? throw new ArgumentNullException(nameof(iauthomatedProcessesRepository));

    public async Task<RequestResult> Handle(FinishAttentionAuthomaticCommand command, CancellationToken cancellationToken)
    {
        var result = await _iauthomatedProcessesRepository.ProcessAttentions(4, command.number);
        if (!result.Success)
            return RequestResult.ErrorRecord(result.Message);
        return RequestResult.SuccessUpdate(message: result.Message, data: result.Data);
    }
}

