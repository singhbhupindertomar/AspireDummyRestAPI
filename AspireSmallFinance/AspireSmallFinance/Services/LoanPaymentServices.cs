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
    public class LoanPaymentServices 
    {


        private readonly IApplicationDBContext _dbContext;
        public LoanPaymentServices(IApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InsertUpdatePayment(int loanApplicationId, decimal amount)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var payments = _dbContext.Payments.ToListAsync().Result.Where(idx => idx.LoanApplicationSysId == loanApplicationId);
                    var application = _dbContext.LoanApplications.ToListAsync().Result.First(idx => idx.LoanApplicationSysId == loanApplicationId);
                    
                    AdjustPayments(payments.ToList(), application, amount);

                    _dbContext.Payments.AttachRange(payments);
                    _dbContext.SaveChangesDB();

                    if (!payments.Any(idx => !idx.IsSettledFlag))
                    {
                        application.IsClosedFlag = true;
                        _dbContext.LoanApplications.Attach(application);
                        _dbContext.SaveChangesDB();

                    }

                    scope.Complete();
                }

            }
            catch
            {
                throw;
            }
        }

        private List<Payments> AdjustPayments(List<Payments> payments, LoanApplications application, decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException($"Entered amount can't be less than or equal to 0.");
            }

            DateTime lastSettleDate = DateTime.MinValue;
            decimal amountSettled = payments.Where(idx => idx.IsSettledFlag).Sum(idx => idx.PaymentDone);
            decimal balanceAmount = application.LoanAmount - amountSettled;

            if (amount > balanceAmount)
            {
                throw new ArgumentException($"Payment {amount} can't be greater than balance amount {balanceAmount}.");
            }
            else if (amount == balanceAmount)
            {
                for (int idx = 0; idx < payments.Count(); idx++)
                {
                    if (payments[idx].IsSettledFlag)
                    {
                        continue;
                    }

                    payments[idx].PaymentDone = amount;
                    amount = 0;
                    payments[idx].IsSettledFlag = true;
                }
            }
            else
            {
                for (int idx = 0; idx < payments.Count(); idx++)
                {
                    lastSettleDate = payments[idx].DueDate.Value;

                    if (!payments[idx].IsSettledFlag)
                    {
                        payments[idx].PaymentDone = amount;
                        payments[idx].IsSettledFlag = true;

                        break;
                    }
                }

                amountSettled = payments.Where(idx => idx.IsSettledFlag).Sum(idx => idx.PaymentDone);
                balanceAmount = application.LoanAmount - amountSettled;

                var paymentDateBucket = GetWeeklyBucketsForDateRange(lastSettleDate, payments.Last().DueDate.Value);
                var paymentAmountBucket = SplitAmount(balanceAmount, paymentDateBucket.Count);



                payments.ForEach(idx =>
                {
                    if (!idx.IsSettledFlag)
                    {
                        var dueDate = idx.DueDate.Value;
                        int index = paymentDateBucket.IndexOf(dueDate);

                        if (index >= 0)
                        {
                            idx.PaymentAmount = paymentAmountBucket[index];
                        }

                    }
                });
            }
            return payments;

        }
    }
}
