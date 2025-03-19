using System.Text;
using Application.Data;
using Domain.Interfaces.AuthomatedProcesses;
using Domain.Interfaces.BuildObjects;
using Domain.Interfaces.Confs;
using Domain.Interfaces.MedicalRecords;
using Domain.Interfaces.Monitoring;
using Domain.Interfaces.Queues;
using Domain.Interfaces.Users;
using Infraestructure.Repositories.AuthomatedProcesses;
using Infraestructure.Repositories.BuildObjects;
using Infraestructure.Repositories.Confs;
using Infraestructure.Repositories.MedicalRecords;
using Infraestructure.Repositories.Monitoring;
using Infraestructure.Repositories.Users;
using Lib.MessageQueues.Functions.IRepositories;
using Lib.MessageQueues.Functions.Repositories.RabbitMQ;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Notifications;
using Queues.Functions.Models;
using StateMachines;
using Web.Core.Business.API.Infraestructure.Persistence.Repositories.Queue;

namespace Infraestructure
{
    /// <summary>
    /// Clase para la configuración de inyección de dependencias
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Agrega los servicios de infraestructura al contenedor de DI
        /// </summary>
        /// <param name="services">Colección de servicios</param>
        /// <param name="configuration">Configuración de la aplicación</param>
        /// <returns>Colección de servicios configurada</returns>
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(configuration);
            services.AddPersistence(configuration);
            services.AddConfMessagingOschestrator(configuration);
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de la base de datos
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            // Repositorios principales
            services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
            services.AddScoped<IQueueRepository, QueueRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProcessMedicalRecordRepository, ProcessMedicalRecordRepository>();

            // Repositorios de procesos y configuración
            services.AddScoped<IAuthomatedProcessesRepository, AuthomatedProcessesRepository>();
            services.AddScoped<IBuildObjectsRepository, BuildObjectsRepository>();
            services.AddScoped<IConfResetRepository, ConfResetRepository>();
            services.AddScoped<IMonitoringRepository, MonitoringRepository>();

            // Servicios de notificaciones
            services.AddScoped<NotificationRepository>();

            // Servicios de máquina de estados
            services.AddScoped<GetMachineStateValidator>();
            services.AddScoped<EntityService>();
            services.AddScoped<SagaOrchestrator>();
            services.AddScoped<StateMachine>();

            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwt = configuration.GetSection("Jwt");
            var issuer = jwt.GetValue<string>("Issuer");
            var audience = jwt.GetValue<string>("Audience");
            var keyEncript = jwt.GetValue<string>("SecretKey");

            if (string.IsNullOrWhiteSpace(keyEncript))
                throw new InvalidOperationException("La clave secreta (SecretKey) no puede ser nula o vacía en la configuración de JWT.");

            if (string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
                throw new InvalidOperationException("El emisor (Issuer) y la audiencia (Audience) no pueden ser nulos o vacíos en la configuración de JWT.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyEncript))
                    };
                });

            return services;
        }

        private static IServiceCollection AddConfMessagingOschestrator(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettingDTO>(configuration.GetSection("RabbitMQConf"));
            services.AddScoped<RabbitMQConsumer>();
            services.AddScoped<RabbitMQPublisher>();
            services.AddScoped<IRabbitMQFunctions, RabbitMQFunctions>();
            return services;
        }
    }
}



