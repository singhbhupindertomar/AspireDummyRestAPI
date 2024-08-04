using AspireSmallFinance.Models.DataAccess;
using AspireSmallFinance.Models.Response;
using AspireSmallFinance.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static AspireSmallFinance.Utilities.ClaimUtils;
using Microsoft.AspNetCore.Authorization;
using AspireSmallFinance.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace AspireSmallFinance.Controllers
{
    [Authorize]
    [ApiController]
    [Tags("Admin operations")]
    public class AdminController : ControllerBase
    {
        
        private readonly ILogger<AdminController> _logger;
        private readonly IApplicationDBContext _context;
        public AdminController(ILogger<AdminController> logger, IApplicationDBContext context)
        {
            _logger = logger;
           _context = context;
        }


        [HttpGet("users")]
        [EndpointDescription("Fetch the details of all the users in database to load users dropdown on UI.")]
        public async Task<List<Users>> ListUsers()
        {
            var referenceDataServices = new ReferenceDataServices(_context);
            return await referenceDataServices.GetAllUsers();
        }


        [HttpGet("applications")]
        [EndpointDescription("List all the loan applications currently in the system.")]

        public async Task<List<LoanApplication>> List()
        {
            var loanApplicationServices = new LoanApplicationServices(_context);
            return await loanApplicationServices.ReadListAsync(0);
        }

        [HttpGet("applications/{id}")]
        [EndpointDescription("Details of the loan application by id.")]
        public Task<LoanDetail> Load(int id)
        {
            var loanApplicationServices = new LoanApplicationServices(_context);
            return loanApplicationServices.ReadAsync(id, 0);
        }

       
        [HttpPost("applications/{id}/approve")]
        [EndpointDescription("Approve the pending loan application.")]
        public async Task<LoanDetail> Approve(int id)
        {
            var loanApplicationServices = new LoanApplicationServices(_context);
            await loanApplicationServices.ApproveAsync(id);
            return await loanApplicationServices.ReadAsync(id,  0);
        }
    }
}
