using SecretManagerAPI.Management.Common.Interfaces;
using BitwardenSecretManagerSDKIntegration.Services;

namespace SecretManagerAPI.Management.Services
{
    public class SecretsManagerService : ISecretsManagerService
    {
        public SecretsManagerService() { }

        public string GetSecretValue(string accessToken, Guid organizationId, string secretKey, string projectName)
        {
            var secretValue = string.Empty;
            using(var bwSecretManager = new SecretManagerService())
            {
                var result = bwSecretManager.AuthorizeInBitwardenSecureManager(accessToken);
                if (!result.isAuthenticated)
                {
                    var message = result.message;

                }

                var secretModel = bwSecretManager.GetSecret(organizationId, secretKey, projectName);
                secretValue = secretModel.Value;
            }

            return secretValue;
        }
    }
}
