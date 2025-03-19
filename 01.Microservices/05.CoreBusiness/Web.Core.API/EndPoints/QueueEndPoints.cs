using Application.Modules.Queues.Commands;
using MediatR;
using Shared;
using Web.Core.API.Commons;

namespace Web.Core.API.EndPoints
{
    public class QueueEndPoints : IEndpoints
    {
        private const string BaseRoute = "Queues";
        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            // Endpoint POST /Queues
            app.MapPost($"{BaseRoute}", QueuesConfigs)
                .WithName("QueuesConfigs")
                .Produces<RequestResult>(200) // Respuesta 200 OK
                .WithDescription("Process Queues")
                //.RequireAuthorization()
                .WithOpenApi();

        }

        /// <summary>
        /// Function that creates queues configurations.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> QueuesConfigs(ISender mediator) => await mediator.Send(new QueueConfCommand());
    }

}
