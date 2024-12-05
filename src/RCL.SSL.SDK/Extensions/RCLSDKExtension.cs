using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RCL.SSL.SDK
{
    public static class RCLSDKExtension
    {
        public static IServiceCollection AddRCLSSLSDK(this IServiceCollection services,
            Action<ApiOptions> configureOptions)
        {
            services.TryAddTransient<ICertificateService, CertificateService>();
            services.Configure<ApiOptions>(configureOptions);
            return services;
        }
    }
}
