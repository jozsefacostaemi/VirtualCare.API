using Application.Modules.MedicalRecord.Queries;
using MediatR;
using Shared.Common.RequestResult;
using Web.Core.API.Commons;

namespace Web.Core.API.EndPoints
{
    public class GetMedicalRecordEndPoints : IEndpoints
    {
        private const string BaseRoute = "MedicalRecords";
        public static void DefineEndpoints(IEndpointRouteBuilder app)
        {
            // Endpoint GET /MedicalRecords
            app.MapGet($"{BaseRoute}", GetAllMedicalRecords)
                .WithName("GetAllMedicalRecords")
                .Produces<RequestResult>(200) // Response 200 OK
                .WithDescription("Get the list of all medical records")
                //.RequireAuthorization()
                .WithOpenApi();

        }

        /// <summary>
        /// Function that queries all medical records.
        /// </summary>
        /// <returns>The result of the request.</returns>
        internal static async Task<RequestResult> GetAllMedicalRecords(ISender mediator) => await mediator.Send(new GetAllMedicalRecordQuery());
    }

}
