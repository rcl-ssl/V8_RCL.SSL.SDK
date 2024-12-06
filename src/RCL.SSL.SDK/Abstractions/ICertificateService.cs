namespace RCL.SSL.SDK
{
    public interface ICertificateService
    {
        Task<Certificate> CertificateGetAsync(string certificatename);
        Task<Certificate> CertificateCreateOrderAsync(Certificate certificate);
        Task CertificateDeleteAsync(string certificatename);
        Task CertificateScheduleCreateAsync(Certificate certificate);
    }
}
