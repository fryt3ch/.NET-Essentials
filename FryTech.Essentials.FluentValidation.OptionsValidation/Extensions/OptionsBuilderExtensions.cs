using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FryTech.Essentials.FluentValidation.OptionsValidation.Extensions;

public static class OptionsBuilderExtensions
{
    public static OptionsBuilder<TOptions> ValidateFluentValidation<TOptions>(this OptionsBuilder<TOptions> optionsBuilder)
        where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(provider =>
            new FluentValidateOptions<TOptions>(optionsBuilder.Name, provider));

        return optionsBuilder;
    }
}