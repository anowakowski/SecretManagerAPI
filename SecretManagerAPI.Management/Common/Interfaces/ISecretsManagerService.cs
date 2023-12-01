using SecretManagerAPI.Management.DTOs;

namespace SecretManagerAPI.Management.Common.Interfaces
{
    public interface ISecretsManagerService
    {
        SecretResponseDTO GetSecretValue(string accessToken, Guid organizationId, string secretKey, string projectName);
    }
}
