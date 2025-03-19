using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Auth.VirtualCare.Infraestructure.Configurations;
using Auth.VirtualCare.Domain.Interfaces.Auth;
using Auth.VirtualCare.Infraestructure.Repositories;
using Auth.VirtualCare.API;
using Auth.VirtualCare.Domain.Interfaces.Messages;
using Auth.VirtualCare.Domain.Services;
using Auth.VirtualCare.Domain.Interfaces.AuthomatedAuth;
using Notifications;

namespace Auth.VirtualCare.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthenticationLib(configuration);
            services.AddPersistence(configuration);
            return services;
        }
        private static IServiceCollection AddAuthenticationLib(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.GetSection("Jwt").Bind(jwtSettings);
            services.Configure<JwtSettings>(options =>
            {
                options.SecretKey = jwtSettings.SecretKey;
                options.HashSize = jwtSettings.HashSize;
                options.Iterations = jwtSettings.Iterations;
                options.ExpiresMinutes = jwtSettings.ExpiresMinutes;
                options.SaltSize = jwtSettings.SaltSize;
                options.Audience = jwtSettings.Audience;
                options.Issuer = jwtSettings.Issuer;
            });
            return services;
        }
        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //AplicationDbContext
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

            //Repositories
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthomatedAuthRepository, AuthomatedAuthRepository>();

            //Notifications
            services.AddScoped<NotificationRepository>();
            return services;
        }
    }
}
