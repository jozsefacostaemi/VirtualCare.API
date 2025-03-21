using System.Globalization;
using Application.Common.Behaviors;
using Domain.Interfaces.Messages;
using Domain.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Shared.Common.RequestResult;
using Web.Core.API.Application.Common.Decorators;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(config => { config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>(); });
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

            //Localization
            services.AddScoped<IMessageService, MessageService>();
            services.AddLocalization();

            //Decorators
            services.AddDecorators();
            return services;
        }
        private static IServiceCollection AddLocalization(this IServiceCollection services)
        {

            // Configurar la localización
            services.AddLocalization(options => options.ResourcesPath = "../Domain/Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                new CultureInfo("en-US"),
                new CultureInfo("es-CO")
            };

                options.DefaultRequestCulture = new RequestCulture("es-CO");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                // Configurar el proveedor de localización para leer el idioma de la cabecera
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    var userLangs = context.Request.Headers["Accept-Language"].ToString();
                    var firstLang = userLangs.Split(',').FirstOrDefault()?.Trim(); // Elimina espacios en blanco

                    // Si firstLang es nulo o vacío, usa un valor por defecto
                    if (string.IsNullOrWhiteSpace(firstLang))
                    {
                        firstLang = "es-CO"; // Idioma predeterminado
                    }

                    return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(firstLang, firstLang));
                }));


            });
            return services;

        }

        public static IServiceCollection AddDecorators(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationAssemblyReference).Assembly;

            // Encuentra todas las implementaciones de IRequestHandler, excluyendo los decoradores
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)) &&
                            !typeof(IDecorators).IsAssignableFrom(t))
                .ToList();
            foreach (var handlerType in handlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
                var requestType = interfaceType.GetGenericArguments()[0];

                if (typeof(IRequest<RequestResult>).IsAssignableFrom(requestType))
                {
                    // Registra el decorador para comandos y queries
                    var decoratorType = typeof(ModuleCommandHandlerDecorator<>).MakeGenericType(requestType);
                    services.Decorate(interfaceType, decoratorType);
                }
            }
            return services;
        }
    }
}
