using System.Globalization;
using Application.Common.Behaviors;
using Domain.Interfaces.Messages;
using Domain.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
