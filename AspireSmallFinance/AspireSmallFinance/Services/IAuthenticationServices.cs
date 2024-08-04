using AspireSmallFinance.Models.DataAccess;
using AspireSmallFinance.Models.Entities;
using AspireSmallFinance.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace AspireSmallFinance.Services
{
    public interface IAuthenticationServices
    {
        Task<Users> Login(string username, string password);
    }
}
