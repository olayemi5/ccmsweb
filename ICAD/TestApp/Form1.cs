using ICAD.Business.Services;
using ICAD.Core.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pushBtn_Click(object sender, EventArgs e)
        {
            var app = new IcadService();
            var startDate = DateTime.Now.AddDays(-3); //AddYears(-2);
            var endDate = DateTime.Now;

            var institutionCode = BankInstitutionCode;
            var bankCode = BankCode;
            var username = ICADUsername;
            var password = ICADPassword;
            var serviceUrl = ICADServiceUrl;

            var message = app.PushService(startDate, endDate, bankCode, institutionCode, serviceUrl, username, password);

            MessageBox.Show(message);
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
         // var response = RestConsumer.ResetPassword(ICADServiceUrl, ICADUsername);
          var response = IcadAccountManager.SubmitIcardData();
            MessageBox.Show(response);
        }

        private void branchBtn_Click(object sender, EventArgs e)
        {
            var username = ICADUsername;
            var password = ICADPassword;
            var serviceUrl = ICADServiceUrl;
            var bankCode = BankCode;
            var institutionCode = BankInstitutionCode;

            var app = new IcadService();

            var message = app.PushBranches(bankCode, institutionCode, serviceUrl, username, password);

            MessageBox.Show(message);
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
