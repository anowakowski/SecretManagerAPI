using SecretManagerAPI.Management.Common.Interfaces;

namespace SecretManagerAPI.Management.Services
{
    public class SecretsManagerService : ISecretsManagerService
    {
        public SecretsManagerService() { }

        public string GetSecretValue(string accessToken)
        {
            using(var bwSecretManager = new BitwardenSecretManagerSDKIntegration.Services.SecretManagerService())
            {
                var result = bwSecretManager.AuthorizeInBitwardenSecureManager(accessToken);
                if (!result.isAuthenticated)
                {
                    var message = result.message;
                }
            }

            return string.Empty;
        }
    }
}
