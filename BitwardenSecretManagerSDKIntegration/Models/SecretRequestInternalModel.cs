
using Bitwarden.Sdk;

namespace BitwardenSecretManagerSDKIntegration.Models
{
    public class SecretRequestInternalModel : SecretRequestModel
    {
        public SecretIdentifierResponse Secret { get; set; }

        public SecretRequestInternalModel(Guid organizationId, string secretKey, string projectName) : base(organizationId, secretKey, projectName)
        {
        }

        public SecretRequestInternalModel(SecretRequestModel model) : base(model.OrganizationId, model.SecretKey, model.ProjectName)
        { 
        }
    }
}
