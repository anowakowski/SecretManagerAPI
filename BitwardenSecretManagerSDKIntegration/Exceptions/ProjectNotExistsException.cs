namespace BitwardenSecretManagerSDKIntegration.Exceptions
{
    public class ProjectNotExistsException : Exception
    {
        public ProjectNotExistsException(string message)
            : base(message)
        {
        }

        public ProjectNotExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
