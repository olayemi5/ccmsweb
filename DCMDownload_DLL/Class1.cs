using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace DCMDownload_DLL
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using DCMDownload_DLL.Model;
using log4net;
using log4net.Config;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using NLog;
using System.Threading.Tasks;



using Oracle.ManagedDataAccess.Client; // ODP.NET Oracle managed provider
using Oracle.ManagedDataAccess.Types;


namespace DCMDownload_DLL
{
    public class Class1
    {


        private static Logger _logger = NLog.LogManager.GetCurrentClassLogger();


        public static void Main(String[] args)
        {

            BasicConfigurator.Configure();
            try
            {
                _logger.Info("Started.");
                int count = ProcessRequest("DCM");
                //Console.WriteLine(count);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }
        public static int ProcessRequest(String reportType)
        {
            BasicConfigurator.Configure();
            int count = 0;
            ArrayList DCMRequestList = DCMDetails(reportType);

            if (DCMRequestList != null)
            {
                //count = InsertIntoCCMS_ccms_comp_detl(DCMRequestList);
                count = InsertIntoCCMS_ccms_comp_detail2(DCMRequestList);
            }
            return count;
        }
        public static int ProcessRequest(DateTime calendar, String reportType)
        {
            BasicConfigurator.Configure();
            int count = 0;
            // ArrayList DCMRequestList = DCMDetails();
            ArrayList DCMRequestList = DCMDetails(calendar, reportType);

            if (DCMRequestList != null)
            {
                //count = InsertIntoCCMS_ccms_comp_detl(DCMRequestList);
                count = InsertIntoCCMS_ccms_comp_detail2(DCMRequestList);
            }
            return count;
        }
        public void test()
        {

        }
        private static DayOfWeek ValidateWeekday()
        {
            DateTime date = DateTime.Now;

            string dateToday = date.ToString("d");
            DayOfWeek day = DateTime.Now.DayOfWeek;
            string dayToday = day.ToString();

            // compare enums
            if (day == DayOfWeek.Monday)
            {
                //Console.WriteLine(dateToday + " is a Monday");
            }
            return day;
        }
        private static ArrayList DCMDetails(String reportType)
        {
            try
            {
                SqlConnection con = DBConnection.DCMSource();
                DayOfWeek weekDay = ValidateWeekday();
                SqlCommand command = null;
                //if (weekDay.ToString() == "Monday")
                //{
                //    if (reportType == "DCM")
                //    {
                //        command = new SqlCommand("Select  * from [ccms_comp_detail_DCM] where " +
                //       " downloaddate >= DATEADD(day, -3, convert(date, GETDATE())) and downloaddate < convert(date, GETDATE())"
                //       , con);
                //    }
                //    else if (reportType == "Profectus")
                //    {
                //        command = new SqlCommand("Select  * from [ccms_comp_detail_Profectus] where " +
                //       " downloaddate >= DATEADD(day, -3, convert(date, GETDATE())) and downloaddate < convert(date, GETDATE())"
                //       , con);
                //    }
                //    else if (reportType == "Integrify")
                //    {
                //        command = new SqlCommand("Select  * from [ccms_comp_detail_Integrify] where " +
                //      " downloaddate >= DATEADD(day, -3, convert(date, GETDATE())) and downloaddate < convert(date, GETDATE())"
                //      , con);
                //    }

                //}
                //else
                //{
                    if (reportType == "DCM")
                    {
                        command = new SqlCommand("Select  * from [ccms_comp_detail2] where " +
                        " downloaddate >= DATEADD(day, -1, convert(date, GETDATE())) and downloaddate < convert(date, GETDATE())"
                        , con);
                    }
                    if (reportType == "Profectus")
                    {
                        command = new SqlCommand("Select  * from [ccms_comp_detail_Profectus] where " +
                        " downloaddate >= DATEADD(day, -1, convert(date, GETDATE())) and downloaddate < convert(date, GETDATE())"
                        , con);
                    }
                    else if (reportType == "Integrify")
                    {
                        command = new SqlCommand("Select  * from [ccms_comp_detailIntegrify] where " +
                          " downloaddate >= DATEADD(day, -1, convert(date, GETDATE())) and downloaddate < convert(date, GETDATE())"
                          , con);
                    }
                    else if (reportType == "CRM_360")
                    {
                        command = new SqlCommand("Select  * from [ccms_comp_detail_CRM360] where " +
                              " downloaddate >= DATEADD(day, -1, convert(date, GETDATE())) and downloaddate < convert(date, GETDATE())"
                              , con);
                 
                    }

                // }
                //SqlCommand command = new SqlCommand("Proc_ccms_comp_detail2", con);
                //command.CommandType = System.Data.CommandType.StoredProcedure;
                string[] accttype = { "SAVINGS", "CURRENT", "LOAN", "DOMICILIARY", "OTHERS" };
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                var requestList = new ArrayList();
                ClsRequestData requestData = null;
                int count = 0;

                //Category cat = JsonConvert.DeserializeObject<Category>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "deji.json"));
                Category cat = JsonConvert.DeserializeObject<Category>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "data.json"));
                while (reader.Read())
                {
                    requestData = new ClsRequestData();
                    requestData.tracknum = reader["tracknum"].ToString();
                    requestData.branch_name = reader["branch_name"].ToString();
                    requestData.UniqueIdentificationNumber = reader["UniqueIdentificationNumber"].ToString();
                    requestData.Preferred_contact_address = reader["Preferred_contact_address"].ToString();
                    requestData.Preferred_contact_Email = reader["Preferred_contact_Email"].ToString();
                    requestData.Preferred_contact_phone = reader["Preferred_contact_phone"].ToString();
                    requestData.Root_cause = reader["Root_cause"].ToString();

                    String descr = reader["bank_remark"].ToString();
                    if (descr == "" || descr == null)
                        requestData.bank_remark = "Others";
                    else if (descr.Contains("Thank you for your mail") || descr.Contains("Dear") || descr.Contains(" Dear ") || descr.Contains("Thank you"))
                        requestData.bank_remark = "No Remarks";
                    else
                        requestData.bank_remark = descr;

                    String status = reader["Status"].ToString();

                    if (status.Length >= 7 || status == "Open")
                    {
                        requestData.Status = "PENDING";
                    }
                    else
                        requestData.Status = (reader["Status"].ToString()).ToUpper();

                    requestData.action_taken = reader["action_taken"].ToString();
                    requestData.amt_recovd = reader["amt_recovd"].ToString();
                    requestData.amt_refunde = reader["amt_refunde"].ToString();
                    requestData.amt_applicable = reader["amt_applicable"].ToString();
                    requestData.comp_date_clsed = reader["comp_date_clsed"].ToString();
                    requestData.comp_date_recv = reader["comp_date_recv"].ToString();
                    requestData.comp_prayer = reader["comp_prayer"].ToString();


                    String desc = reader["comp_desc"].ToString();
                    if (desc == "" || desc == null)
                        requestData.comp_desc = "Others";
                    else if (desc == "" || desc == null || desc.Contains("Thank you for your mail") || desc.Contains("Thank you") || desc.Contains("Thank you") || desc.Contains("Thank you"))
                        requestData.comp_desc = "NO REMARKS";
                    else
                        requestData.comp_desc = desc;

                    requestData.comp_subj = reader["comp_subj"].ToString();
                    requestData.comp_name = reader["comp_name"].ToString();
                    requestData.First_name_petitioner = reader["First_name_pet"].ToString();
                    requestData.Last_name_pet = reader["Last_name_pet"].ToString();
                    requestData.Middle_name_petitioner = reader["Middle_name_pet"].ToString();
                    requestData.comp_fininmpl = reader["comp_fininmpl"].ToString();
                    String comp_cat = reader["comp_cat"].ToString();
                    requestData.comp_cat = "A999";
                    //requestData.comp_subcat = reader["comp_subcat"].ToString();     
                    String subCat = reader["comp_subcat"].ToString();
                    //subCat = "J999";
                    if (reportType == "DCM")
                    {
                        //    foreach (Sheet1 a in cat.Sheet1)
                        //        if (a.Item == reader["comp_subj"].ToString())
                        //            subCat = a.CCMSCODE;
                    }

                    requestData.comp_subcat = reader["comp_subcat"].ToString(); //reader["comp_subj"].ToString();// subCat;
                    //requestData.addr_2_comp = reader["addr_2_comp"].ToString();
                    if ((reader["addr_1_comp"].ToString() == "") || (reader["addr_1_comp"].ToString() == null))
                        requestData.addr_1_comp = "NA";
                    else
                        requestData.addr_1_comp = reader["addr_1_comp"].ToString();

                    if (reader["addr_2_comp"].ToString() == "" || (reader["addr_2_comp"].ToString() == null))
                        requestData.addr_2_comp = "NA";
                    else
                        requestData.addr_2_comp = reader["addr_2_comp"].ToString();

                    //requestData.acct_owner_offphnum = reader["acct_owner_offphnum"].ToString();
                    String acct_owner_offphnum = reader["acct_owner_offphnum"].ToString();
                    if (acct_owner_offphnum == "NA")
                        acct_owner_offphnum = "0";
                    // else
                    //   acct_owner_offphnum = "0";
                    String formatacct_owner_offphnum = covertMobileTo(acct_owner_offphnum);
                    if (formatacct_owner_offphnum == "(234) 234-000000 ")
                        formatacct_owner_offphnum = "(234) 234-0000000";
                    requestData.acct_owner_offphnum = formatacct_owner_offphnum;

                    requestData.comp_email = reader["comp_email"].ToString();
                    //requestData.comp_location = reader["comp_location"].ToString();
                    // String comp_City = reader["comp_location"].ToString();
                    //if (comp_City == "NA")
                    //  requestData.comp_location = "LGS";
                    //else
                    //  requestData.comp_location = "IBD";
                    requestData.comp_location = reader["branch_name"].ToString();

                    requestData.comp_channel = reader["comp_channel"].ToString();

                    String mobilephone = reader["acct_owner_phnum"].ToString();
                    if (mobilephone == "NA")
                        mobilephone = "0";

                    // else
                    //  mobilephone = "0";
                    String formatPhone = covertMobileTo(mobilephone);
                    //formatacct_owner_offphnum;
                    if (formatPhone == "(234) 234-000000 ")
                        formatPhone = "(234) 234-0000000";
                    requestData.acct_owner_phnum = formatPhone;

                    String pCode = reader["acct_owner_pcode"].ToString();

                    requestData.acct_owner_pcode = "234";

                    //requestData.acct_owner_country = reader["acct_owner_country"].ToString();
                    String ownerCountry = reader["acct_owner_country"].ToString();
                    //if (ownerCountry == "" || ownerCountry == null)
                    //    requestData.acct_owner_country = "NA";
                    //else
                    requestData.acct_owner_country = "NG";
                    String ownerState = reader["acct_owner_state"].ToString();
                    if ((ownerState == "") || (ownerState == null))
                        requestData.acct_owner_state = "NA";// getCityAndState(requestData.addr_1_comp, "State");// "NA";
                    else
                        requestData.acct_owner_state = reader["acct_owner_state"].ToString();
                    String city = reader["acct_owner_city"].ToString();
                    if (city == "" || city == null)
                        requestData.acct_owner_city = "NA";//getCityAndState(requestData.addr_1_comp,"City");//"NA";
                    else
                        requestData.acct_owner_city = reader["acct_owner_city"].ToString();


                    requestData.acct_ccy = reader["acct_ccy"].ToString();
                    if (reader["acct_ccy"].ToString() == "$")
                        requestData.acct_ccy = "USD";

                    //if (reader["acct_type"].ToString() == "NA")
                    // requestData.acct_type = "OTHERS";
                    //   string tempaccttype =  getaccounttype(reader["acct_num"].ToString());
                    string acct = "OTHERS";
                    //foreach (string item in accttype)
                    //{
                    //    if ((tempaccttype).Contains(item))
                    //    {
                    //        acct = item;
                    //    }
                    //}
                    // acct=reader["acct_type"].ToString();
                    // requestData.acct_type = acct;
                    string macct_type = reader["acct_type"].ToString();

                    if (macct_type == "DOMICILLIARY SAVINGS" || macct_type == "DOMICILLIARY CURRENT")
                    {
                        requestData.acct_type = "DOMICILIARY";
                    }
                    else if (macct_type == "BASIC SAVINGS ACCOUN" || macct_type == "BUSINESS SAVINGS ACC" || macct_type.Contains("SAVINGS") || macct_type == "CLASSIC ADVANCE SALA" || macct_type == "FLEXX YOUTH SAVINGS" ||
                  macct_type == "Virtual Savings" || macct_type == "THIRD PARTY SAVINGS" || macct_type == "TARGET ACCOUNT NAIRA" || macct_type == "Savings Account - St" || macct_type == "SALARY SAVINGS (TIER" || macct_type == "MASTERCARD PREFUNDED" || macct_type == "KIDS ACCOUNT TIER 1" || macct_type == "KIDS ACCOUNT" || macct_type == "FLEXX YOUTH SAVINGS" || macct_type == "FCMB SALARY SAVINGS" || macct_type == "FCMB BUSINESS ACCOUN" || macct_type == "E-SAVINGS ACCOUNT" || macct_type == "Easy Account Naira (" || macct_type == "DSA SAVINGS ACCOUNT" || macct_type == "CLASSIC SAVINGS ACCO" || macct_type == "NAIRAWISE MASS MARKE" || macct_type == "FCMB SALARY SAVINGS" || macct_type == "PREMIUM SAVINGS ACCO" || macct_type.Contains("SAVINGS"))
                    {
                        requestData.acct_type = "SAVINGS";
                    }
                    else if (macct_type.Contains("CURRENT") || macct_type == "CLASSIC CURRENT ACCO" || macct_type.Contains("CURRENT") || macct_type == "CURRENT ACCOUNT- LTD" || macct_type == "CURRENT ACCOUNT- NON" ||
                   macct_type == "STAFF CURRENT ACCOUN" || macct_type == "STAFF - CURRENT ACCO" || macct_type == "PUBLIC SECTOR CURREN" || macct_type == "PREMIUM SALARY CURRN" || macct_type == "PREMIUM CURRENT ACCO" || macct_type == "PERSONAL BUSINESS AC" || macct_type == "Current Account - Ot" || macct_type == "CURRENT ACCOUNT - CD" || macct_type == "SALARY CURRENT ACCOU")
                    {
                        requestData.acct_type = "CURRENT";
                    }

                    else if (macct_type == "Customer Collection")
                    {
                        requestData.acct_type = "LOAN";
                    }
                    else
                    {
                        requestData.acct_type = "OTHERS";
                    }

                    //  requestData.acct_type = getaccounttype(reader["acct_num"].ToString());
                    // else
                    // requestData.acct_type = reader["acct_type"].ToString();

                    requestData.acct_num = reader["acct_num"].ToString();

                    requestData.Last_name_pet = reader["Last_name_pet"].ToString();

                    string mclient_type = reader["client_type"].ToString();

                    if (mclient_type == "EDU" || mclient_type == "CLU" || mclient_type == "IND" || mclient_type == "INFO" || mclient_type == "JONT" || mclient_type == "PRIV" || mclient_type == "SOLE")
                    {
                        requestData.client_type = "INDV";
                    }
                    else if (mclient_type == "AGRIC" || mclient_type == "COR")
                    {
                        requestData.client_type = "CORP";
                    }
                    else
                    {
                        requestData.client_type = "INDV";
                    }
                    //requestData.client_type = reader["client_type"].ToString();
                    // requestData.client_type = "INDV";
                    requestData.technician = reader["technician"].ToString();
                    requestData.branch_name = reader["branch_name"].ToString();
                    requestData.comp_age = reader["comp_age"].ToString();
                    // requestData.comp_gender = reader["comp_gender"].ToString();
                    // requestData.comp_gender = "O";
                    string mcomp_gender = reader["comp_gender"].ToString();

                    if (mcomp_gender == "MALE" || mcomp_gender == "M")
                    {
                        requestData.comp_gender = "M";
                    }
                    else if (mcomp_gender == "FEMALE" || mcomp_gender == "F")
                    {
                        requestData.comp_gender = "F";
                    }
                    else
                    {
                        requestData.comp_gender = "O";
                    }
                    requestList.Add(requestData);
                    ++count;
                }
                //LOGGER.Info("DCMDetails COMPLETED");
                con.Close();
                return requestList;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return null;
        }
        private static ArrayList DCMDetails(DateTime calender, String reportType)
        {
            try
            {
                SqlConnection con = DBConnection.DCMSource();
                DayOfWeek weekDay = ValidateWeekday();
                SqlCommand command = null;
                string dowloadDate = $"{calender.Year}-{calender.Month}-{calender.Day}";
          //      if (calender.DayOfWeek.ToString() == "Monday")
          //      {
          //          if (reportType == "DCM")
          //          {
          //              command = new SqlCommand("Select  * from [ccms_comp_detail_DCM] where " +
          //" downloaddate >= DATEADD(day, -3, convert(date," + "'" + dowloadDate + "'" + ")) and downloaddate < convert(date," + "'" + dowloadDate + "'" + ")"
          //, con);
          //          }
          //          else if (reportType == "Profectus")
          //          {
          //              command = new SqlCommand("Select  * from [ccms_comp_detail_Profectus] where " +
          //" downloaddate >= DATEADD(day, -3, convert(date," + "'" + dowloadDate + "'" + ")) and downloaddate < convert(date," + "'" + dowloadDate + "'" + ")"
          //, con);
          //          }
          //          else if (reportType == "Integrify")
          //          {
          //              command = new SqlCommand("Select  * from [ccms_comp_detail_Integrify] where " +
          //" downloaddate >= DATEADD(day, -3, convert(date," + "'" + dowloadDate + "'" + ")) and downloaddate < convert(date," + "'" + dowloadDate + "'" + ")"
          //, con);
          //          }


          //      }
          //      else
          //      {
                    if (reportType == "DCM")
                    {
                        command = new SqlCommand("Select  * from [ccms_comp_detail2] where " +
                                     " downloaddate >= DATEADD(day, -1, convert(date," + "'" + dowloadDate + "'" + ")) and downloaddate < convert(date," + "'" + dowloadDate + "'" + ")"
                                     , con);
                    }
                    if (reportType == "Profectus")
                    {
                        command = new SqlCommand("Select  * from [ccms_comp_detail_Profectus] where " +
                                         " downloaddate >= DATEADD(day, -1, convert(date," + "'" + dowloadDate + "'" + ")) and downloaddate < convert(date," + "'" + dowloadDate + "'" + ")"
                                         , con);
                    }
                    else if (reportType == "Integrify")
                    {
                        command = new SqlCommand("  Select  * from [ccms_comp_detail_Integrify] where " +
                                         " downloaddate >= DATEADD(day, -1, convert(date," + "'" + dowloadDate + "'" + ")) and downloaddate < convert(date," + "'" + dowloadDate + "'" + ")"
                                           , con);
                    } 
                    else if (reportType == "CRM_360")
                    {
                        command = new SqlCommand("Select  * from [ccms_comp_detail_CRM360 ] where " +
                                         " downloaddate >= DATEADD(day, -1, convert(date," + "'" + dowloadDate + "'" + ")) and downloaddate < convert(date," + "'" + dowloadDate + "'" + ")"
                                           , con);
                    }

               // }
                //SqlCommand command = new SqlCommand("Proc_ccms_comp_detail2", con);
                //command.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                var requestList = new ArrayList();
                ClsRequestData requestData = null;
                int count = 0;
                string[] accttype = { "SAVINGS", "CURRENT", "LOAN", "DOMICILIARY", "OTHERS" };
                // Category cat = JsonConvert.DeserializeObject<Category>(File.ReadAllText(@"C:\Users\ayodeji.olukowi\Documents\data.json"));
                Category cat = JsonConvert.DeserializeObject<Category>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "data.json"));

                while (reader.Read())
                {
                    requestData = new ClsRequestData();
                    requestData.tracknum = reader["tracknum"].ToString();
                    requestData.branch_name = reader["branch_name"].ToString();
                    requestData.UniqueIdentificationNumber = reader["UniqueIdentificationNumber"].ToString();
                    requestData.Preferred_contact_address = reader["Preferred_contact_address"].ToString();
                    requestData.Preferred_contact_Email = reader["Preferred_contact_Email"].ToString();
                    requestData.Preferred_contact_phone = reader["Preferred_contact_phone"].ToString();
                    requestData.Root_cause = reader["Root_cause"].ToString();
                    // requestData.bank_remark = reader["bank_remark"].ToString();

                    String descr = reader["bank_remark"].ToString();
                    if (descr == "" || descr == null)
                        requestData.bank_remark = "Others";
                    else if (descr.Contains("Thank you for your mail") || descr.Contains("Dear") || descr.Contains(" Dear ") || descr.Contains("Thank you"))
                        requestData.bank_remark = "No Remarks";
                    else
                        requestData.bank_remark = descr;

                    String status = reader["Status"].ToString();

                    if (status.Length >= 7 || status == "Logged" || status == "Open")
                    {
                        requestData.Status = "PENDING";
                    }
                    else
                        requestData.Status = (reader["Status"].ToString()).ToUpper();

                    requestData.action_taken = reader["action_taken"].ToString();
                    requestData.amt_recovd = reader["amt_recovd"].ToString();
                    requestData.amt_refunde = reader["amt_refunde"].ToString();
                    requestData.amt_applicable = reader["amt_applicable"].ToString();
                    requestData.comp_date_clsed = reader["comp_date_clsed"].ToString();
                    requestData.comp_date_recv = reader["comp_date_recv"].ToString();
                    requestData.comp_prayer = reader["comp_prayer"].ToString();


                    String desc = reader["comp_desc"].ToString();
                    if (desc == "" || desc == null)
                        requestData.comp_desc = "Others";
                    else if (desc == "" || desc == null || desc.Contains("Thank you for your mail") || desc.Contains("Thank you") || desc.Contains("Thank you") || descr.Contains("Dear") || descr.Contains(" Dear "))
                        requestData.comp_desc = "NO REMARKS";
                    else
                        requestData.comp_desc = desc;


                    requestData.comp_subj = reader["comp_subj"].ToString();
                    requestData.comp_name = reader["comp_name"].ToString();
                    requestData.First_name_petitioner = reader["First_name_pet"].ToString();
                    requestData.Last_name_pet = reader["Last_name_pet"].ToString();
                    requestData.Middle_name_petitioner = reader["Middle_name_pet"].ToString();
                    requestData.comp_fininmpl = reader["comp_fininmpl"].ToString();
                    String comp_cat = reader["comp_cat"].ToString();
                    requestData.comp_cat = "A999";
                    //requestData.comp_subcat = reader["comp_subcat"].ToString();     
                    String subCat = reader["comp_subcat"].ToString();
                    subCat = "J999";
                    //foreach (Sheet1 a in cat.Sheet1)
                    //    if (a.Item == reader["comp_subj"].ToString())
                    //        subCat = a.CCMSCODE;
                    requestData.comp_subcat = reader["comp_subcat"].ToString(); //reader["comp_subj"].ToString();
                                                                                //requestData.addr_2_comp = reader["addr_2_comp"].ToString();
                    if ((reader["addr_1_comp"].ToString() == "") || (reader["addr_1_comp"].ToString() == null))
                        requestData.addr_1_comp = "NA";
                    else
                        requestData.addr_1_comp = reader["addr_1_comp"].ToString();

                    if (reader["addr_2_comp"].ToString() == "" || (reader["addr_2_comp"].ToString() == null))
                        requestData.addr_2_comp = "NA";
                    else
                        requestData.addr_2_comp = reader["addr_2_comp"].ToString();

                    //requestData.acct_owner_offphnum = reader["acct_owner_offphnum"].ToString();
                    String acct_owner_offphnum = reader["acct_owner_offphnum"].ToString();
                    if (acct_owner_offphnum == "NA")
                        acct_owner_offphnum = "0";
                    // else
                    //   acct_owner_offphnum = "0";
                    String formatacct_owner_offphnum = covertMobileTo(acct_owner_offphnum);
                    if (formatacct_owner_offphnum == "(234) 234-000000 ")
                        formatacct_owner_offphnum = "(234) 234-0000000";

                    requestData.acct_owner_offphnum = formatacct_owner_offphnum;

                    requestData.comp_email = reader["comp_email"].ToString();
                    //requestData.comp_location = reader["comp_location"].ToString();
                    //String comp_City = reader["comp_location"].ToString();
                    //if (comp_City == "NA")
                    //    requestData.comp_location = "LGS";
                    //else
                    //    requestData.comp_location = "IBD";
                    requestData.comp_location = reader["branch_name"].ToString();

                    requestData.comp_channel = reader["comp_channel"].ToString();

                    String mobilephone = reader["acct_owner_phnum"].ToString();
                    if (mobilephone == "NA")
                        mobilephone = "0";
                    // else
                    //   mobilephone = "0";
                    String formatPhone = covertMobileTo(mobilephone);
                    if (formatPhone == "(234) 234-000000 ")
                        formatPhone = "(234) 234-0000000";
                    requestData.acct_owner_phnum = formatPhone; //formatacct_owner_offphnum;

                    String pCode = reader["acct_owner_pcode"].ToString();

                    requestData.acct_owner_pcode = "234";

                    //requestData.acct_owner_country = reader["acct_owner_country"].ToString();
                    String ownerCountry = reader["acct_owner_country"].ToString();
                    //if (ownerCountry == "" || ownerCountry == null)
                    //    requestData.acct_owner_country = "NA";
                    //else
                    requestData.acct_owner_country = "NG";
                    String ownerState = reader["acct_owner_state"].ToString();
                    if ((ownerState == "") || (ownerState == null))
                        requestData.acct_owner_state = "NA";//getCityAndState(reader["addr_1_comp"].ToString(), "State");//"NA";
                    else
                        requestData.acct_owner_state = reader["acct_owner_state"].ToString();
                    String city = reader["acct_owner_city"].ToString();
                    if (city == "" || city == null)
                        requestData.acct_owner_city = "NA";//getCityAndState(reader["addr_1_comp"].ToString(), "City"); //"NA";
                    else
                        requestData.acct_owner_city = reader["acct_owner_city"].ToString();

                    requestData.acct_ccy = reader["acct_ccy"].ToString();
                    if (reader["acct_ccy"].ToString() == "$")
                        requestData.acct_ccy = "USD";
                    if (reader["acct_ccy"].ToString() == "£")
                        requestData.acct_ccy = "GBP";

                    //if (reader["acct_type"].ToString() == "NA")
                    // requestData.acct_type = "OTHERS";


                    //string tempaccttype =  getaccounttype(reader["acct_num"].ToString());
                    string acct = "OTHERS";
                    //foreach (string item in accttype)
                    //{
                    //    if ((tempaccttype).Contains(item))
                    //    {
                    //        acct = item;
                    //    }
                    //}
                    // acct = reader["acct_type"].ToString();
                    string macct_type = reader["acct_type"].ToString();
                    if (macct_type == "DOMICILLIARY SAVINGS" || macct_type == "DOMICILLIARY CURRENT")
                    {
                        requestData.acct_type = "DOMICILIARY";
                    }
                    else if (macct_type == "BASIC SAVINGS ACCOUN" || macct_type == "BUSINESS SAVINGS ACC" || macct_type.Contains("SAVINGS") || macct_type == "CLASSIC ADVANCE SALA" || macct_type == "FLEXX YOUTH SAVINGS" ||
                  macct_type == "Virtual Savings" || macct_type == "THIRD PARTY SAVINGS" || macct_type == "TARGET ACCOUNT NAIRA" || macct_type == "Savings Account - St" || macct_type == "SALARY SAVINGS (TIER" || macct_type == "MASTERCARD PREFUNDED" || macct_type == "KIDS ACCOUNT TIER 1" || macct_type == "KIDS ACCOUNT" || macct_type == "FLEXX YOUTH SAVINGS" || macct_type == "FCMB SALARY SAVINGS" || macct_type == "FCMB BUSINESS ACCOUN" || macct_type == "E-SAVINGS ACCOUNT" || macct_type == "Easy Account Naira (" || macct_type == "DSA SAVINGS ACCOUNT" || macct_type == "CLASSIC SAVINGS ACCO" || macct_type == "NAIRAWISE MASS MARKE" || macct_type == "FCMB SALARY SAVINGS" || macct_type == "PREMIUM SAVINGS ACCO")
                    {
                        requestData.acct_type = "SAVINGS";
                    }
                    else if (macct_type == "CLASSIC CURRENT ACCO" || macct_type == "CURRENT ACCOUNT- LTD" || macct_type.Contains("CURRENT") || macct_type == "CURRENT ACCOUNT- NON" ||
                   macct_type == "STAFF CURRENT ACCOUN" || macct_type == "STAFF - CURRENT ACCO" || macct_type == "PUBLIC SECTOR CURREN" || macct_type == "PREMIUM SALARY CURRN" || macct_type == "PREMIUM CURRENT ACCO" || macct_type == "PERSONAL BUSINESS AC" || macct_type == "Current Account - Ot" || macct_type == "CURRENT ACCOUNT - CD" || macct_type == "SALARY CURRENT ACCOU")
                    {
                        requestData.acct_type = "CURRENT";
                    }

                    else if (macct_type == "Customer Collection")
                    {
                        requestData.acct_type = "LOAN";
                    }
                    else
                    {
                        requestData.acct_type = "OTHERS";
                    }

                    // requestData.acct_type = acct;

                    // requestData.acct_type = getaccounttype(reader["acct_num"].ToString());
                    // else
                    // requestData.acct_type = reader["acct_type"].ToString();

                    requestData.acct_num = reader["acct_num"].ToString();

                    requestData.Last_name_pet = reader["Last_name_pet"].ToString();

                    //  requestData.client_type = reader["client_type"].ToString();
                    string mclient_type = reader["client_type"].ToString();

                    if (mclient_type == "EDU" || mclient_type == "CLU" || mclient_type == "IND" || mclient_type == "INFO" || mclient_type == "JONT" || mclient_type == "PRIV" || mclient_type == "SOLE")
                    {
                        requestData.client_type = "INDV";
                    }
                    else if (mclient_type == "AGRIC" || mclient_type == "COR")
                    {
                        requestData.client_type = "CORP";
                    }
                    else
                    {
                        requestData.client_type = "INDV";
                    }
                    //  requestData.client_type = "INDV";
                    requestData.technician = reader["technician"].ToString();
                    requestData.branch_name = reader["branch_name"].ToString();
                    requestData.comp_age = reader["comp_age"].ToString();
                    // requestData.comp_gender = reader["comp_gender"].ToString();
                    //requestData.comp_gender = "O";
                    string mcomp_gender = reader["comp_gender"].ToString();

                    if (mcomp_gender == "MALE" || mcomp_gender == "M")
                    {
                        requestData.comp_gender = "M";
                    }
                    else if (mcomp_gender == "FEMALE" || mcomp_gender == "F")
                    {
                        requestData.comp_gender = "F";
                    }
                    else
                    {
                        requestData.comp_gender = "O";
                    }
                    requestList.Add(requestData);
                    ++count;
                }
                //LOGGER.Info("DCMDetails COMPLETED");
                con.Close();
                return requestList;
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return null;
        }
        public static int InsertIntoCCMS_ccms_comp_detl(ArrayList dcmbRequestList)
        {
            //LOGGER.Info("InsertIntoCCMS Processing...");
            string SqlText = "insertCCMSdetail";
            int count = 0;
            try
            {
                SqlConnection connection = DBConnection.CCMSPortalDestination();
                SqlConnection con = connection;
                SqlCommand cmd = new SqlCommand(SqlText, con);
                foreach (ClsRequestData ReqData in dcmbRequestList)
                {
                    {
                        con.Open();
                        cmd.CommandText = SqlText;
                        cmd.CommandType = CommandType.StoredProcedure;
                        // cmd.Parameters.AddWithValue("@tracknum", ReqData.tracknum);
                        cmd.Parameters.AddWithValue("@p_branch_name", ReqData.branch_name);
                        cmd.Parameters.AddWithValue("@p_technician", ReqData.technician);
                        cmd.Parameters.AddWithValue("@p_client_type", ReqData.client_type);
                        cmd.Parameters.AddWithValue("@p_comp_name", ReqData.comp_name);
                        cmd.Parameters.AddWithValue("@p_First_name_pet", ReqData.First_name_petitioner);
                        cmd.Parameters.AddWithValue("@p_Middle_name_pet", ReqData.Middle_name_petitioner);
                        cmd.Parameters.AddWithValue("@p_Last_name_pet", ReqData.Last_name_pet);
                        cmd.Parameters.AddWithValue("@p_acct_num", ReqData.acct_num);
                        cmd.Parameters.AddWithValue("@p_acct_type", ReqData.acct_type);
                        cmd.Parameters.AddWithValue("@p_acct_ccy", ReqData.acct_ccy);
                        cmd.Parameters.AddWithValue("@p_addr_1_comp", ReqData.addr_1_comp);
                        cmd.Parameters.AddWithValue("@p_addr_2_comp", ReqData.addr_2_comp);
                        cmd.Parameters.AddWithValue("@p_acct_owner_city", ReqData.acct_owner_city);
                        cmd.Parameters.AddWithValue("@p_acct_owner_state", ReqData.acct_owner_state);
                        cmd.Parameters.AddWithValue("@p_acct_owner_country", ReqData.acct_owner_country);
                        cmd.Parameters.AddWithValue("@p_acct_owner_pcode", ReqData.acct_owner_pcode);
                        cmd.Parameters.AddWithValue("@p_acct_owner_phnum", ReqData.acct_owner_phnum);
                        cmd.Parameters.AddWithValue("@p_acct_owner_offphnum", ReqData.acct_owner_offphnum);
                        cmd.Parameters.AddWithValue("@p_comp_channel", ReqData.comp_channel);
                        cmd.Parameters.AddWithValue("@p_comp_location", ReqData.comp_location);
                        cmd.Parameters.AddWithValue("@p_comp_email", ReqData.comp_email);
                        cmd.Parameters.AddWithValue("@p_comp_fininmpl", ReqData.comp_fininmpl);
                        cmd.Parameters.AddWithValue("@p_comp_cat", ReqData.comp_cat);
                        cmd.Parameters.AddWithValue("@p_comp_subcat", ReqData.comp_subcat);
                        cmd.Parameters.AddWithValue("@p_comp_subj", ReqData.comp_subj);
                        cmd.Parameters.AddWithValue("@p_comp_desc", ReqData.comp_desc);
                        cmd.Parameters.AddWithValue("@p_comp_prayer", ReqData.comp_prayer);
                        cmd.Parameters.AddWithValue("@p_comp_date_recv", ReqData.comp_date_recv);
                        cmd.Parameters.AddWithValue("@p_comp_date_clsed", ReqData.comp_date_clsed);
                        cmd.Parameters.AddWithValue("@p_amt_applicable", ReqData.amt_applicable);
                        cmd.Parameters.AddWithValue("@p_amt_refund", ReqData.amt_refunde);
                        cmd.Parameters.AddWithValue("@p_amt_recovd", ReqData.amt_recovd);
                        cmd.Parameters.AddWithValue("@p_action_taken", ReqData.action_taken);
                        cmd.Parameters.AddWithValue("@p_Status", ReqData.Status);
                        cmd.Parameters.AddWithValue("@p_tracknum", ReqData.tracknum);
                        cmd.Parameters.AddWithValue("@p_bank_remark", ReqData.bank_remark);
                        cmd.Parameters.AddWithValue("@p_comp_age", ReqData.comp_age);
                        cmd.Parameters.AddWithValue("@p_comp_gender", ReqData.comp_gender);
                        cmd.ExecuteNonQuery();
                        // var trackNum = returnTrackNum.Value;

                        ++count;
                        UpdateDCMTable(ReqData.tracknum);
                        //if (count == 1)
                        //    break;

                    }
                }
                con.Close();
                //LOGGER.Info("InsertIntoCCMS completed!");
                return count;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                return 0;
            }
        }
        public static int InsertIntoCCMS_ccms_comp_detail2(ArrayList dcmbRequestList)
        {
            //LOGGER.Info("InsertIntoCCMS Processing...");

            String connectionString = ConfigurationManager.ConnectionStrings["CCMSPortalConn"].ToString();
            string SqlText = "Proc_ccms_comp_detail2a";
            int count = 0;
            try
            {
                foreach (ClsRequestData ReqData in dcmbRequestList)
                {

                    SqlConnection con = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand(SqlText, con);
                    {
                        try
                        {

                            con.Open();
                            cmd.CommandText = SqlText;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@tracknum", ReqData.tracknum);
                            cmd.Parameters.AddWithValue("@branch_name", ReqData.branch_name);
                            cmd.Parameters.AddWithValue("@technician", ReqData.technician);
                            cmd.Parameters.AddWithValue("@client_type", ReqData.client_type);
                            cmd.Parameters.AddWithValue("@comp_name", ReqData.comp_name);
                            cmd.Parameters.AddWithValue("@First_name_pet", ReqData.First_name_petitioner);
                            cmd.Parameters.AddWithValue("@Middle_name_pet", ReqData.Middle_name_petitioner);
                            cmd.Parameters.AddWithValue("@Last_name_pet", ReqData.Last_name_pet);
                            cmd.Parameters.AddWithValue("@acct_num", ReqData.acct_num);
                            cmd.Parameters.AddWithValue("@acct_type", ReqData.acct_type);
                            cmd.Parameters.AddWithValue("@acct_ccy", ReqData.acct_ccy);
                            cmd.Parameters.AddWithValue("@addr_1_comp", ReqData.addr_1_comp);
                            cmd.Parameters.AddWithValue("@addr_2_comp", ReqData.addr_2_comp);
                            cmd.Parameters.AddWithValue("@acct_owner_city", ReqData.acct_owner_city);
                            cmd.Parameters.AddWithValue("@acct_owner_state", ReqData.acct_owner_state);
                            cmd.Parameters.AddWithValue("@acct_owner_country", ReqData.acct_owner_country);
                            cmd.Parameters.AddWithValue("@acct_owner_pcode", ReqData.acct_owner_pcode);
                            cmd.Parameters.AddWithValue("@acct_owner_phnum", ReqData.acct_owner_phnum);
                            cmd.Parameters.AddWithValue("@acct_owner_offphnum", ReqData.acct_owner_offphnum);
                            cmd.Parameters.AddWithValue("@comp_channel", ReqData.comp_channel);
                            cmd.Parameters.AddWithValue("@comp_location", ReqData.comp_location);
                            cmd.Parameters.AddWithValue("@comp_email", ReqData.comp_email);
                            cmd.Parameters.AddWithValue("@comp_fininmpl", ReqData.comp_fininmpl);
                            cmd.Parameters.AddWithValue("@comp_cat", ReqData.comp_cat);
                            cmd.Parameters.AddWithValue("@comp_subcat", ReqData.comp_subcat);
                            cmd.Parameters.AddWithValue("@comp_subj", ReqData.comp_subj);
                            cmd.Parameters.AddWithValue("@comp_desc", ReqData.comp_desc);
                            cmd.Parameters.AddWithValue("@comp_prayer", ReqData.comp_prayer);
                            cmd.Parameters.AddWithValue("@comp_date_recv", ReqData.comp_date_recv);
                            cmd.Parameters.AddWithValue("@comp_date_clsed", ReqData.comp_date_clsed);
                            cmd.Parameters.AddWithValue("@amt_applicable", ReqData.amt_applicable);
                            cmd.Parameters.AddWithValue("@amt_refunde", ReqData.amt_refunde);
                            cmd.Parameters.AddWithValue("@amt_recovd", ReqData.amt_recovd);
                            cmd.Parameters.AddWithValue("@action_taken", ReqData.action_taken);
                            cmd.Parameters.AddWithValue("@Status", ReqData.Status);
                            cmd.Parameters.AddWithValue("@bank_remark", ReqData.bank_remark);
                            cmd.Parameters.AddWithValue("@Root_cause", ReqData.Root_cause);
                            cmd.Parameters.AddWithValue("@Preferred_contact_phone", ReqData.Preferred_contact_phone);
                            cmd.Parameters.AddWithValue("@Preferred_contact_Email", ReqData.Preferred_contact_Email);
                            cmd.Parameters.AddWithValue("@Preferred_contact_address", ReqData.Preferred_contact_address);
                            cmd.Parameters.AddWithValue("@UniqueIdentificationNumber", ReqData.UniqueIdentificationNumber);
                            cmd.Parameters.AddWithValue("@comp_age", ReqData.comp_age);
                            cmd.Parameters.AddWithValue("@comp_gender", ReqData.comp_gender);

                            var returnTrackNum = cmd.Parameters.Add("@ReturnVal", SqlDbType.VarChar);
                            returnTrackNum.Direction = ParameterDirection.ReturnValue;
                            cmd.ExecuteNonQuery();
                            ++count;
                            Console.WriteLine(ReqData.tracknum);
                            UpdateDCMTable(ReqData.tracknum);

                            ++count;

                        }
                        finally
                        {
                            con.Close();
                        }



                    }
                }
                return count;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return 0;
            }
        }

        public static void UpdateDCMTable(string trackNum)
        {
            try
            {
                string UpdateCommand = "UpdateCCMSdetail2";
                SqlConnection con = DBConnection.DCMSource();
                using (SqlCommand sqlRenameCommand = new SqlCommand(UpdateCommand, con))
                {
                    sqlRenameCommand.CommandType = CommandType.StoredProcedure;
                    sqlRenameCommand.Parameters.Add("@trackNum", SqlDbType.NVarChar).Value = trackNum;
                    con.Open();
                    sqlRenameCommand.ExecuteNonQuery();
                    con.Close();
                    // LOGGER.Info("DCM Table updated successfully!");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }
        private static string covertMobileTo(string number)
        {
            string value;
            if (number.Length >= 11 & number.StartsWith("234") == false)
                value = "(234) " + number.Substring(1, 3) + "-" + number.Substring(number.Length - 7);
            else if (number.Length == 10 | (number.StartsWith("234") & number.Length == 10))
                value = "(234) " + number.Substring(0, 3) + "-" + number.Substring(number.Length - 7);
            else
            {
                number = number.PadLeft(11, '0');
                value = "(234) " + number.Substring(1, 3) + "-" + number.Substring(number.Length - 7);
            }

            return value;
        }
        private static string getCityAndState(string address, string CityOrState)
        {
            if (address == "NA")
                return "NA";
            else
            {
                Uri Url = new Uri("https://maps.googleapis.com/maps/api/geocode");
                IRestClient restClient = new RestClient(Url.ToString());
                IRestRequest restRequest = new RestRequest("json", Method.GET);
                restRequest.AddParameter("address", address);
                restRequest.AddParameter("key", ConfigurationManager.AppSettings["GoogleApiKey"].ToString());
                IRestResponse<GoogleGeoLocationParseResultModel> restResponse = restClient.Execute<GoogleGeoLocationParseResultModel>(restRequest);

                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GoogleGeoLocationParseResultModel model = JsonConvert.DeserializeObject<GoogleGeoLocationParseResultModel>(restResponse.Content);
                    if (model.status == "OK")
                    {
                        foreach (Address_Components item in model.results[0].address_components)
                        {
                            if (CityOrState == "City" && item.types[0] == "locality")
                                return item.long_name;
                            else if (CityOrState == "State" && item.types[0] == "administrative_area_level_1")
                                return item.long_name;
                            //else return "NA";
                        }
                        return "NA";
                    }
                    else
                    {
                        return "NA";
                    }
                }
                else
                {
                    Console.WriteLine(restResponse.ErrorMessage);
                    return "NA";
                }
            }
        }
        private static string getaccounttype(string acctNumber)
        {
            try
            {
                string re = "OTHERS";
                string oradb = "User Id=bateam;Password=Lamp_1234; Data Source=172.27.10.70:1521/UATFCMB";
                OracleConnection conn = new OracleConnection(oradb);  // C#
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT gsp.schm_desc AccountType FROM tbaadm.gam gam INNER JOIN tbaadm.gsp gsp ON gam.schm_code = gsp.schm_code where gam.cif_id = (select cif_id from tbaadm.gam where foracid = '" + acctNumber + "')";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                re = dr.GetString(0);
                conn.Dispose();

                //  var client = new RestClient("https://devapi.fcmb.com:8443/accountinquiry/api/AccountInquiry/customerAccountTypeByAccountNo");

                //  client.Timeout = -1;
                //  var request = new RestRequest(Method.GET);
                //  request.AddHeader("product_id", "");
                //  request.AddHeader("client_id", "250");
                //  request.AddHeader("Ocp-Apim-Subscription-Key", "24ee7c354211430287f20b59bbb04d9d");
                //  request.AddHeader("Content-Type", "application/json");
                //  request.AddHeader("Cookie", "incap_ses_802_2587615=hNNzHqfiPTFO+o1SMkchC+QLXmEAAAAA5IYea+hQv8kTZNSEvtes4w==; visid_incap_2587615=f9JT/cg0R5ynyrpQxl6uOL+hXGEAAAAAQUIPAAAAAADZ1do5VKp6J/0JJUCwyfmZ");
                //  Dictionary<string, string> logincred = new Dictionary<string, string>();
                //  logincred.Add("accountNo", acctNumber);
                //  request.AddParameter("application/json", JsonConvert.SerializeObject(logincred), ParameterType.RequestBody);
                //  request.AddParameter("accountNo", acctNumber);
                //  var cancellationTokenSource = new CancellationTokenSource();
                ////  var cli = new Class1();
                //  string re="OTHERS";
                //  //IRestResponse response;// = client.Execute(request);//await cli.ExecuteAsync<AcctInquiryResponse>(request);
                //  var asyncHandler =  client.ExecuteAsync<AcctInquiryResponse>(request, r =>
                //  {
                //      if (r.ResponseStatus == ResponseStatus.Completed)
                //      {
                //         // var resp = JsonConvert.DeserializeObject<AcctInquiryResponse>(r.Content);
                //         // int i = r.Content.IndexOf("\"accountType\":", StringComparison.CurrentCultureIgnoreCase);
                //          re= r.Content.ToString();
                //      }

                //  });


                //  client.Delete(request);


                return re;
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format($"An error occured in {System.Reflection.MethodBase.GetCurrentMethod().Name}, with error :::{0}", ex.StackTrace));
                return "OTHERS";
            }


        }

    }
}






