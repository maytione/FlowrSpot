using FlowrSpot.Application.Common.Behaviors;
using FlowrSpot.Application.QuoteOfTheDay.Service;
using FlowrSpot.Application.QuoteOfTheDay.Settings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceConfiguration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        });


        // Configure JWT settings - Nothing smart, just fetch from configuration
        //services.AddOptions<QodSettings>();

        

        services.Configure<QodSettings>(configuration.GetSection(nameof(QodSettings)));

        // Configure QuoteOfTheDayService 
        services.AddHttpClient<QuoteOfTheDayService>((serviceProvider, httpClient) =>
        {
            var qodSettings = serviceProvider.GetRequiredService<IOptions<QodSettings>>().Value;
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + qodSettings.ApiKey);
            httpClient.BaseAddress = new Uri(qodSettings.BaseAddress!);
        });

        return services;
    }
}
