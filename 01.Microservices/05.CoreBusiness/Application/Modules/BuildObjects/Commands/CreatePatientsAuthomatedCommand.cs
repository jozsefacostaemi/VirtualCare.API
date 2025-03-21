using Domain.Interfaces.BuildObjects;
using MediatR;
using Shared.Common.RequestResult;

namespace Application.Modules.BuildObjects.Commands;
public record CreatePatientsAuthomatedCommand(int number) : IRequest<RequestResult>;

public sealed class CreatePatientsAuthomatedCommandHandler : IRequestHandler<CreatePatientsAuthomatedCommand, RequestResult>
{
    private readonly IBuildObjectsRepository _IBuildObjectsRepository;

    public CreatePatientsAuthomatedCommandHandler(IBuildObjectsRepository IBuildObjectsRepository) =>
        _IBuildObjectsRepository = IBuildObjectsRepository ?? throw new ArgumentNullException(nameof(IBuildObjectsRepository));

    public async Task<RequestResult> Handle(CreatePatientsAuthomatedCommand command, CancellationToken cancellationToken)
    {
        bool result = await _IBuildObjectsRepository.CreatePatients(command.number);
        if (!result)
            return RequestResult.ErrorRecord();
        return RequestResult.SuccessUpdate();
    }
}

