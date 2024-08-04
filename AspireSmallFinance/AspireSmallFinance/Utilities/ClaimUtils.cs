using System.Net;
using System.Security.Claims;

namespace AspireSmallFinance.Utilities
{
    public static class ClaimUtils
    {
        public static string GetClaimValue(string claimType, IEnumerable<Claim> claims)
        {
            if(claims == null || !claims.Any(idx => idx.Type == claimType))
            {
                throw new WebException("No claims found. Authentication failure.");
            }

            var claim = claims.Single(idx => idx.Type == claimType);
            return claim.Value;
            
        }
    }
}
