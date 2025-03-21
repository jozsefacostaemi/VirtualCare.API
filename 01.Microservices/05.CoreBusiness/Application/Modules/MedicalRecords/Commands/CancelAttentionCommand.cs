using Domain.Interfaces.MedicalRecords;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Common.RequestResult;
using SharedClasses._02.Core.DTOs;

namespace Application.Modules.MedicalRecords.Commands;
public record CancelAttentionCommand(Guid MedicalRecordId) : IRequest<RequestResult>;

public sealed class CancelAttentionCommandHandler : IRequestHandler<CancelAttentionCommand, RequestResult>
{
    private readonly IProcessMedicalRecordRepository IProcessMedicalRecordRepository;

    public CancelAttentionCommandHandler(IProcessMedicalRecordRepository ProcessMedicalRecordRepository) =>
        IProcessMedicalRecordRepository = ProcessMedicalRecordRepository ?? throw new ArgumentNullException(nameof(ProcessMedicalRecordRepository));

    public async Task<RequestResult> Handle(CancelAttentionCommand command, CancellationToken cancellationToken)
    {
        ResultProcessAttentionDTO result = await IProcessMedicalRecordRepository.CancelAttention(command.MedicalRecordId);
        if (!result.Success)
            return RequestResult.ErrorRecord(result.Message);
        return RequestResult.SuccessUpdate(message: result.Message, data: result.AttentionDTO);
    }
}

