using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FryTech.Essentials.FluentValidation.Tools;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds validators from EntryAssembly when is not null or from CallingAssembly otherwise
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddFluentValidators(this IServiceCollection services)
    {
        return AddFluentValidators(services, Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly());
    }

    public static IServiceCollection AddFluentValidators(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddValidatorsFromAssemblies(assemblies,
            filter: result => result.ValidatorType.GetCustomAttribute<DoNotRegisterAttribute>() is null);
        
        return services;
    }
}