namespace SecretManagerAPI.Management.DTOs
{
    public class SecretResponseDTO
    {
        public string SecretValue { get; private set; }
        public string ErrorMessage { get; private set; }
        public bool IsError { get; private set; }

        public SecretResponseDTO(string secretValue)
        {
            SecretValue = secretValue;
        }

        public SecretResponseDTO(string errorMessage, bool isError) 
        {
            ErrorMessage = errorMessage;
            IsError = isError;
        }
    }
}
