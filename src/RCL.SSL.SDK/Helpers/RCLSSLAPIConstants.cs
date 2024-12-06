namespace RCL.SSL.SDK
{
    public static class RCLSSLAPIConstants
    {
        public static string httpChallenge => "http";
        public static string dnsChallenge => "dns";
        public static string targetStandAlone => "Stand ALone";
        public static string targetAzureDNS => "Azure DNS";
        public static string targetAzureKeyVaultDNS => "Azure Key Vault + DNS";
        public static string targetAzureAppService => "Azure App Service";

        public static string azureResource => "https://management.core.windows.net";
        public static string keyVaultResource => "https://vault.azure.net";
    }
}
