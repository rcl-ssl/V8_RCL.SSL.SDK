#nullable disable

using Microsoft.Extensions.Options;

namespace RCL.SSL.SDK.Test
{
    [TestClass]
    public class CertificateServiceTest
    {
        private readonly ICertificateService _certificateService;
        private readonly IOptions<Certificate> _certificateRequestOptons;

        public CertificateServiceTest()
        {
            _certificateService = (ICertificateService)DependencyResolver
                .ServiceProvider().GetService(typeof(ICertificateService));

            _certificateRequestOptons = (IOptions<Certificate>)DependencyResolver.ServiceProvider()
                .GetService(typeof(IOptions<Certificate>));
        }

        [TestMethod]
        public async Task GetCertificateTest()
        {
            try
            {
                Certificate certificate = await _certificateService
                    .CertificateGetAsync(_certificateRequestOptons.Value.certificateName);

                Assert.AreEqual(_certificateRequestOptons.Value.certificateName, certificate?.certificateName);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.Fail();
            }
        }
    }
}
