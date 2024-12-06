#nullable disable

using Microsoft.Extensions.Options;

namespace RCL.SSL.SDK.Test
{
    [TestClass]
    public class CertificateServiceTest
    {
        private readonly IAzureAccessTokenService _azureAccessTokenService;
        private readonly ICertificateService _certificateService;
        private readonly IOptions<Certificate> _certificateRequestOptons;

        private Certificate _certificateTest = new Certificate
        {
            certificateName = "shopeneur.com",
            rootDomain = "shopeneur.com",
            email = "rcl@mail.com",
            password = "password123"
        };

        public CertificateServiceTest()
        {
            _azureAccessTokenService = (IAzureAccessTokenService)DependencyResolver.ServiceProvider()
                .GetService(typeof(IAzureAccessTokenService));

            _certificateService = (ICertificateService)DependencyResolver
                .ServiceProvider().GetService(typeof(ICertificateService));

            _certificateRequestOptons = (IOptions<Certificate>)DependencyResolver.ServiceProvider()
                .GetService(typeof(IOptions<Certificate>));
        }

        [TestMethod]
        public async Task GetAzureAccessTokenTest()
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

        [TestMethod]
        public async Task CreateCertificateOrderTest()
        {
            try
            {
                _certificateTest.target = RCLSSLAPIConstants.targetStandAlone;
                _certificateTest.challengeType = RCLSSLAPIConstants.dnsChallenge;
              //  _certificateTest.challengeType = RCLSSLConstants.httpChallenge;
                _certificateTest.isSAN = false;
              //  _certificateTest.isSAN = true;

                Certificate certificate = await _certificateService
                    .CertificateCreateOrderAsync(_certificateTest);

                Assert.AreEqual(_certificateRequestOptons.Value.certificateName, certificate?.certificateName);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ScheduleCertificateCreateStandAloneTest()
        {
            try
            {
                _certificateTest.target = RCLSSLAPIConstants.targetStandAlone;
                _certificateTest.challengeType = RCLSSLAPIConstants.dnsChallenge;
                //  _certificateTest.challengeType = RCLSSLConstants.httpChallenge;
                _certificateTest.isSAN = false;
                //  _certificateTest.isSAN = true;
                _certificateTest.orderUri = "https://acme-staging-v02.api.letsencrypt.org/acme/order/135518893/21065245264";

                await _certificateService.CertificateScheduleCreateAsync(_certificateTest);

                Assert.AreEqual(1,1);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ScheduleCertificateCreateAzureDNSTest()
        {
            try
            {
                _certificateTest.target = RCLSSLAPIConstants.targetAzureDNS;
                _certificateTest.challengeType = RCLSSLAPIConstants.dnsChallenge;
                _certificateTest.isSAN = false;
                //  _certificateTest.isSAN = true;
                _certificateTest.azureSubscriptionId = _certificateRequestOptons.Value.azureSubscriptionId;
                _certificateTest.dnsZoneResourceGroup = _certificateRequestOptons.Value.dnsZoneResourceGroup;
                _certificateTest.accessToken = await GetAzureAccessTokenAsync(RCLSSLAPIConstants.azureResource);

                await _certificateService.CertificateScheduleCreateAsync(_certificateTest);

                Assert.AreEqual(1, 1);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task DeleteCertificateTest()
        {
            try
            {
                await _certificateService
                    .CertificateDeleteAsync(_certificateTest.certificateName);

                Assert.AreEqual(1,1);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.Fail();
            }
        }

        private async Task<string> GetAzureAccessTokenAsync(string resource)
        {
            string _accessToken = string.Empty;

            try
            {
                var token = await _azureAccessTokenService.GetTokenAsync(resource);

                _accessToken = token?.access_token ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _accessToken;
        }
    }
}
