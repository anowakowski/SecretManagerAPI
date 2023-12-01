using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretManagerAPI.Management.Common.Interfaces;
using SecretManagerAPI.Models;

namespace SecretManagerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecretController : Controller
    {
        private readonly ISecretsManagerService _secretsManagerService;

        public SecretController(ISecretsManagerService secretsManagerService)
        {
            _secretsManagerService = secretsManagerService;
        }

        [HttpGet("GetSecretValue", Name = nameof(GetSecretValue))]
        public IActionResult GetSecretValue(SecretRequestModel model)
        {
            var result = _secretsManagerService.GetSecretValue(model.AccessToken, model.OrganizationId, model.SecretKey, model.ProjectName);

            return Ok(result);
        }
    }
}
