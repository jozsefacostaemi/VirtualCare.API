using Domain.Interfaces.BuildObjects;
using MediatR;
using Shared.Common.RequestResult;

namespace Application.Modules.BuildObjects.Commands;
public record CreateUsersAuthomatedCommand(int number) : IRequest<RequestResult>;

public sealed class CreateUsersAuthomatedCommandHandler : IRequestHandler<CreateUsersAuthomatedCommand, RequestResult>
{
    private readonly IBuildObjectsRepository _IBuildObjectsRepository;

    public CreateUsersAuthomatedCommandHandler(IBuildObjectsRepository IBuildObjectsRepository) =>
        _IBuildObjectsRepository = IBuildObjectsRepository ?? throw new ArgumentNullException(nameof(IBuildObjectsRepository));

    public async Task<RequestResult> Handle(CreateUsersAuthomatedCommand command, CancellationToken cancellationToken)
    {
        bool result = await _IBuildObjectsRepository.CreateUsers(command.number);
        if (!result)
            return RequestResult.ErrorRecord();
        return RequestResult.SuccessUpdate();
    }
}

