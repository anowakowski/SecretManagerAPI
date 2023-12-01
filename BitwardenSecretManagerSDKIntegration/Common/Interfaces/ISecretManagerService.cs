using BitwardenSecretManagerSDKIntegration.Models;

namespace BitwardenSecretManagerSDKIntegration.Common.Interfaces
{
    public interface ISecretManagerService
    {
        (bool isAuthenticated, string message) AuthorizeInBitwardenSecureManager(string accessToken);
        SecretModel GetSecret(SecretRequestModel model);

        void Dispose();
    }
}
