using AspireSmallFinance.Constants;
using AspireSmallFinance.Models.DataAccess;
using AspireSmallFinance.Models.Entities;
using AspireSmallFinance.Models.Response;
using AspireSmallFinance.Services;
using AspireSmallFinance.Tests.Common;
using AspireSmallFinance.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Constraints;
using System.Security.Claims;

namespace AspireSmallFinance.Controllers
{
    [TestFixture(Author = "Bhupinder Singh", Description = "Test methods for CustomerController")]
    public class CustomerControllerTests
    {
        IApplicationDBContext _dbContext;
        CustomerController _controller;

        [SetUp]
        public void Setup() { 
            if(_dbContext == null)
            {
                _dbContext = DBContextLoader.GetMockedApplicationDBContext();
            }

            _controller = new CustomerController(null, _dbContext);
            var fakeClaims = new List<Claim>()
            {
               new Claim(ClaimTypes.PrimarySid, "2")
            };

            var fakeIdentity = new ClaimsIdentity(fakeClaims, "FakeClaims");
            var fakeClaimsPrincipal = new ClaimsPrincipal(fakeIdentity);

            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = fakeClaimsPrincipal
            };

        }

        [Test(Author = "Bhupinder Singh", Description = "Test methods for Getting Loans for a user.")]
        public void TestList()
        {            
            var data = _controller.List().Value;
            Assert.That(data, Is.Not.Null);
            Assert.That(data, Has.Count.EqualTo(2));
        }


        [Test(Author = "Bhupinder Singh", Description = "Test methods for Getting Loan detail for a loan.")]
        public void TestLoad()
        {
            
            var data = _controller.Load(1).Value;
            Assert.That(data, Is.Not.Null);
            Assert.That(data.Payments, Has.Count.EqualTo(2));
        }


    }
}
