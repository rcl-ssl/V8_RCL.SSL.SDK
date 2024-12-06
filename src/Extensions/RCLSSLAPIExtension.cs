using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RCL.SSL.SDK
{
    public static class RCLSSLAPIExtension
    {
        public static IServiceCollection AddRCLSSLAPIService(this IServiceCollection services,
            Action<ApiOptions> configureOptions)
        {
            services.TryAddTransient<ICertificateService, CertificateService>();
            services.Configure<ApiOptions>(configureOptions);
            return services;
        }
    }
}
