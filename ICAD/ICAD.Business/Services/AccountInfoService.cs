using ICAD.Core.IcardBLL;
using ICAD.Data.DataAccess;
using ICAD.Data.DataContext;
using ICAD.Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ICAD.Business.Services
{
    public class AccountInfoService
    {
        private readonly InfopoolDataAccess _infopoolDataAccess;
        private readonly IcadDataAccess _icadDataAccess;
        public AccountInfoService()
        {
            _infopoolDataAccess = new InfopoolDataAccess();
            _icadDataAccess = new IcadDataAccess();
        }

        public List<AccountInfo> GetAll()
        {
            Console.WriteLine("Get account list from ICAD records...");
            var model = _icadDataAccess.GetAll();
            var result = model.Select(x =>
            {
                //deserialize BvnString to List
                x.bvns = JsonConvert.DeserializeObject<List<BvnData>>(x.BvnString);
                return x;
            }).ToList();
            return result;
        }

        public List<AccountInfo> GetByDate(DateTime date)
        {
            Console.WriteLine("Get account list by date...");
            var result = GetAll().Where(x => x.Date.Date == date.Date).ToList();

            return result;
        }

        public List<AccountInfo> GetByDate(string bankCode, string institutionCode, DateTime date)
        {
            Console.WriteLine("Get account list by date...");
            var model = GetAll();
            var result = model.Where(x => x.Date.Date == date.Date)
                .Select(x =>
                {
                    x.InstitutionCode = institutionCode;
                    x.RequestID = GenerateRequestId(bankCode, date, x.Id.ToString());
                    return x;
                })
                .ToList();
            
            return result;
        }

        public AccountInfo GetById(int id)
        {
            Console.WriteLine("Get account detail by id (" + id + ")...");
            var model = _icadDataAccess.GetById(id);
            model.bvns = JsonConvert.DeserializeObject<List<BvnData>>(model.BvnString);
            return model;
        }

        public void Create(AccountInfo model)
        {
            if (model != null)
            {
                _icadDataAccess.Create(model);
            }
        }

        public void BulkInsert(IEnumerable<AccountInfo> list)
        {
            _icadDataAccess.BulkInsert(list);
        }

        public void Edit(int id, AccountInfo model)
        {
            if (model != null)
            {
                //Console.WriteLine("Updating records with Id " + id + "...");
                _icadDataAccess.Edit(id, model);
            }
        }

        public void UpdateServiceResponse(AccountInfo model, DateTime? requestTime, DateTime? responseTime, string responseMsg, bool status)
        {
            _icadDataAccess.UpdateServiceResponse(model, requestTime, responseTime, responseMsg, status);
        }

        private AccountInfo NameConverter(string name)
        {
            var humanName = new NameParser.HumanName(name);
            var model = new AccountInfo();
            model.FirstName = humanName.First;
            model.LastName = humanName.Last;
            model.MiddleName = humanName.Middle + " " + humanName.Suffix + " " + humanName.Nickname;
            return model;
        }

        public int GetAccountsFromInfopool(DateTime startDate, DateTime endDate)
        {
            var data = _infopoolDataAccess.GetIcadLists(startDate, endDate);
            if (data.Count() > 0)
            {
                var results = data.Select(x =>
                {
                    var name = NameConverter(x.AccountName);

                    x.FirstName = name.FirstName;
                    x.LastName = name.LastName;
                    x.MiddleName = name.MiddleName != null ? name.MiddleName : ""; //Regex.Replace(name.MiddleName != null ? name.MiddleName : "N/A", @"\s+", " ");
                    //convert bvn list to json string
                    x.BvnString = JsonConvert.SerializeObject(x.bvns);
                    //remove bvns from list to avoid database mapping error
                    x.bvns = null;
                    x.RequestID = x.Id.ToString();
                    x.Date = endDate;
                    x.DateOpened = DateTime.Parse(x.DateOpened != null ? x.DateOpened : DateTime.Now.ToString()).ToString("yyyyMMdd");
                    x.DateModified = DateTime.Parse(x.DateModified != null ? x.DateModified : DateTime.Now.ToString()).ToString("yyyyMMdd");
                    x.DateOfBirth = DateTime.Parse(x.DateOfBirth != null ? x.DateOfBirth : DateTime.Now.ToString()).ToString("yyyyMMdd");
                    //x.BranchCode = "214";
                    //remove default email
                    if (x.Email == "noemail@fcmb.com")
                    {
                        x.Email = null;
                    }
                    return x;
                }
                ).ToList();

                //insert to local table
                //Console.WriteLine("Creating new record...");
                //Create(results[0]);
                Console.WriteLine("Bulk insert records in progress...");
                BulkInsert(results);
                Console.WriteLine(results.Count() + " records was inserted!");
                Console.WriteLine("---------------------------------------------------------------------");
                return results.Count();
            };
            return 0;
        }

        public RequestObj ConvertToRequest(AccountInfo data, string institutionCode)
        {
            if (data != null)
            {
                var results = new RequestObj()
                {
                    accountDesignation = data.AccountDesignation,
                    accountName = data.AccountName,
                    accountNumber = data.AccountNumber,
                    accountStatus = data.AccountStatus,
                    accountType = data.AccountType,
                    //branchCode = branchCode,
                    bvns = (data.bvns != null) ? data.bvns.Select(
                        y => new bvnObj()
                        {
                            bvn = y.bvn
                        }).ToList() : new List<bvnObj>(),
                    currency = data.CurrencyCode,
                    dateModified = data.DateModified,
                    dateOfBirth = data.DateOfBirth,
                    dateOpened = data.DateOpened,
                    BranchCode = data.BranchCode,
                    email = data.Email,
                    firstName = data.FirstName,
                    institutionCode = institutionCode,
                    lastName = data.LastName,
                    middleName = data.MiddleName,
                    oldAccountNumber = data.OldAccountNumber,
                    phoneNumber = data.PhoneNumber,
                    phoneNumber2 = data.PhoneNumber2,
                    rcNo = data.RcNo,
                    requestID = data.RequestID,
                    uniqueCustomerId = data.UniqueCustomerId
                };
                return results;
            };
            return null;
        }

        public IEnumerable<RequestObj> ConvertListToRequest(IEnumerable<AccountInfo> list, string institutionCode)
        {
            if (list.Count() > 0)
            {
                var results = list.Select(x => new RequestObj()
                {
                    accountDesignation = x.AccountDesignation,
                    accountName = x.AccountName,
                    accountNumber = x.AccountNumber,
                    accountStatus = x.AccountStatus,
                    accountType = x.AccountType,
                    //branchCode = branchCode,
                    bvns = x.bvns.Select(
                        y => new bvnObj()
                        {
                            bvn = y.bvn
                        }).ToList(),
                    //currency = x.Currency,
                    currency = x.CurrencyCode,
                    dateModified = x.DateModified,
                    dateOfBirth = x.DateOfBirth,
                    dateOpened = x.DateOpened,
                    BranchCode = x.BranchCode,
                    email = x.Email,
                    firstName = x.FirstName,
                    institutionCode = institutionCode,
                    lastName = x.LastName,
                    middleName = x.MiddleName,
                    oldAccountNumber = x.OldAccountNumber,
                    phoneNumber = x.PhoneNumber,
                    phoneNumber2 = x.PhoneNumber2,
                    rcNo = x.RcNo,
                    requestID = x.RequestID,
                    uniqueCustomerId = x.UniqueCustomerId
                });
                return results;
            };
            return null;
        }

        private string GenerateRequestId(string bankCode, DateTime date, string recordId)
        {
            var requestCode = string.Format("{0}{1}{2}", bankCode.PadLeft(6, '0'), date.ToString("yyyyMMddHHmmss"), recordId.PadLeft(10, '0'));
            return requestCode;
        }

        public List<BranchObj> GetBankBranches(string bankCode, string institutionCode)
        {
            var branches = _infopoolDataAccess.GetBranchLists().Select(x => new BranchObj()
            {
                BranchCode = x.BranchCode,
                branchLocation = x.BranchLocation,
                branchName = x.BranchName,
                institutionCode = institutionCode,
                requestID = GenerateRequestId(bankCode, DateTime.Now, x.BranchCode)
            }).ToList();

            return branches;
        }
    }
}
