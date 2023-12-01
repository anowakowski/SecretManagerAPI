namespace BitwardenSecretManagerSDKIntegration.Exceptions
{
    internal class SecretNotExistsException : Exception
    {
        public SecretNotExistsException(string message)
            : base(message)
        {
        }

        public SecretNotExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
