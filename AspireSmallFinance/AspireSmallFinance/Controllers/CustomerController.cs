using AspireSmallFinance.Models.DataAccess;
using AspireSmallFinance.Models.Entities;
using AspireSmallFinance.Models.Request;
using AspireSmallFinance.Models.Response;
using AspireSmallFinance.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static AspireSmallFinance.Utilities.ClaimUtils;

namespace AspireSmallFinance.Controllers
{
    [Authorize]
    [ApiController]
    [Tags("Customer operations")]

    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IApplicationDBContext _context;
        public CustomerController(ILogger<CustomerController> logger, IApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("loans")]
        [EndpointDescription("List all the loan application for the logged in user.")]
        public async Task<List<LoanApplication>> List()
        {
            var loanApplicationServices = new LoanApplicationServices(_context);
            string userSysId = GetClaimValue(ClaimTypes.PrimarySid, User.Claims);
            return await loanApplicationServices.ReadListAsync(Convert.ToInt32(userSysId));
        }


        [HttpGet("loans/{id}")]
        [EndpointDescription("Load details of loan by id for the logged in user.")]
        public async Task<LoanDetail> Load(int id)
        {
            var loanApplicationServices = new LoanApplicationServices(_context);
            string userSysId = GetClaimValue(ClaimTypes.PrimarySid, User.Claims);
            return await loanApplicationServices.ReadAsync(id, Convert.ToInt32(userSysId));
        }



        [HttpPost("loans/new")]
        [EndpointDescription("Submit the request for new loan application.")]
        public async Task<LoanDetail> Submit([FromBody] NewApplicationRequest application)
        {
            var loanApplicationServices = new LoanApplicationServices(_context);
            string userSysId = GetClaimValue(ClaimTypes.PrimarySid, User.Claims);
            var dbResponse = loanApplicationServices.InsertAsync(application, Convert.ToInt32(userSysId));
            return await loanApplicationServices.ReadAsync(dbResponse, Convert.ToInt32(userSysId));
        }


        [HttpPost("loans/{id}/pay")]
        [EndpointDescription("Perform payments for the loan.")]
        public async Task<LoanDetail> MakePayment(int id, [FromQuery(Name = "amount")] double amount)
        {
            var paymentServices = new LoanPaymentServices(_context);
            var loanApplicationServices = new LoanApplicationServices(_context);
            string userSysId = GetClaimValue(ClaimTypes.PrimarySid, User.Claims);
            paymentServices.InsertUpdatePayment(id, Convert.ToDecimal(amount));
            return await loanApplicationServices.ReadAsync(id, Convert.ToInt32(userSysId));

        }
    }
}
