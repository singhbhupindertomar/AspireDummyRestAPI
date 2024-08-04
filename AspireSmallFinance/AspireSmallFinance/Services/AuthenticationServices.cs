using AspireSmallFinance.Models.DataAccess;
using AspireSmallFinance.Models.Entities;
using AspireSmallFinance.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace AspireSmallFinance.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IApplicationDBContext _dbContext;

        public AuthenticationServices() : this(new ApplicationDBContext())
        {
            
        }

        public AuthenticationServices(IApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Users> Login(string username, string password)
        {
            return await _dbContext.Users.SingleAsync(idx => string.Equals(idx.UserName.ToUpper(),username.ToUpper()) && password == "welcome@1");
        }
    }
}
