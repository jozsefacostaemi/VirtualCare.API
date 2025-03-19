using Auth.VirtualCare.Domain.Interfaces.Auth;
using MediatR;
using Shared;
namespace Auth.VirtualCare.Application.Modules.Auth.Commands;
public record LoginCommand(string userName, string password) : IRequest<RequestResult>;
public sealed class AuthCommandHandle : IRequestHandler<LoginCommand, RequestResult>
{
    private readonly IAuthRepository _IAuthRepository;
    public AuthCommandHandle(IAuthRepository IAuthRepository) =>
        _IAuthRepository = IAuthRepository ?? throw new ArgumentNullException(nameof(IAuthRepository));
    public async Task<RequestResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        => await _IAuthRepository.Login(command.userName, command.password);
}

