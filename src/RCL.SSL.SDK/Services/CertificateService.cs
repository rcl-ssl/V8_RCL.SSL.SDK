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
                SetClientHeaders(_apiOptions.Value.ApiKey, _apiOptions.Value.Source);
                _certificate = await GetAsync<Certificate>($"{_apiOptions.Value.ApiBaseUrl}/ssl/certificate/subscription/{_apiOptions.Value.Subscription}/get/certificatename/{certificatename}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _certificate;
        }

        public async Task<Certificate> CertificateCreateOrderAsync(Certificate certificate)
        {
            Certificate _certificate = new Certificate();

            try
            {
                SetClientHeaders(_apiOptions.Value.ApiKey, _apiOptions.Value.Source);
                _certificate = await PostAsync<Certificate,Certificate>($"{_apiOptions.Value.ApiBaseUrl}/ssl/certificate/subscription/{_apiOptions.Value.Subscription}/order/create", certificate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _certificate;
        }

        public async Task CertificateScheduleCreateAsync(Certificate certificate)
        {
            try
            {
                SetClientHeaders(_apiOptions.Value.ApiKey, _apiOptions.Value.Source);
                await PostAsync<Certificate>($"{_apiOptions.Value.ApiBaseUrl}/ssl/certificate/subscription/{_apiOptions.Value.Subscription}/schedule/create", certificate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CertificateDeleteAsync(string certificatename)
        {
            try
            {
                SetClientHeaders(_apiOptions.Value.ApiKey, _apiOptions.Value.Source);
                await DeleteAsync($"{_apiOptions.Value.ApiBaseUrl}/ssl/certificate/subscription/{_apiOptions.Value.Subscription}/delete/certificatename/{certificatename}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
