using Auth.VirtualCare.Domain.Interfaces.Auth;
using Auth.VirtualCare.Domain.Interfaces.AuthomatedAuth;
using MediatR;
using Shared._01.Auth.DTOs;
using Shared.Common.RequestResult;

namespace Auth.VirtualCare.Application.Modules.Auth.Commands;
public record AuthomatedLogOutCommand(int number) : IRequest<RequestResult>;
public sealed class AuthomatedLogOutCommandHandle : IRequestHandler<AuthomatedLogOutCommand, RequestResult>
{
    private readonly IAuthomatedAuthRepository _IAuthomatedAuthRepository;
    public AuthomatedLogOutCommandHandle(IAuthomatedAuthRepository IAuthomatedAuthRepository) =>
        _IAuthomatedAuthRepository = IAuthomatedAuthRepository ?? throw new ArgumentNullException(nameof(IAuthomatedAuthRepository));
    public async Task<RequestResult> Handle(AuthomatedLogOutCommand command, CancellationToken cancellationToken)
        => RequestResult.SuccessResult(message: "Logout", data: await _IAuthomatedAuthRepository.AuthomatedLogOut(command.number));

}

