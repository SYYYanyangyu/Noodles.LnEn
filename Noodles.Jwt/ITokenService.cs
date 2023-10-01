using System.Collections.Generic;
using System.Security.Claims;

namespace Noodles.Jwt
{
    public interface ITokenService
    {
        string BuildToken(IEnumerable<Claim> claims, JwtOptions options);
    }
}
