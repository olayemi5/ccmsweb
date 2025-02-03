using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAD.WindowsService
{
    public class AppConfig
    {
        //root directory of the application
        public static string ApplicationPath {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
        
        public static string ServiceMode
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceMode"];
            }
        }

        public static string ScheduledTime
        {
            get
            {
            return ConfigurationManager.AppSettings["ScheduledTime"];
            }
        }

        public static string IntervalMinutes
        {
            get
            {
                return ConfigurationManager.AppSettings["IntervalMinutes"];
            }
        }

        public static string EmailFrom
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailFrom"];
            }
        }

        public static string EmailTo
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailTo"];
            }
        }

        public static string EmailCc
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailCc"];
            }
        }

        public static string EmailBcc
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailBcc"];
            }
        }

        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ICADConnection"].ConnectionString;
            }
        }

        public static string BankInstitutionCode
        {
            get
            {
                return ConfigurationManager.AppSettings["BankInstitutionCode"];
            }
        }

        public static string BankCode
        {
            get
            {
                return ConfigurationManager.AppSettings["BankCode"];
            }
        }

        public static string ICADUsername
        {
            get
            {
               return ConfigurationManager.AppSettings["ICADUsername"];
            }
        }

        public static string ICADPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["ICADPassword"];
            }
        }

        public static string ICADServiceUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ICADServiceUrl"];
            }
        }
    }
}
