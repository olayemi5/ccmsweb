using System;
using System.Collections.Generic;
using System.Text;

namespace ICAD.Core.IcardBLL
{
    public class RequestObj
    {
        public string accountName { get; set; }
        public string requestID { get; set; }
        public string dateOfBirth { get; set; }
        public string email { get; set; }
        public string dateOpened { get; set; }
        public string accountNumber { get; set; }
        public int currency { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string BranchCode { get; set; }
        public int accountStatus { get; set; }
        public string institutionCode { get; set; }
        //public string requestorInstitutionCode { get; set; }
        public int accountType { get; set; }
        public int accountDesignation { get; set; }
        public string oldAccountNumber { get; set; }
        public string uniqueCustomerId { get; set; }
        public string rcNo { get; set; }
        public string dateModified { get; set; }
        public string phoneNumber { get; set; }
        public string phoneNumber2 { get; set; }

        public List<bvnObj> bvns = new List<bvnObj>();
    }
}
