using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FryTech.Essentials.FluentValidation.OptionsValidation;

public class FluentValidateOptions<TOptions> : IValidateOptions<TOptions>
    where TOptions : class
{
    private readonly IServiceProvider _serviceProvider;
    private readonly string? _name;

    public FluentValidateOptions(string? name, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _name = name;
    }

    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (_name != null && _name != name)
        {
            return ValidateOptionsResult.Skip;
        }

        ArgumentNullException.ThrowIfNull(options);

        using var scope = _serviceProvider.CreateScope();

        var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

        var results = validator.Validate(options);

        if (results.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        var typeName = options.GetType().Name;

        var errors = results.Errors.Select(x => $"Fluent validation failed for <{typeName}.{x.PropertyName}> with the error: <{x.ErrorMessage}>.");

        return ValidateOptionsResult.Fail(errors);
    }
}