using Bitwarden.Sdk;
using BitwardenSecretManagerSDKIntegration.Common.Interfaces;
using BitwardenSecretManagerSDKIntegration.Exceptions;
using BitwardenSecretManagerSDKIntegration.Extensions;
using BitwardenSecretManagerSDKIntegration.Models;

namespace BitwardenSecretManagerSDKIntegration.Services
{
    public class SecretManagerService : ISecretManagerService, IDisposable
    {
        BitwardenClient _bitwardenClient;
        bool _disposed = false;

        public (bool isAuthenticated, string message) AuthorizeInBitwardenSecureManager(string accessToken)
        {
            try
            {
                _bitwardenClient = new BitwardenClient();
                _bitwardenClient.AccessTokenLogin(accessToken);
                _disposed = false;

                return (true, "successfully autencticated on bitwarden secret manager");
            }
            catch(BitwardenAuthException ex)
            {
                return (false, $"Problem with autenticate with bitwarden secure manager, error message: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Error during autenticate with bitwarden secure manager, error message: {ex.Message}");
            }
        }

        public SecretModel GetSecret(SecretRequestModel model)
        {
            var modelInternal = Create(model);

            modelInternal
                .UserIsAuthorizedOnBWSecretManagerValidation(_bitwardenClient, _disposed)
                .OrganizationExistsValidation(_bitwardenClient)
                .ProjectExistsValidation(_bitwardenClient)
                .GetSecretAndValidate(_bitwardenClient);

            var secretApikeyId = modelInternal.Secret.Id;
            var secretResponse = _bitwardenClient.Secrets.Get(secretApikeyId);

            return new SecretModel(secretResponse.Value);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _bitwardenClient.Dispose();
            }
        }

        private SecretRequestInternalModel Create(SecretRequestModel model) => new SecretRequestInternalModel(model);
    }
}
