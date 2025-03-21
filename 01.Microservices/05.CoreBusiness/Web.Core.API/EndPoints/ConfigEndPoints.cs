using Application.Modules.Queues.Commands;
using MediatR;
using Shared.Common.RequestResult;
using Web.Core.API.Commons;

namespace Web.Core.API.EndPoints
{
    public class ConfigEndPoints : IEndpoints
    {
        private const string BaseRoute = "Config";
        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            // Endpoint POST /Reset
            app.MapPost($"{BaseRoute}/Reset", Reset)
                .WithName("Reset")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Reset all configs")
                //.RequireAuthorization()
                .WithOpenApi();

        }

        /// <summary>
        /// Function that reset confs of users, patients, queues, and others objects.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> Reset(ISender mediator) => await mediator.Send(new ConfsResetCommand());
    }

}
