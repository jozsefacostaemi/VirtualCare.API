using Application.Modules.BuildObjects.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Web.Core.API.Commons;

namespace Web.Core.API.EndPoints
{
    public class AuthomatedBuildObjectsEndPoints : IEndpoints
    {
        private const string BaseRoute = "AuthomatedBuildObjects";
        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            // Endpoint POST /CreatePatients
            app.MapPost($"{BaseRoute}/CreatePatients", CreatePatients)
                .WithName("CreatePatientss")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Create Patients")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint POST /CreateUsers
            app.MapPost($"{BaseRoute}/CreateUsers", CreateUsers)
                .WithName("CreateUserss")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Create Users")
                //.RequireAuthorization()
                .WithOpenApi();
        }

        /// <summary>
        /// Function that creates patients automatically.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> CreatePatients([FromBody] CreatePatientsAuthomatedCommand command, ISender mediator) => await mediator.Send(command);

        /// <summary>
        /// Function that create users automatically.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> CreateUsers([FromBody] CreateUsersAuthomatedCommand command, ISender mediator) => await mediator.Send(command);


    }

}
