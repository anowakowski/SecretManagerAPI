namespace BitwardenSecretManagerSDKIntegration.Models
{
    public class SecretModel
    {
        public string Value { get; private set; }

        public SecretModel(string value)
        {
            Value = value;
        }
    }
}
