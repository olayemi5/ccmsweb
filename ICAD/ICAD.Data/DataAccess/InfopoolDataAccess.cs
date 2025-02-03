using ICAD.Data.DataContext;
using ICAD.Data.Extensions;
using ICAD.Data.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAD.Data.DataAccess
{
    public class InfopoolDataAccess
    {
        private readonly InfopoolContext _context;
        private bool disposed = false;

        public InfopoolDataAccess(InfopoolContext context)
        {
            _context = context;
        }

        public InfopoolDataAccess()
        {
            _context = new InfopoolContext();
        }

        public IEnumerable<AccountInfo> GetAccountLists(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine("Start fetching ICAD records from infopool for selected date (" + startDate.Date + " - " + endDate.Date + ")...");
            var query = "GetICADDetails @StartDate, @EndDate";

            var result = _context.Database.SqlQuery<AccountInfo>(query, new SqlParameter("@StartDate", startDate.ToString("dd-MMM-yyyy")), new SqlParameter("@EndDate", endDate.ToString("dd-MMM-yyyy")))
                .Skip(0)
                .Take(10000).ToList()
                ;

            Console.WriteLine("GetAccountLists query executed successfully");
            Console.WriteLine(result.Count() + " records was returned!");
            Console.WriteLine("---------------------------------------------------------------------");
            return result;
        }

        public IEnumerable<BvnData> GetBvnData(string customerId)
        {
            try
            {
                var bvns = new List<BvnData>();
                var constring = ConfigurationManager.ConnectionStrings["InfopoolDb"].ConnectionString;
                using (var cnn = new SqlConnection(constring))
                {
                    using (var SqCmd = new SqlCommand("GetBvnByCustomerId", cnn))
                    {
                        SqCmd.CommandType = CommandType.StoredProcedure;
                        SqCmd.Parameters.AddWithValue("@CustomerId", customerId);
                        cnn.Open();
                        using (var Rd = SqCmd.ExecuteReader())
                        {
                            if (Rd.HasRows)
                            {
                                while (Rd.Read())
                                {
                                    var item = new BvnData()
                                    {
                                        bvn = Rd["BiometricId"].ToString()
                                    };
                                    bvns.Add(item);
                                }
                            }
                        }
                    }
                    cnn.Close();
                }
                return bvns;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Source);
                Console.WriteLine("Error occured... Type exit to close the window");
                Console.WriteLine("---------------------------------------------------------------------");
                throw ex;
            }
        }


        public IEnumerable<Branch> GetBranchLists()
        {
            Console.WriteLine("Start fetching Branch records from infopool...");
            var query = "SELECT BranchName, Address as BranchLocation, SortCode as BranchCode FROM BranchSortCode";

            var result = _context.Database.SqlQuery<Branch>(query)
                .ToList();

            Console.WriteLine("GetBranchLists executed successfully");
            Console.WriteLine(result.Count() + " records was returned!");
            Console.WriteLine("---------------------------------------------------------------------");
            return result;
        }

        public IEnumerable<AccountInfo> GetIcadLists(DateTime startDate, DateTime endDate)
        {
            var accounts = GetAccountLists(startDate, endDate);
            var lists = new List<AccountInfo>();
            if (accounts.Count() > 0)
            {
                Console.WriteLine("Fetching BVN for the accounts...");
                foreach (var item in accounts)
                {
                    var list = item;
                    var bvns = GetBvnData(item.UniqueCustomerId);
                    list.bvns = bvns.ToList();
                    lists.Add(list);
                }
                Console.WriteLine("Account BVN was generated successfully");
                Console.WriteLine("---------------------------------------------------------------------");
            }
            //var me = lists.Where(x => x.bvns.Count() > 0).ToList();
            return lists;
        }

        private void Dummy()
        {
            //dump all former / removed useful code
            var query = "GetBvnByCustomerId@CustomerId";
            var result = _context.GetDynamicResult(query, new Dictionary<string, object> {
                //{ "@CustomerId", customerId },
                //{ "@BiometricId", 1 },
            }).ToList();
            //var result = _context.Database.CollectionFromSql.SqlQuery<dynamic>(query, new SqlParameter("@CustomerId", customerId)).ToList();
            var bvns = result.Select(x => new BvnData()
            {
                bvn = x.BiometricId
            });
            //return bvns;



            //var result = data.Select(x => new AccountInfo()
            //{
            //    AccountDesignation = x.AccountDesignation,
            //    AccountName = x.AccountName,
            //    AccountNumber = x.AccountNumber,
            //    AccountStatus = x.AccountStatus,
            //    AccountType = x.AccountType,
            //    BranchCode = x.BranchCode,
            //    Currency = x.Currency,
            //    DateOfBirth = x.DateOfBirth,
            //    DateOpened = x.DateOpened,
            //    Email = x.Email,
            //    OldAccountNumber = x.OldAccountNumber,
            //    RcNo = x.RcNo,
            //    PhoneNumber = x.PhoneNumber,
            //    UniqueCustomerId = x.UniqueCustomerId                
            //});
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
