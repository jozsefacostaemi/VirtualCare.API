using Application.Modules.MedicalRecords.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Web.Core.API.Commons;

namespace Web.Core.API.EndPoints
{
    public class AuthomatedAttentionsEndPoints : IEndpoints
    {
        private const string BaseRoute = "AuthomatedAttentions";
        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            // Endpoint POST /CreateAttentions
            app.MapPost($"{BaseRoute}/CreateAttentions", CreateAttentions)
                .WithName("CreateAttentions")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Create Attentions")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint POST /AsignAttentions
            app.MapPost($"{BaseRoute}/AsignAttentions", AsignAttentions)
                .WithName("AsignAttentions")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Asign Attentions")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint POST /InProcessAttentions
            app.MapPost($"{BaseRoute}/InProcessAttentions", InProcessAttentions)
                .WithName("InProcessAttentions")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("In Process Attentions")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint POST /FinishAttentions
            app.MapPost($"{BaseRoute}/FinishAttentions", FinishAttentions)
                .WithName("FinishAttentions")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Finish Attentions")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint POST /CancelAttentions
            app.MapPost($"{BaseRoute}/CancelAttentions", CancelAttentions)
                .WithName("CancelAttentions")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Cancel Attentions")
                //.RequireAuthorization()
                .WithOpenApi();

        }

        /// <summary>
        /// Function that creates attentions automatically.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> CreateAttentions([FromBody] CreateAttentionAuthomaticCommand command, ISender mediator) => await mediator.Send(command);

        /// <summary>
        /// Function that assign attentions automatically.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> AsignAttentions([FromBody] AssignAttentionAuthomaticCommand command, ISender mediator) => await mediator.Send(command);

        /// <summary>
        /// Function that processes attentions automatically.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> InProcessAttentions([FromBody] InProcessAttentionAuthomaticCommand command, ISender mediator) => await mediator.Send(command);

        /// <summary>
        /// Function that finish attentions automatically.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> FinishAttentions([FromBody] FinishAttentionAuthomaticCommand command, ISender mediator) => await mediator.Send(command);

        /// <summary>
        /// Function that cancel attentions automatically.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> CancelAttentions([FromBody] CancelAttentionAuthomaticCommand command, ISender mediator) => await mediator.Send(command);
    }

}
