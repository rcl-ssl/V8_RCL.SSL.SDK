namespace RCL.SSL.SDK
{
    public interface ICertificateService
    {
        Task<Certificate> CertificateGetAsync(string certificatename);
    }
}
