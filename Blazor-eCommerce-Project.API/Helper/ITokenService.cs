using Microsoft.AspNetCore.Identity;

namespace Blazor_eCommerce_Project.API.Helper
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user); 
    }
}
