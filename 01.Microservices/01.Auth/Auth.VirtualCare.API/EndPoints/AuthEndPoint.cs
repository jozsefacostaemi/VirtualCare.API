namespace Auth.VirtualCare.API.EndPoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Auth.VirtualCare.API.Common;
using Application.Modules.Auth.Commands;
using MediatR;
using Shared.Common.RequestResult;

public class AuthEndpoints : IEndpoints
{
    private const string BaseRoute = "Auth";
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // Endpoint POST /Auth/Login
        app.MapPost($"{BaseRoute}/Login", Login)
            .WithName("Login")
            .Produces<RequestResult>(200)
            .WithDescription("Allows a user to log in")
            .WithOpenApi();
    }

    /// <summary>
    /// Manage the login request.
    /// </summary>
    /// <param name="command">Auth command.</param>
    /// <param name="mediator">Mediator for send command.</param>
    /// <returns>El resultado de la solicitud.</returns>
    internal static async Task<RequestResult> Login([FromBody] LoginCommand command, ISender mediator) =>
        await mediator.Send(command);
}
