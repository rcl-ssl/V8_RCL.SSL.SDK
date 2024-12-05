#nullable disable

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace RCL.SSL.SDK
{
    public static class DependencyResolver
    {
        public static ServiceProvider ServiceProvider()
        {
            ServiceProvider serviceProvider = null;

            try
            {
                IServiceCollection services = new ServiceCollection();

                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddUserSecrets<TestProject>();
                IConfiguration configuration = builder.Build();
                services.AddRCLSSLSDK(options => configuration.Bind("API", options));
                services.Configure<Certificate>(options => configuration.Bind("Certificate",options));

                serviceProvider = services.BuildServiceProvider();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            return serviceProvider;
        }
    }

    public class TestProject
    {
    }
}
