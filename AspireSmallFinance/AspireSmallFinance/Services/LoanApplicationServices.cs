using AspireSmallFinance.Constants;
using AspireSmallFinance.Models.DataAccess;
using AspireSmallFinance.Models.Entities;
using AspireSmallFinance.Models.Request;
using AspireSmallFinance.Models.Response;
using AspireSmallFinance.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static AspireSmallFinance.Utilities.DateUtils;
using static AspireSmallFinance.Utilities.MathUtils;

namespace AspireSmallFinance.Services
{
    public class LoanApplicationServices
    {
        private readonly IApplicationDBContext _dbContext;
        public LoanApplicationServices(IApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Approve(int id)
        {
            try
            {
                LoanApplications applications = _dbContext.LoanApplications.Single(idxApp => idxApp.LoanApplicationSysId == id);
                applications.IsApprovedFlag = true;
                _dbContext.LoanApplications.Attach(applications);
                _dbContext.SaveChangesDB();
                    
            }
            catch
            {
                throw;
            }
        }

        public int Insert(NewApplicationRequest application, int UserSysId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                { 
                LoanApplications newApplication = new LoanApplications()
                {
                    LoanAmount = application.LoanAmount,
                    IsApprovedFlag = false,
                    IsClosedFlag = false,
                    EndDate = Convert.ToDateTime(application.EndDate),
                    StartDate = Convert.ToDateTime(application.StartDate),
                    UserSysId = UserSysId
                };
                    _dbContext.LoanApplications.Attach(newApplication);
                    _dbContext.SaveChangesDB();

                    List<Payments> payments = new List<Payments>();
                    var paymentDateBucket = GetWeeklyBucketsForDateRange(newApplication.StartDate, newApplication.EndDate);
                    var paymentAmountBucket = SplitAmount(newApplication.LoanAmount, paymentDateBucket.Count);

                    for (int idx = 0; idx < paymentAmountBucket.Count; idx++)
                    {
                        payments.Add(new Payments()
                        {
                            DueDate = paymentDateBucket[idx],
                            IsSettledFlag = false,
                            LoanApplicationSysId = newApplication.LoanApplicationSysId,
                            PaymentAmount = paymentAmountBucket[idx],
                            PaymentDone = 0
                        });
                    }

                    _dbContext.Payments.AttachRange(payments);
                    _dbContext.SaveChangesDB();

                    scope.Complete();
                    return newApplication.LoanApplicationSysId;
                }
            }
            catch (SqlException sqlEx)
            {
                    throw new Exception($"Error while database operations. {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to perform required action. {ex.Message}");
            }

        }

        public LoanDetail Read(int id, int userSysId)
        {
            LoanDetail response = new LoanDetail();
            var applications = _dbContext.LoanApplications.ToList();
            var users = _dbContext.Users.ToList();
            var payments = _dbContext.Payments.ToList();

            if (applications != null && users != null)
            {
                if(userSysId != 0)
                {
                    users.RemoveAll(idx => idx.UserSysId != userSysId);
                }

                applications.Where(app => app.LoanApplicationSysId == id).Join(users, app => app.UserSysId, usr => usr.UserSysId, (app, usr) => new { app, usr })
                    .ToList().ForEach(idx =>
                    {
                        response = new LoanDetail()
                        {
                            ApplicationId = idx.app.LoanApplicationSysId,
                            ApplicationStatus = idx.app.IsApprovedFlag ? EnStatus.STATUS_APPROVED.GetDescription() : EnStatus.STATUS_PENDING.GetDescription(),
                            LoanStatus = idx.app.IsClosedFlag ? EnStatus.STATUS_CLOSED.GetDescription() : EnStatus.STATUS_RUNNING.GetDescription(),
                            ApplicantName = idx.usr.UserFullName,
                            StartDate = idx.app.StartDate.Date.GetISODateString(),
                            EndDate = idx.app.EndDate.Date.GetISODateString(),
                            LoanAmount = idx.app.LoanAmount,
                        };

                        var lstPayments = payments.Where(idxPmt => idxPmt.LoanApplicationSysId == id)?.ToList();
                        response.TotalInstallments = lstPayments.Count;
                        response.RemainingInstallments = lstPayments.Count(idxPmt => idxPmt.IsSettledFlag == false);
                        response.NextDueDate = response.RemainingInstallments == 0 ? null : lstPayments.Where(idxPmt => !idxPmt.IsSettledFlag).OrderBy(pmt => pmt.DueDate).FirstOrDefault().DueDate.Value.GetISODateString();
                        response.Balance = response.RemainingInstallments == 0 ? 0 : lstPayments.Where(idxPmt => !idxPmt.IsSettledFlag).Sum(pmt => pmt.PaymentAmount);

                        lstPayments.ForEach(idxPmt =>
                        {
                            response.Payments?.Add(new Payment()
                            {
                                DueDate = idxPmt.DueDate?.GetISODateString(),
                                PaymentDone = idxPmt.PaymentDone,
                                IsSettledFlag = idxPmt.IsSettledFlag,
                                PaymentAmount = idxPmt.PaymentAmount,
                                PaymentSysId = idxPmt.PaymentSysId
                            });
                        });
                    });
            }

            return response;
        }

        public List<LoanApplication> ReadList(int userSysId)
        {
            List<LoanApplication> response = new List<LoanApplication>();

            var applications = _dbContext.LoanApplications.ToList();
            var users = _dbContext.Users.ToList();
            var payments = _dbContext.Payments.ToList();
                        
            if (applications != null && users != null && payments != null)
            {
                if (userSysId != 0)
                {
                    users.RemoveAll(idx => idx.UserSysId != userSysId);
                }

                applications.Join(users, app => app.UserSysId, usr => usr.UserSysId, (app, usr) => new { app, usr })
                    .ToList().ForEach(idx =>
                    {
                        var tempApplication = new LoanApplication()
                        {
                            ApplicationId = idx.app.LoanApplicationSysId,
                            ApplicationStatus = idx.app.IsApprovedFlag ? EnStatus.STATUS_APPROVED.GetDescription() : EnStatus.STATUS_PENDING.GetDescription(),
                            LoanStatus = idx.app.IsClosedFlag ? EnStatus.STATUS_CLOSED.GetDescription() : EnStatus.STATUS_RUNNING.GetDescription(),
                            ApplicantName = idx.usr.UserFullName,
                            StartDate = idx.app.StartDate.Date.GetISODateString(),
                            EndDate = idx.app.EndDate.Date.GetISODateString(),
                            LoanAmount = idx.app.LoanAmount,
                           
                        };

                        var lstPayments = payments.Where(idxPmt => idxPmt.LoanApplicationSysId == tempApplication.ApplicationId).ToList();

                        tempApplication.TotalInstallments = lstPayments.Count;
                        tempApplication.RemainingInstallments = lstPayments.Count(idxPmt => idxPmt.IsSettledFlag == false);
                        tempApplication.NextDueDate = tempApplication.RemainingInstallments == 0 ? null : lstPayments.Where(idxPmt => !idxPmt.IsSettledFlag).OrderBy(pmt => pmt.DueDate).FirstOrDefault().DueDate.Value.GetISODateString();
                        tempApplication.Balance = tempApplication.RemainingInstallments == 0 ? 0 : lstPayments.Where(idxPmt => !idxPmt.IsSettledFlag).Sum(pmt => pmt.PaymentAmount);


                        response.Add(tempApplication);

                    });

            }

            return response;
        }
    }
}
