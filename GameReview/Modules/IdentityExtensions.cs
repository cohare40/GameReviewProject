using System.Security.Claims;
using System.Security.Principal;

namespace GameReview.Modules
{
    public static class IdentityExtensions
        {
            public static string GetUserFirstname(this IIdentity identity)
            {
                var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.GivenName);
                // Test for null to avoid issues during local testing
                return (claim != null) ? claim.Value : string.Empty;
            }

            public static string GetUserLastname(this IIdentity identity)
            {
                var claim = ((ClaimsIdentity)identity).FindFirst("LastName");
                // Test for null to avoid issues during local testing
                return (claim != null) ? claim.Value : string.Empty;
            }

            public static string GetUserProfilePic(this IIdentity identity)
            {
                var claim = ((ClaimsIdentity)identity).FindFirst("ProfilePic");
                // Test for null to avoid issues during local testing
                return (claim != null) ? claim.Value : string.Empty;
            }
    }
}