using Domain.Interfaces.MedicalRecords;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Modules.MedicalRecords.Commands;
public record StartAttentionCommand(Guid MedicalRecordId) : IRequest<RequestResult>;

public sealed class StartAttentionCommandHandler : IRequestHandler<StartAttentionCommand, RequestResult>
{
    private readonly IProcessMedicalRecordRepository IProcessMedicalRecordRepository;

    public StartAttentionCommandHandler(IProcessMedicalRecordRepository ProcessMedicalRecordRepository) =>
        IProcessMedicalRecordRepository = ProcessMedicalRecordRepository ?? throw new ArgumentNullException(nameof(ProcessMedicalRecordRepository));

    public async Task<RequestResult> Handle(StartAttentionCommand command, CancellationToken cancellationToken)
    {
        var result = await IProcessMedicalRecordRepository.StartAttention(command.MedicalRecordId);
        if (!result.Success)
            return RequestResult.ErrorRecord(result.Message);
        return RequestResult.SuccessUpdate(message: result.Message, data: result.Data);
    }
}

