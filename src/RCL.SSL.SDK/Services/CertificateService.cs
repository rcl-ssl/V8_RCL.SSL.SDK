using Microsoft.Extensions.Options;

namespace RCL.SSL.SDK
{
    internal class CertificateService : ApiRequestBase, ICertificateService
    {
        private readonly IOptions<ApiOptions> _apiOptions;

        public CertificateService(IOptions<ApiOptions> apiOptions)
        {
            _apiOptions = apiOptions;
        }

        public async Task<Certificate> CertificateGetAsync(string certificatename)
        {
            Certificate _certificate = new Certificate();

            try
            {
                SetClientHeaders(_apiOptions.Value.ApiKey);
                _certificate = await GetAsync<Certificate>($"{_apiOptions.Value.ApiBaseUrl}/ssl/certificate/subscription/{_apiOptions.Value.Subscription}/get/certificatename/{certificatename}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _certificate;
        }
    }
}
