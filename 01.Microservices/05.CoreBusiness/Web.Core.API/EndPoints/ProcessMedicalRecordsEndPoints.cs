using Application.Modules.MedicalRecords.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Web.Core.API.Commons;

namespace Web.Core.API.EndPoints
{
    public class ProcessMedicalRecordsEndPoints : IEndpoints
    {
        private const string BaseRoute = "ProcessMedicalRecords";
        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            // Endpoint POST /CreateAttention
            app.MapPost($"{BaseRoute}/CreateAttention", CreateAttention)
                .WithName("CreateAttention")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Create attention")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint POST /AssignAttention
            app.MapPost($"{BaseRoute}/AssignAttention", AssignAttention)
                .WithName("AssignAttention")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Assign attention")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint POST /StartAttention
            app.MapPost($"{BaseRoute}/StartAttention", StartAttention)
                .WithName("StartAttention")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Start Attention")
                //.RequireAuthorization()
                .WithOpenApi();


            // Endpoint POST /FinishAttention
            app.MapPost($"{BaseRoute}/FinishAttention", FinishAttention)
                .WithName("FinishAttention")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Finish attention")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint POST /CancelAttention
            app.MapPost($"{BaseRoute}/CancelAttention", CancelAttention)
                .WithName("CancelAttention")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Cancel attention")
                //.RequireAuthorization()
                .WithOpenApi();
        }

        /// <summary>
        /// Function that creates attention.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> CreateAttention([FromBody] CreateAttentionCommand command, ISender mediator) => await mediator.Send(command);

        /// <summary>
        /// Function that assigns attention.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> AssignAttention([FromBody] AssignAttentionCommand command, ISender mediator) => await mediator.Send(command);

        /// <summary>
        /// Function that processes attention.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> StartAttention([FromBody] StartAttentionCommand command, ISender mediator) => await mediator.Send(command);

        /// <summary>
        /// Function that finishes attention.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> FinishAttention([FromBody] FinishAttentionCommand command, ISender mediator) => await mediator.Send(command);

        /// <summary>
        /// Function that cancels attention.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> CancelAttention([FromBody] CancelAttentionCommand command, ISender mediator) => await mediator.Send(command);
    }
}
