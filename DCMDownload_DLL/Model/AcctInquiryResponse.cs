using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCMDownload_DLL.Model
{
    
    public class AcctInquiryResponse
    {
        public InquiryData data { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }
    public class InquiryData
    {
        public string accountType { get; set; }
    }
}
