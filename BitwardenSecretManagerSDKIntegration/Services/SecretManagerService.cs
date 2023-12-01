using Bitwarden.Sdk;
using BitwardenSecretManagerSDKIntegration.Common.Interfaces;
using BitwardenSecretManagerSDKIntegration.Exceptions;
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

        public SecretModel GetSecret(Guid organizationId, string secretKey, string projectName)
        {
            if (!CheckIfUserIsAuthorizedOnBWSecretManager()) throw new NotAuthorizedException("You are currentrly not authorized on bitwarden secret manager");
            if (!CheckIfProjectExists(projectName, organizationId)) throw new ProjectNotExistsException($"project {projectName} not exists");

            var secrets = _bitwardenClient.Secrets.List(organizationId);

            if (!secrets.Data.Any()) throw new SecretNotExistsException($"you currently havan't any secrets on your organization: {organizationId}");

            var secret = secrets.Data.FirstOrDefault(x => x.Key == secretKey);

            if (secret == null) throw new SecretNotExistsException($"your secret: {secretKey} is not exist in your organization: {organizationId}");

            var secretApikeyId = secret.Id;
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

        private bool CheckIfUserIsAuthorizedOnBWSecretManager()
        {
            if (_disposed || _bitwardenClient == null) return false;

            return true;
        }

        private bool CheckIfProjectExists(string projectName, Guid organizationId)
        {
            var projects = _bitwardenClient.Projects.List(organizationId);
            var projectList = projects.Data.ToList();

            var foundedProject = projectList.FirstOrDefault(x => x.Name == projectName);

            return foundedProject != null;
        }
    }
}
