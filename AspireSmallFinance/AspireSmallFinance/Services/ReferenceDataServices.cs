using AspireSmallFinance.Models.Entities;
using AspireSmallFinance.Models.Response;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Transactions;
using AspireSmallFinance.Models.DataAccess;
using Microsoft.AspNetCore.Builder;
using static AspireSmallFinance.Utilities.DateUtils;
using static AspireSmallFinance.Utilities.MathUtils;
using System;

namespace AspireSmallFinance.Services
{
    public class ReferenceDataServices
    {


        private readonly IApplicationDBContext _dbContext;
        public ReferenceDataServices(IApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Users> GetAllUsers()
        {
            return _dbContext.Users.ToList();

        }
    }
}
