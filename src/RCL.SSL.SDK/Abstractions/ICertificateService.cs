namespace RCL.SSL.SDK
{
    public interface ICertificateService
    {
        Task<Certificate> CertificateGetAsync(string certificatename);
        Task<Certificate> CertificateCreateOrderAsync(Certificate certificate);
        Task CertificateScheduleCreateAsync(Certificate certificate);
        Task CertificateScheduleRenewAsync(Certificate certificate);
        Task CertificateDeleteAsync(string certificatename);
    }
}
