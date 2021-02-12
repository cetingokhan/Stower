using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stower
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStower(this IServiceCollection services, Action<IServiceProvider, StowerOptions> configureOptions)
        {
            services.AddOptions<StowerOptions>().Configure<IServiceProvider>((options, resolver) => configureOptions(resolver, options))
                .PostConfigure(options =>
                {
                    if (options.MaxStackLenght <= 0)
                        throw new ArgumentException("MaxStackLenght more than zero!");
                    if (options.MaxWaitInSecond <= 0 || options.MaxWaitInSecond > 50000)
                        throw new ArgumentException("MaxWait must between 0 and 090909");                   

                });

            services.TryAddTransient<IStower>(resolver => resolver.GetRequiredService<BaseStower>());
            return services.AddSingleton<BaseStower>();

        }


        public static IServiceCollection AddStower(this IServiceCollection services, Action<StowerOptions> configureOptions)
        {
            return services.AddStower((_, options) => configureOptions(options));
        }

        public static IServiceCollection AddToppleHandler<T>(this IServiceCollection services) where T: IToppleHandler
        {

            return services.AddSingleton(typeof(IToppleHandler), typeof(T));            
        }
    }
}
