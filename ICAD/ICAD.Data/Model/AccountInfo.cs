using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAD.Data.Model
{
    [Table("AccountRecords")]
    public class AccountInfo
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string RequestID { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string DateOpened { get; set; }
        public string Currency { get; set; }
        public int CurrencyCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string BranchCode { get; set; }
        public int AccountStatus { get; set; }
        [NotMapped]
        public string InstitutionCode { get; set; }
        public int AccountType { get; set; }
        public int AccountDesignation { get; set; }
        public string OldAccountNumber { get; set; }
        public string UniqueCustomerId { get; set; }
        public string RcNo { get; set; }
        public string DateModified { get; set; }
        public DateTime Date { get; set; }

        //web service data
        public DateTime? ServiceRequestTime { get; set; }
        public DateTime? ServiceResponseTime { get; set; }
        public string ServiceResponseMessage { get; set; }
        public bool IsSuccessful { get; set; }
        public string BvnString { get; set; }

        [NotMapped]
        public List<BvnData> bvns { get; set; } // = new List<BvnData>();
    }
}
