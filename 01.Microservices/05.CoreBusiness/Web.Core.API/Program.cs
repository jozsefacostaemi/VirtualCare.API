using NLog;
using NLog.Web;
using Web.Core.API.EndPoints;
using Web.Core.API;
using Web.Core.API.Middlewares.GlobalExceptions;
using Infraestructure;
using Application;
using Notifications;
using Web.Core.API.Middlewares.Interceptors;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;


var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{

    var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.
    builder.Services.AddPresentation(builder.Configuration).AddInfraestructure(builder.Configuration).AddAplication();

    // Configure NLog
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    var app = builder.Build();
    if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    {
        app.UseCors("AllowOrigin");
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web.Core API V1");
            options.RoutePrefix = "swagger";
        });
    }

    // Endpoint Maps
    GetMedicalRecordEndPoints.DefineEndpoints(app);
    QueueEndPoints.DefineEndpoints(app);
    ProcessMedicalRecordsEndPoints.DefineEndpoints(app);
    AuthomatedAttentionsEndPoints.DefineEndpoints(app);
    AuthomatedBuildObjectsEndPoints.DefineEndpoints(app);
    ConfigEndPoints.DefineEndpoints(app);
    MonitoringEndPoints.DefineEndpoints(app);

    // Mapear el endpoint de Health Checks
    //app.MapHealthChecks("/api/health");
    app.MapHealthChecks("/api/health", new HealthCheckOptions() { Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
    //HealthCheck Middleware
    app.UseHealthChecksUI(delegate (HealthChecks.UI.Configuration.Options options) { options.UIPath = "/healthcheck-ui"; });

    // Configure the HTTP request pipeline.
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseRequestLocalization();
    app.MapHub<EventHub>("/eventhub");
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    app.UseMiddleware<SignalRAfterResponseMiddleware>();
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, $"The program was stopped because there was an error: {ex.Message}");
}
finally
{
    NLog.LogManager.Shutdown();
}
