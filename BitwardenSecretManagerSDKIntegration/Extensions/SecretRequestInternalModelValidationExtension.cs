using Bitwarden.Sdk;
using BitwardenSecretManagerSDKIntegration.Exceptions;
using BitwardenSecretManagerSDKIntegration.Models;

namespace BitwardenSecretManagerSDKIntegration.Extensions
{
    public static class SecretRequestInternalModelValidationExtension
    {
        public static SecretRequestInternalModel UserIsAuthorizedOnBWSecretManagerValidation(this SecretRequestInternalModel model, BitwardenClient bitwardenClient, bool disposed)
        {
            if (disposed || bitwardenClient == null) throw new NotAuthorizedException("You are currentrly not authorized on bitwarden secret manager");

            return model;
        }

        public static SecretRequestInternalModel OrganizationExistsValidation(this SecretRequestInternalModel model, BitwardenClient bitwardenClient)
        {
            try
            {
                bitwardenClient.Projects.List(model.OrganizationId);
                return model;
            }
            catch (BitwardenException)
            {
                throw new OrganizationNotExistException($"Your organization: {model.OrganizationId} not exists");
            }
        }

        public static SecretRequestInternalModel ProjectExistsValidation(this SecretRequestInternalModel model, BitwardenClient bitwardenClient)
        {
            var projects = bitwardenClient.Projects.List(model.OrganizationId);
            var projectList = projects.Data.ToList();

            var foundedProject = projectList.FirstOrDefault(x => x.Name == model.ProjectName);

            if (foundedProject == null) throw new ProjectNotExistsException($"project {model.ProjectName} not exists");

            return model;
        }

        public static SecretRequestInternalModel GetSecretAndValidate(this SecretRequestInternalModel model, BitwardenClient bitwardenClient)
        {
            var secrets = bitwardenClient.Secrets.List(model.OrganizationId);

            if (!secrets.Data.Any()) throw new SecretNotExistsException($"you currently havan't any secrets on your organization: {model.OrganizationId}");

            var secret = secrets.Data.FirstOrDefault(x => x.Key == model.SecretKey);

            if (secret == null) throw new SecretNotExistsException($"your secret: {model.SecretKey} is not exist in your organization: {model.OrganizationId}");

            model.Secret = secret;

            return model;
        }
    }
}
