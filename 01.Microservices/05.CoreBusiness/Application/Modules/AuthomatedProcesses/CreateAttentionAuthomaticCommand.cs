﻿using Domain.Interfaces.AuthomatedProcesses;
using MediatR;
using Shared;

namespace Application.Modules.MedicalRecords.Commands;
public record CreateAttentionAuthomaticCommand(int number) : IRequest<RequestResult>;

public sealed class CreateAttentionAuthomaticCommandHandler : IRequestHandler<CreateAttentionAuthomaticCommand, RequestResult>
{
    private readonly IAuthomatedProcessesRepository _iauthomatedProcessesRepository;

    public CreateAttentionAuthomaticCommandHandler(IAuthomatedProcessesRepository iauthomatedProcessesRepository) =>
        _iauthomatedProcessesRepository = iauthomatedProcessesRepository ?? throw new ArgumentNullException(nameof(iauthomatedProcessesRepository));

    public async Task<RequestResult> Handle(CreateAttentionAuthomaticCommand command, CancellationToken cancellationToken)
    {
        var result = await _iauthomatedProcessesRepository.ProcessAttentions(1, command.number);
        if (!result.Success)
            return RequestResult.ErrorRecord(result.Message);
        return RequestResult.SuccessUpdate(message: result.Message, data: result.Data);
    }
}

