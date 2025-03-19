using Auth.VirtualCare.Domain.Interfaces.Auth;
using MediatR;
using Shared;
namespace Auth.VirtualCare.Application.Modules.Auth.Commands;
public record LogOutCommand(Guid UserId) : IRequest<RequestResult>;
public sealed class LogOutCommandHandle : IRequestHandler<LogOutCommand, RequestResult>
{
    private readonly IAuthRepository _IAuthRepository;
    public LogOutCommandHandle(IAuthRepository IAuthRepository) =>
        _IAuthRepository = IAuthRepository ?? throw new ArgumentNullException(nameof(IAuthRepository));
    public async Task<RequestResult> Handle(LogOutCommand command, CancellationToken cancellationToken)
        => await _IAuthRepository.LogOut(command.UserId);
}

