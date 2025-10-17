using System.Security.Claims;
using CrossDSP.Domain.Exceptions;

namespace CrossDSP.Infrastructure.Helpers
{
    public static class ClaimPrincipleHelper
    {
        public static string GetClaimValueByName(
            this ClaimsPrincipal user,
            string claimName)
        {
            var identity = user.Identities.FirstOrDefault(identity => identity.Claims.Any(
                claim => claim.Type == claimName
            )) ?? 
            throw new AccessException(
                Domain.Enums.ExceptionCode.Forbidden,
                "Required Role not found."
            );

            return identity.Claims.First(claim => claim.Type == claimName).Value;
        }
    }
}