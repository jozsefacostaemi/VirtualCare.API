using Domain.Interfaces.MedicalRecords;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Modules.MedicalRecords.Commands;
public record FinishAttentionCommand(Guid MedicalRecordId) : IRequest<RequestResult>;

public sealed class FinishAttentionCommandHandler : IRequestHandler<FinishAttentionCommand, RequestResult>
{
    private readonly IProcessMedicalRecordRepository IProcessMedicalRecordRepository;

    public FinishAttentionCommandHandler(IProcessMedicalRecordRepository ProcessMedicalRecordRepository) =>
         IProcessMedicalRecordRepository = ProcessMedicalRecordRepository ?? throw new ArgumentNullException(nameof(ProcessMedicalRecordRepository));

    public async Task<RequestResult> Handle(FinishAttentionCommand command, CancellationToken cancellationToken)
    {
        var result = await IProcessMedicalRecordRepository.FinishAttention(command.MedicalRecordId);
        if (!result.Success)
            return RequestResult.ErrorRecord(result.Message);
        return RequestResult.SuccessUpdate(message: result.Message, data: result.Data);
    }
}

