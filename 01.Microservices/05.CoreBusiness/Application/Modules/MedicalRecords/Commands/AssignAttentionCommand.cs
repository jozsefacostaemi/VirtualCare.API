using Domain.Interfaces.MedicalRecords;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared;

namespace Application.Modules.MedicalRecords.Commands;
public record AssignAttentionCommand(Guid UserId) : IRequest<RequestResult>;

public sealed class AssignAttentionCommandHandler : IRequestHandler<AssignAttentionCommand, RequestResult>
{
    private readonly IProcessMedicalRecordRepository IProcessMedicalRecordRepository;

    public AssignAttentionCommandHandler(IProcessMedicalRecordRepository ProcessMedicalRecordRepository) =>
        IProcessMedicalRecordRepository = ProcessMedicalRecordRepository ?? throw new ArgumentNullException(nameof(ProcessMedicalRecordRepository));

    public async Task<RequestResult> Handle(AssignAttentionCommand command, CancellationToken cancellationToken)
    {
        var result = await IProcessMedicalRecordRepository.AssignAttention(command.UserId);
        if (!result.Success)
            return RequestResult.ErrorRecord(result.Message);
        return RequestResult.SuccessUpdate(message: result.Message, data: result.Data);
    }
}

