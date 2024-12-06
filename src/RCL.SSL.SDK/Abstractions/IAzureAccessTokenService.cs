namespace RCL.SSL.SDK
{
    public interface IAzureAccessTokenService
    {
       Task<AzureAccessToken> GetTokenAsync(string resource);
    }
}
