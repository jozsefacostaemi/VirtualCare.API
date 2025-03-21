using Auth.VirtualCare.Domain.Interfaces.Auth;
using MediatR;
using Shared._01.Auth.DTOs;
using Shared.Common.RequestResult;

namespace Auth.VirtualCare.Application.Modules.Auth.Commands;

public record LoginCommand(string userName, string password) : IRequest<RequestResult>;

public sealed class AuthCommandHandle : IRequestHandler<LoginCommand, RequestResult>
{
    private readonly IAuthRepository _IAuthRepository;

    public AuthCommandHandle(IAuthRepository IAuthRepository) =>
        _IAuthRepository = IAuthRepository ?? throw new ArgumentNullException(nameof(IAuthRepository));

    public async Task<RequestResult> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        LoginResultDTO? result = await _IAuthRepository.Login(command.userName, command.password);
        if (result.Success)
            return RequestResult.SuccessOperation(result.Token, result.Message);
        return RequestResult.SuccessResultNoRecords(message: result.Message);
    }
}
