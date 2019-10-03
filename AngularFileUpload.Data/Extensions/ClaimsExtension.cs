using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AngularFileUpload.Data.Extensions
{
    public static class ClaimsExtension
    {
        public static string GetUserId(this IEnumerable<Claim> claims)
        {
            return claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();
        }
    }
}
