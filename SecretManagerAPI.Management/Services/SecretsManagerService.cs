using SecretManagerAPI.Management.Common.Interfaces;
using BitwardenSecretManagerSDKIntegration.Services;
using SecretManagerAPI.Management.DTOs;
using BitwardenSecretManagerSDKIntegration.Models;

namespace SecretManagerAPI.Management.Services
{
    public class SecretsManagerService : ISecretsManagerService
    {
        public SecretsManagerService() { }

        public SecretResponseDTO GetSecretValue(string accessToken, Guid organizationId, string secretKey, string projectName)
        {
            var secretValue = string.Empty;
            using(var bwSecretManager = new SecretManagerService())
            {
                var result = bwSecretManager.AuthorizeInBitwardenSecureManager(accessToken);
                if (!result.isAuthenticated)
                {
                    var message = result.message;
                    return new SecretResponseDTO(message, true);
                }

                try
                {
                    var secretModel = bwSecretManager.GetSecret(new SecretRequestModel(organizationId, secretKey, projectName));
                    secretValue = secretModel.Value;
                }
                catch(Exception ex)
                {
                    return new SecretResponseDTO(ex.Message, true);
                }
            }

            return new SecretResponseDTO(secretValue);
        }
    }
}
