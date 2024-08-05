using AspireSmallFinance.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using AspireSmallFinance.Models.DataAccess;
using Moq;
using System.Net.Sockets;

namespace AspireSmallFinance.Tests.Common
{
    public static class DBContextLoader
    {

        public static IApplicationDBContext GetMockedApplicationDBContext()
        {
            var mockContext = new Mock<IApplicationDBContext>();
            mockContext.Setup(c => c.Users).Returns(GetMockUsers());
            mockContext.Setup(c => c.LoanApplications).Returns(GetMockLoanApplcations());
            mockContext.Setup(c => c.Payments).Returns(GetMockPayments());
            return mockContext.Object;  
        }

        private static DbSet<Payments> GetMockPayments()
        {
            var data = new List<Payments>()
            {
                new Payments(){ PaymentSysId = 1,LoanApplicationSysId = 1, DueDate = new DateTime(2024,04,08), PaymentAmount = 5000, PaymentDone = 0, IsSettledFlag = false },
                new Payments(){ PaymentSysId = 2,LoanApplicationSysId = 1, DueDate = new DateTime(2024,04,15), PaymentAmount = 5000, PaymentDone = 0, IsSettledFlag = false },
                new Payments(){ PaymentSysId = 3,LoanApplicationSysId = 2, DueDate = new DateTime(2024,04,08), PaymentAmount = 5000, PaymentDone = 0, IsSettledFlag = false },
                new Payments(){ PaymentSysId = 4,LoanApplicationSysId = 3, DueDate = new DateTime(2024,04,15), PaymentAmount = 5000, PaymentDone = 0, IsSettledFlag = false }
            }.AsQueryable();

            return GetMockedDBSet<Payments>(data);
        }

        private static DbSet<LoanApplications> GetMockLoanApplcations()
        {
            var data = new List<LoanApplications>()
            {
                new LoanApplications(){ LoanApplicationSysId = 1, UserSysId = 2, StartDate = new DateTime(2024,04,01), EndDate = new DateTime(2024,04,15), LoanAmount = 10000, IsApprovedFlag = true, IsClosedFlag = false },
                new LoanApplications(){ LoanApplicationSysId = 2, UserSysId = 2, StartDate = new DateTime(2024,04,01), EndDate = new DateTime(2024,04,15), LoanAmount = 10000, IsApprovedFlag = false, IsClosedFlag = false }
            }.AsQueryable();

            return GetMockedDBSet<LoanApplications>(data);
        }

        private static DbSet<Users> GetMockUsers()
        {
            var data = new List<Users>()
            {
                new Users(){ UserSysId = 1, UserName = "WayneB", UserFullName = "Bruce Wayne", IsAdminFlag = true },
                new Users(){ UserSysId = 2, UserName = "MarcoP", UserFullName = "Paul Marco", IsAdminFlag = false },
                new Users(){ UserSysId = 3, UserName = "HudsonL", UserFullName = "Laura Hudson", IsAdminFlag = false }
            }.AsQueryable();

            return GetMockedDBSet<Users>(data);

        }

        private static DbSet<T> GetMockedDBSet<T>(IQueryable<T> data) where T: class
        {
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            return mockSet.Object;
        }


    }
}
