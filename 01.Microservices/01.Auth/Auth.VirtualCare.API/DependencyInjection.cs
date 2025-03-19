using System.Reflection;
using AspNetCoreRateLimit;
using Auth.VirtualCare.API.Middlewares.GlobalExceptions;
using Microsoft.OpenApi.Models;

namespace Auth.VirtualCare.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddAuthorization();
            services.AddControllers();
            services.AddTransient<GlobalExceptionHandlingMiddleware>();
            //services.AddApplicationInsights(configuration); 
            //TODO: Habilitarlo con repo en Azure para obtener datos de Telemetria en servicios
            services.AddUseRateLimiting(configuration);
            return services;
        }

        private static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Auth API",
                    Version = "v1",
                    Description = "API para la autenticación de usuarios en VirtualCare"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        private static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
        {
            // Application Insights
            services.AddApplicationInsightsTelemetry(options => { options.ConnectionString = configuration["ApplicationInsights:ConnectionString"]; });
            return services;
        }

        private static IServiceCollection AddUseRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar Rate Limiting
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            return services;
        }
    }
}
