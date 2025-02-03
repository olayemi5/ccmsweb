using Fcmb.Utilities.Logger;
using ICAD.Data.DataContext;
using ICAD.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAD.Data.DataAccess
{
    public class FinacleDataAccess
    {
        private readonly FinacleContext _context;
        public FinacleDataAccess(FinacleContext context)
        {
            _context = context;
        }

        public FinacleDataAccess()
        {
            _context = new FinacleContext();
        }


        public decimal GetCustomerBalance(string accountNo)
        {
            try
            {
                string query = "select custom.nestlegetavailbal('" + accountNo + "') available_balance from dual";
                var reader = _context.RunQuery(query);
                decimal balance = 0;
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            balance = _context.GetOrdinalDecimalValue(reader, "available_balance");
                        }
                    }
                }
                reader.Close();
                return balance;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex.ToString());
                throw ex;
            }
        }

        public decimal GetAccountMinimumBalance(string accountNo)
        {
            try
            {
                string query = "select a.schm_code, b.ACCT_MIN_BALANCE from tbaadm.gam a, tbaadm.csp b where b.schm_code=a.schm_code and foracid = '" + accountNo + "' and rownum <2";
                var reader = _context.RunQuery(query);
                decimal minimBalance = 0;
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            minimBalance = _context.GetOrdinalDecimalValue(reader, "ACCT_MIN_BALANCE");
                        }
                    }
                }
                reader.Close();
                return minimBalance;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex.ToString());
                throw ex;
            }
        }

        public IEnumerable<Branch> GetBranches(string bankCode)
        {
            try
            {
                string query = "select bank_code, sortcode, name from custom.tbl_branch where bank_code ='" + bankCode + "'";
                var reader = _context.RunQuery(query);
                var list = new List<Branch>();
                if (reader != null)
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var model = new Branch()
                            {
                                //set properties
                                //BankCode = _context.GetOrdinalStringValue(reader, "bank_code"),
                                BranchCode = _context.GetOrdinalStringValue(reader, "sortcode"),
                                BranchName = _context.GetOrdinalStringValue(reader, "name"),
                            };
                            list.Add(model);
                        }
                    }
                }
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex.ToString());
                throw ex;
            }
        }

        //public CustomerFinacleDetail GetCustomerDetailByAccountNo(string accountNo)
        //{
        //    try
        //    {
        //        string query = "select a.cif_id, a.acct_name, a.foracid, a.schm_code, b.email, to_char(c.date_of_birth, 'DD-MM-YYYY')  date_of_birth, b.phonenolocalcode from tbaadm.gam a, crmuser.phoneemail b, tbaadm.cmg c where b.orgkey=a.cif_id and c.cif_id = a.cif_id and foracid = '" + accountNo + "' and rownum <2";

        //        //string query = "select a.cif_id, a.acct_name, a.foracid, a.schm_code, (select email from crmuser.phoneemail where orgkey=a.cif_id and rownum <2) email, to_char(c.date_of_birth, 'DD-MM-YYYY')  date_of_birth, b.phonenolocalcode from tbaadm.gam a, crmuser.phoneemail b, tbaadm.cmg c where b.orgkey=a.cif_id and c.cif_id = a.cif_id and foracid = '" + accountNo + "' and b.phonenolocalcode is not null and rownum <2";

        //        var reader = _context.RunQuery(query);

        //        if (reader != null)
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    var model = new CustomerFinacleDetail()
        //                    {
        //                        //set properties
        //                        AccountNumber = _context.GetOrdinalStringValue(reader, "foracid"),
        //                        CustomerName = _context.GetOrdinalStringValue(reader, "acct_name"),
        //                        CustomerId = _context.GetOrdinalStringValue(reader, "cif_id"),
        //                        Email = _context.GetOrdinalStringValue(reader, "email"),
        //                        PhoneNumber = _context.GetOrdinalStringValue(reader, "phonenolocalcode"),
        //                        DateOfBirth = _context.GetOrdinalDateValue(reader, "date_of_birth"),
        //                        SchemeCode = _context.GetOrdinalStringValue(reader, "schm_code"),
        //                    };
        //                    reader.Close();
        //                    return model;
        //                }
        //            }
        //        }
        //        reader.Close();
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.Log(ex.ToString());
        //        throw ex;
        //    }
        //}

        //public CustomerFinacleDetail GetCustomerDetailByPhoneNo(string phoneNo)
        //{
        //    try
        //    {
        //        string query = "select a.cif_id, a.acct_name, a.foracid, a.schm_code, b.email, (select to_char(cust_dob, 'DD-MM-YYYY') from crmuser.accounts where orgkey=a.cif_id) date_of_birth, b.phonenolocalcode from tbaadm.gam a, crmuser.phoneemail b where  preferredflag='Y' and   b.phonenolocalcode = '" + phoneNo + "' and b.orgkey=a.cif_id  and rownum <2";

        //        var reader = _context.RunQuery(query);

        //        if (reader != null)
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    var model = new CustomerFinacleDetail()
        //                    {
        //                        //set properties
        //                        AccountNumber = _context.GetOrdinalStringValue(reader, "foracid"),
        //                        CustomerName = _context.GetOrdinalStringValue(reader, "acct_name"),
        //                        CustomerId = _context.GetOrdinalStringValue(reader, "cif_id"),
        //                        Email = _context.GetOrdinalStringValue(reader, "email"),
        //                        PhoneNumber = _context.GetOrdinalStringValue(reader, "phonenolocalcode"),
        //                        DateOfBirth = _context.GetOrdinalDateValue(reader, "date_of_birth"),
        //                        SchemeCode = _context.GetOrdinalStringValue(reader, "schm_code"),
        //                    };
        //                    reader.Close();
        //                    return model;
        //                }
        //            }
        //        }
        //        reader.Close();
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.Log(ex.ToString());
        //        throw ex;
        //    }
        //}

        //public CustomerAccountDetail GetCustomerDetailByAccountNo(string accountNo)
        //{
        //    try
        //    {
        //        string query = "select a.cif_id, a.acct_name, a.foracid, b.email, to_char(c.date_of_birth, 'DD-MM-YYYY')  date_of_birth, b.phonenolocalcode from tbaadm.gam a, crmuser.phoneemail b, tbaadm.cmg c where b.orgkey=a.cif_id and c.cif_id = a.cif_id and foracid = '" + accountNo + "' and rownum <2";

        //        var reader = _context.RunQuery(query);

        //        if (reader != null)
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    var model = new CustomerAccountDetail()
        //                    {
        //                        //set properties
        //                        AccountNumber = _context.GetOrdinalStringValue(reader, "foracid"),
        //                        CustomerName = _context.GetOrdinalStringValue(reader, "acct_name"),
        //                        CustomerId = _context.GetOrdinalStringValue(reader, "cif_id"),
        //                        Email = _context.GetOrdinalStringValue(reader, "email"),
        //                        PhoneNumber = _context.GetOrdinalStringValue(reader, "phonenolocalcode"),
        //                        DateOfBirth = _context.GetOrdinalDateValue(reader, "date_of_birth"),
        //                    };
        //                    reader.Close();
        //                    return model;
        //                }
        //            }
        //        }
        //        reader.Close();
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.Log(ex.ToString());
        //        throw ex;
        //    }
        //}


        //public CustomerAccountDetail GetCustomerDetailByPhoneNo(string phoneNo)
        //{
        //    try
        //    {
        //        string query = "select a.cif_id, a.acct_name, a.foracid, b.email, (select to_char(cust_dob, 'DD-MM-YYYY') from crmuser.accounts where orgkey=a.cif_id) date_of_birth, b.phonenolocalcode from tbaadm.gam a, crmuser.phoneemail b where  preferredflag='Y' and   b.phonenolocalcode = '" + phoneNo + "' and b.orgkey=a.cif_id  and rownum <2";

        //        var reader = _context.RunQuery(query);

        //        if (reader != null)
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    var model = new CustomerAccountDetail()
        //                    {
        //                        //set properties
        //                        AccountNumber = _context.GetOrdinalStringValue(reader, "foracid"),
        //                        CustomerName = _context.GetOrdinalStringValue(reader, "acct_name"),
        //                        CustomerId = _context.GetOrdinalStringValue(reader, "cif_id"),
        //                        Email = _context.GetOrdinalStringValue(reader, "email"),
        //                        PhoneNumber = _context.GetOrdinalStringValue(reader, "phonenolocalcode"),
        //                        DateOfBirth = _context.GetOrdinalDateValue(reader, "date_of_birth"),
        //                    };
        //                    reader.Close();
        //                    return model;
        //                }
        //            }
        //        }
        //        reader.Close();
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.Log(ex.ToString());
        //        throw ex;
        //    }
        //}
        

        //public AccountStatus GetAccountStatus(string accountNo)
        //{
        //    try
        //    {
        //        var model = new AccountStatus();

        //        //check savings account first
        //        string query = "select a.cust_id, a.sol_id, b.sol_desc, a.acct_name, a.foracid, to_char(acct_status_date, 'DD-MM-YYYY') acct_status_date, acct_status, to_char(a.acct_opn_date, 'DD-MM-YYYY') acct_opn_date from tbaadm.gam a, tbaadm.sol b, crmuser.address c, tbaadm.smt m where a.sol_id = b.sol_id and a.foracid = '" + accountNo + "' and a.acid = m.acid and a.acct_cls_flg = 'N' and a.entity_cre_flg = 'Y' and c.addresscategory <> 'Swift' and c.PREFERREDADDRESS = 'Y' and a.acct_cls_flg = 'N' and a.del_flg = 'N' and rownum < 2";

        //        var reader = _context.RunQuery(query);

        //        if (reader != null)
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    //set properties for savings
        //                    model.CustomerId = _context.GetOrdinalStringValue(reader, "cust_id");
        //                    model.BranchCode = _context.GetOrdinalStringValue(reader, "sol_id");
        //                    model.BranchName = _context.GetOrdinalStringValue(reader, "sol_desc");
        //                    model.AccountNumber = _context.GetOrdinalStringValue(reader, "foracid");
        //                    model.CustomerName = _context.GetOrdinalStringValue(reader, "acct_name");
        //                    model.AcctountStatusDate = _context.GetOrdinalDateValue(reader, "acct_status_date");
        //                    model.AcctountStatus = _context.GetOrdinalStringValue(reader, "acct_status");
        //                    model.AcctountOpenDate = _context.GetOrdinalDateValue(reader, "acct_opn_date");
        //                    return model;
        //                }
        //            }
        //        }
        //        reader.Close();

        //        //then check the current account
        //        query = "select a.cust_id, a.sol_id, b.sol_desc, a.acct_name, a.foracid, to_char(acct_status_date, 'DD-MM-YYYY') acct_status_date, acct_status, to_char(a.acct_opn_date, 'DD-MM-YYYY') acct_opn_date from tbaadm.gam a, tbaadm.sol b, crmuser.address c, tbaadm.cam m where a.sol_id = b.sol_id and a.foracid = '" + accountNo + "' and a.acid = m.acid and a.acct_cls_flg = 'N' and a.entity_cre_flg = 'Y' and c.addresscategory <> 'Swift' and c.PREFERREDADDRESS = 'Y' and a.acct_cls_flg = 'N' and a.del_flg = 'N' and rownum < 2";

        //        reader = _context.RunQuery(query);

        //        if (reader != null)
        //        {
        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    //set properties for current
        //                    model.CustomerId = _context.GetOrdinalStringValue(reader, "cust_id");
        //                    model.BranchCode = _context.GetOrdinalStringValue(reader, "sol_id");
        //                    model.BranchName = _context.GetOrdinalStringValue(reader, "sol_desc");
        //                    model.AccountNumber = _context.GetOrdinalStringValue(reader, "foracid");
        //                    model.CustomerName = _context.GetOrdinalStringValue(reader, "acct_name");
        //                    model.AcctountStatusDate = _context.GetOrdinalDateValue(reader, "acct_status_date");
        //                    model.AcctountStatus = _context.GetOrdinalStringValue(reader, "acct_status");
        //                    model.AcctountOpenDate = _context.GetOrdinalDateValue(reader, "acct_opn_date");
        //                    return model;
        //                }
        //            }
        //        }
        //        reader.Close();
        //        return model;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogger.Log(ex.ToString());
        //        throw ex;
        //    }
        //}
    }
}
