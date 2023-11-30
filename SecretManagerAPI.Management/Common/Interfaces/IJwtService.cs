using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SecretManagerAPI.Management.Common.Interfaces
{
    public interface IJwtService
    {
        JwtSecurityToken GetToken(List<Claim> authClaims);
    }
}
