using AspNetCoreRateLimit;
using Auth.VirtualCare.API;
using Auth.VirtualCare.API.EndPoints;
using Auth.VirtualCare.API.Middlewares.GlobalExceptions;
using Auth.VirtualCare.API.Middlewares.Interceptors;
using Auth.VirtualCare.Application;
using Auth.VirtualCare.Infraestructure;
using NLog;
using NLog.Web;
using Notifications;
using Prometheus;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddPresentationAuth(builder.Configuration).AddInfraestructureAuth(builder.Configuration).AddAplicationAuth();

    // Configure NLog
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    var app = builder.Build();
    if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    {
        app.UseCors("AllowOrigin");
        app.UseHsts();
    }

    // Mapeamos los endpoints
    AuthEndpoints.DefineEndpoints(app);
    AuthomatedAuthEndpoints.DefineEndpoints(app);

    // Configure the HTTP request pipeline.
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();


    app.MapControllers();
    app.UseRequestLocalization();

    app.UseHttpMetrics();
    app.MapMetrics();

    app.UseIpRateLimiting();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API V1");
        options.RoutePrefix = "swagger";
    });

    app.MapHub<EventHub>("/eventhubb");
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

