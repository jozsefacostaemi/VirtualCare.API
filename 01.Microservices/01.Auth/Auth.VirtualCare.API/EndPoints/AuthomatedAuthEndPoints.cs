namespace Auth.VirtualCare.API.EndPoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Auth.VirtualCare.API.Common;
using Application.Modules.Auth.Commands;
using MediatR;
using Shared.Common.RequestResult;

public class AuthomatedAuthEndpoints : IEndpoints
{
    private const string BaseRoute = "AuthomatedAuth";
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        // Endpoint POST /AuthomatedAuth/AuthomatedLogin
        app.MapPost($"{BaseRoute}/AuthomatedLogin", AuthomatedLogin)
            .WithName("AuthomatedLogin")
            .Produces<RequestResult>(200)
            .WithDescription("Allows a user to log in of way massive")
            .WithOpenApi();

        // Endpoint POST /AuthomatedAuth/AuthomatedLogOut
        app.MapPost($"{BaseRoute}/AuthomatedLogOut", AuthomatedLogOut)
            .WithName("AuthomatedLogOut")
            .Produces<RequestResult>(200)
            .WithDescription("Allows a user to log Out of way massive")
            .WithOpenApi();
    }



    /// <summary>
    /// Manage the login request.
    /// </summary>
    /// <param name="command">Auth command.</param>
    /// <param name="mediator">Mediator for send command.</param>
    /// <returns>El resultado de la solicitud.</returns>
    internal static async Task<RequestResult> AuthomatedLogin([FromBody] AuthomatedLoginCommand command, ISender mediator) =>
        await mediator.Send(command);


    /// <summary>
    /// Manage the login request.
    /// </summary>
    /// <param name="command">Auth command.</param>
    /// <param name="mediator">Mediator for send command.</param>
    /// <returns>El resultado de la solicitud.</returns>
    internal static async Task<RequestResult> AuthomatedLogOut([FromBody] AuthomatedLogOutCommand command, ISender mediator) =>
        await mediator.Send(command);
}
