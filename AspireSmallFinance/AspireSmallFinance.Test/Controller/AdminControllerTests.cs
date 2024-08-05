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

namespace AspireSmallFinance.Controllers
{
    [TestFixture(Author = "Bhupinder Singh", Description = "Test methods for AdminController")]
    public class AdminControllerTests
    {
        IApplicationDBContext _dbContext;
        AdminController _adminController;

        [SetUp]
        public void Setup() { 
            if(_dbContext == null)
            {
                _dbContext = DBContextLoader.GetMockedApplicationDBContext();
            }

            _adminController = new AdminController(null, _dbContext);
        }

        [Test(Author = "Bhupinder Singh", Description = "Test methods for Getting Users for Admin.")]
        public void TestListUsers()
        {
            var data = _adminController.ListUsers().Value;
            Assert.That(data, Is.Not.Null);
            Assert.That(data, Has.Count.EqualTo(3));
        }


        [Test(Author = "Bhupinder Singh", Description = "Test methods for Getting Loan Applications for Admin.")]
        public void TestListApplications()
        {
            var data = _adminController.List().Value;
            Assert.That(data, Is.Not.Null);
            Assert.That(data, Has.Count.EqualTo(2));

        }

        [Test(Author = "Bhupinder Singh", Description = "Test methods for Getting Users for Admin.")]
        public void TestLoadApplication()
        {
            var data = _adminController.Load(1).Value;
            Assert.That(data, Is.Not.Null);
            Assert.That(data.Payments, Has.Count.EqualTo(2));
        }

        [Test(Author = "Bhupinder Singh", Description = "Test methods for Getting Users for Admin.")]
        public void TestApproveApplications()
        {
            var data = _adminController.Approve(1).Value;
            Assert.That(data, Is.Not.Null);
            Assert.That(data.ApplicationStatus.Equals(EnStatus.STATUS_APPROVED.GetDescription()));

        }


    }
}
