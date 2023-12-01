namespace SecretManagerAPI.Management.Common.Interfaces
{
    public interface ISecretsManagerService
    {
        string GetSecretValue(string accessToken, Guid organizationId, string secretKey, string projectName);
    }
}
