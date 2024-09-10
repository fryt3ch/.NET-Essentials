using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FryTech.Essentials.FluentValidation.Tools;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFluentValidators(this IServiceCollection services)
    {
        return AddFluentValidators(services, Assembly.GetExecutingAssembly());
    }

    public static IServiceCollection AddFluentValidators(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddValidatorsFromAssemblies(assemblies,
            filter: result => result.ValidatorType.GetCustomAttribute<DoNotRegisterAttribute>() is null);
        
        return services;
    }
}