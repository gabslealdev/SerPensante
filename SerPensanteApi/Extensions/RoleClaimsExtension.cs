using System.Security.Claims;
using SerPensanteApi.Models;

namespace SerPensanteApi.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Role, user.Role.ToString())
        };
        return result;
    }
}