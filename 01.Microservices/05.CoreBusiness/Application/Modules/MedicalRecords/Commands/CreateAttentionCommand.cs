using Domain.Interfaces.MedicalRecords;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Common.RequestResult;
using SharedClasses._02.Core.DTOs;

namespace Application.Modules.MedicalRecords.Commands;
public record CreateAttentionCommand(string ProcessCode, Guid PatientId) : IRequest<RequestResult>;

public sealed class CreateAttentionCommandHandler : IRequestHandler<CreateAttentionCommand, RequestResult>
{
    private readonly IProcessMedicalRecordRepository IProcessMedicalRecordRepository;

    public CreateAttentionCommandHandler(IProcessMedicalRecordRepository ProcessMedicalRecordRepository) =>
        IProcessMedicalRecordRepository = ProcessMedicalRecordRepository ?? throw new ArgumentNullException(nameof(ProcessMedicalRecordRepository));

    public async Task<RequestResult> Handle(CreateAttentionCommand command, CancellationToken cancellationToken)
    {
        ResultProcessAttentionDTO result = await IProcessMedicalRecordRepository.CreateAttention(command.ProcessCode, command.PatientId);
        if (!result.Success)
            return RequestResult.ErrorRecord(result.Message);
        return RequestResult.SuccessUpdate(message: result.Message, data: result.AttentionDTO);
    }
}

