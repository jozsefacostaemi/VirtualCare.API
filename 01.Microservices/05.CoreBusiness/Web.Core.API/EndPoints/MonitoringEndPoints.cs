using Application.Modules.Monitoring.Queries;
using MediatR;
using Shared.Common.RequestResult;
using Web.Core.API.Commons;

namespace Web.Core.API.EndPoints
{
    public class MonitoringEndPoints : IEndpoints
    {
        private const string BaseRoute = "Monitoring";
        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            // Endpoint GET /GetUsageCPU
            app.MapGet($"{BaseRoute}/GetUsageCPU", GetUsageCPU)
                .WithName("GetUsageCPU")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("GetUsageCPU")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint GET /GetQuantityByState
            app.MapGet($"{BaseRoute}/GetQuantityByState", GetQuantityByState)
                .WithName("GetQuantityByState")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("GetQuantityByState")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint GET /GetAttentionsFinishByHealthCareStaff
            app.MapGet($"{BaseRoute}/GetAttentionsFinishByHealthCareStaff", GetAttentionsFinishByHealthCareStaff)
                .WithName("GetAttentionsFinishByHealthCareStaff")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("GetAttentionsFinishByHealthCareStaff")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint GET /GetLogguedByHealthCareStaff
            app.MapGet($"{BaseRoute}/GetLogguedByHealthCareStaff", GetLogguedByHealthCareStaff)
                .WithName("GetLogguedByHealthCareStaff")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("GetLogguedByHealthCareStaff")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint GET /GetAttentionsByTimeLine
            app.MapGet($"{BaseRoute}/GetAttentionsByTimeLine", GetAttentionsByTimeLine)
                .WithName("GetAttentionsByTimeLine")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("GetAttentionsByTimeLine")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint GET /GetPercentAttentionsFinish
            app.MapGet($"{BaseRoute}/GetPercentAttentionsFinish", GetPercentAttentionsFinish)
                .WithName("GetPercentAttentionsFinish")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("GetPercentAttentionsFinish")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint GET /GetNumberAttentionsByCity
            app.MapGet($"{BaseRoute}/GetNumberAttentionsByCity", GetNumberAttentionsByCity)
                .WithName("GetNumberAttentionsByCity")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("GetNumberAttentionsByCity")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint GET /GetQueuesActive
            app.MapGet($"{BaseRoute}/GetQueuesActive", GetQueuesActive)
                .WithName("GetQueuesActive")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("GetQueuesActive")
                //.RequireAuthorization()
                .WithOpenApi();

            // Endpoint GET /GetNumberActive
            app.MapGet($"{BaseRoute}/GetNumberActive", GetNumberActive)
                .WithName("GetNumberActive")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("GetNumberActive")
                //.RequireAuthorization()
                .WithOpenApi();
        }

        /// <summary>
        /// Function that GetUsageCPU.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> GetUsageCPU(ISender mediator) => await mediator.Send(new GetUsageCPUQuery());

        /// <summary>
        /// Function that GetQuantityByState.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> GetQuantityByState(ISender mediator) => await mediator.Send(new GetQuantityByStateQuery());

        /// <summary>
        /// Function that GetAttentionsFinishByHealthCareStaff.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> GetAttentionsFinishByHealthCareStaff(ISender mediator) => await mediator.Send(new GetAttentionsFinishByHealthCareStaffQuery());

        /// <summary>
        /// Function that GetLogguedByHealthCareStaff.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> GetLogguedByHealthCareStaff(ISender mediator) => await mediator.Send(new GetLogguedByHealthCareStaffQuery());

        /// <summary>
        /// Function that GetAttentionsByTimeLine.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> GetAttentionsByTimeLine(ISender mediator) => await mediator.Send(new GetAttentionsByTimeLineQuery());

        /// <summary>
        /// Function that GetPercentAttentionsFinish.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> GetPercentAttentionsFinish(ISender mediator) => await mediator.Send(new GetPercentAttentionsFinishQuery());

        /// <summary>
        /// Function that GetNumberAttentionsByCity.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> GetNumberAttentionsByCity(ISender mediator) => await mediator.Send(new GetNumberAttentionsByCityQuery());

        /// <summary>
        /// Function that GetQueuesActive.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> GetQueuesActive(ISender mediator) => await mediator.Send(new GetQueuesActiveQuery());

        /// <summary>
        /// Function that GetNumberActive.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> GetNumberActive(ISender mediator) => await mediator.Send(new GetNumberActiveQuery());
    }

}
