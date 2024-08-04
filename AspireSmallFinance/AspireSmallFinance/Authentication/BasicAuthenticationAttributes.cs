using Microsoft.AspNetCore.Authorization;

namespace AspireSmallFinance.Authentication
{
    public class BasicAuthenticationAttributes : AuthorizeAttribute
    {
        public BasicAuthenticationAttributes() {
            AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme;
        }
    }
}
