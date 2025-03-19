
using System.Reflection;
using Microsoft.OpenApi.Models;
using Web.Core.API.Middlewares.GlobalExceptions;
using Web.Core.API.Middlewares.HealthCheck;


namespace Web.Core.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();

            services.ConfigureHealthChecks(configuration);
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder
                    .WithOrigins("http://localhost:4200", "http://localhost:52528", "http://localhost:56681", "http://www.virtualcare.somee.com")
                        .AllowCredentials()
                    .AllowAnyHeader()
                        .AllowAnyMethod());
            });
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddTransient<GlobalExceptionHandlingMiddleware>();
            return services;
        }
        private static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Configuración del documento Swagger
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BusinessCore API",
                    Version = "v1",
                    Description = "API for management all users transactions VirtualCare"
                });

                // Definición de seguridad para JWT Bearer
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Type 'Bearer' after a space and then the token JWT.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // Requisito de seguridad para usar el esquema Bearer
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                // Incluir comentarios XML para la documentación
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}
