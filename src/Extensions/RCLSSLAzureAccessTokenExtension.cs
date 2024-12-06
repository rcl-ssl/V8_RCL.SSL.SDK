using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RCL.SSL.SDK
{
    public static class RCLSSLAzureAccessTokenExtension
    {
        public static IServiceCollection AddRCLSSLAzureAccessTokenService(this IServiceCollection services,
            Action<MicrosoftEntraAppOptions> configureOptions)
        {
            services.TryAddTransient<IAzureAccessTokenService, AzureAccessTokenService>();
            services.Configure<MicrosoftEntraAppOptions>(configureOptions);
            return services;
        }
    }
}
