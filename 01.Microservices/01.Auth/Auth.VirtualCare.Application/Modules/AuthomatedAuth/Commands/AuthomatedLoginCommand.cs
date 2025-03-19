using Auth.VirtualCare.Domain.Interfaces.Auth;
using Auth.VirtualCare.Domain.Interfaces.AuthomatedAuth;
using MediatR;
using Shared;
namespace Auth.VirtualCare.Application.Modules.Auth.Commands;
public record AuthomatedLoginCommand(int number) : IRequest<RequestResult>;
public sealed class AuthomatedLoginCommandHandle : IRequestHandler<AuthomatedLoginCommand, RequestResult>
{
    private readonly IAuthomatedAuthRepository _IAuthomatedAuthRepository;
    public AuthomatedLoginCommandHandle(IAuthomatedAuthRepository IAuthomatedAuthRepository) =>
        _IAuthomatedAuthRepository = IAuthomatedAuthRepository ?? throw new ArgumentNullException(nameof(IAuthomatedAuthRepository));
    public async Task<RequestResult> Handle(AuthomatedLoginCommand command, CancellationToken cancellationToken)
        => await _IAuthomatedAuthRepository.AuthomatedLogin(command.number);
}

