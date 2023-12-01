using System.ComponentModel.DataAnnotations;

namespace SecretManagerAPI.Models
{
    public class SecretRequestModel
    {
        [Required(ErrorMessage = $"{nameof(AccessToken)} is required")]
        public string AccessToken { get; set; }

        [Required(ErrorMessage = $"{nameof(OrganizationId)} is required")]
        public Guid OrganizationId { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string SecretKey { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string ProjectName { get; set; }


    }
}
