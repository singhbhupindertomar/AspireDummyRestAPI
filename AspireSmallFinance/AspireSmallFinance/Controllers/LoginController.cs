using AspireSmallFinance.Models.DataAccess;
using AspireSmallFinance.Models.Entities;
using AspireSmallFinance.Models.Request;
using AspireSmallFinance.Models.Response;
using AspireSmallFinance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspireSmallFinance.Controllers
{

    [AllowAnonymous]
    [ApiController]
    [Tags("Authentication")]
    public class LoginController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IApplicationDBContext _context;

        public LoginController(ILogger<AdminController> logger, IApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpPost("login")]
        [EndpointDescription("Log-in and return the user information to UI.")]
        public Task<Users> Login([FromBody] LoginRequest request)
        {
            var authenticationServices = new AuthenticationServices(_context);

            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            {
                throw new BadHttpRequestException("Missing username or password.");
            }
            return authenticationServices.Login(request.UserName, request.Password);
        }
    }
}
