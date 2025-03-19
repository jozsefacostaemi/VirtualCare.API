﻿using Domain.Interfaces.AuthomatedProcesses;
using MediatR;
using Shared;

namespace Application.Modules.MedicalRecords.Commands;
public record InProcessAttentionAuthomaticCommand(int number) : IRequest<RequestResult>;

public sealed class InProcessAttentionAuthomaticCommandHandler : IRequestHandler<InProcessAttentionAuthomaticCommand, RequestResult>
{
    private readonly IAuthomatedProcessesRepository _iauthomatedProcessesRepository;

    public InProcessAttentionAuthomaticCommandHandler(IAuthomatedProcessesRepository iauthomatedProcessesRepository) =>
        _iauthomatedProcessesRepository = iauthomatedProcessesRepository ?? throw new ArgumentNullException(nameof(iauthomatedProcessesRepository));

    public async Task<RequestResult> Handle(InProcessAttentionAuthomaticCommand command, CancellationToken cancellationToken)
    {
        var result = await _iauthomatedProcessesRepository.ProcessAttentions(3, command.number);
        if (!result.Success)
            return RequestResult.ErrorRecord(result.Message);
        return RequestResult.SuccessUpdate(message: result.Message, data: result.Data);
    }
}

