using ICAD.Business.Services;
using ICAD.WindowsService.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICAD.WindowsService
{
    public partial class ICADServiceManager : ServiceBase
    {
        private Timer Scheduler;
        private string[] args;

        public enum TimerEnum
        {
            None = 0,
            Daily = 1,
            Interval = 2
        }

        public ICADServiceManager()
        {
            InitializeComponent();
        }

        public ICADServiceManager(string[] args)
        {
            this.args = args;
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }

        protected override void OnStart(string[] args)
        {
            //Logger.LogToFile("ICAD Service delayed till " + DateTime.Now.AddMinutes(1).ToString("dd/MM/yyyy hh:mm:ss tt"));
            //Timer timer = null;
            //timer = new Timer((obj) =>
            //{
            //    Logger.LogToFile("ICAD Service started at " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            //    this.ICARDServiceScheduler();
            //    timer.Dispose();
            //}, null, 20000, Timeout.Infinite);

            Logger.LogToFile("ICAD Service started at " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            this.ICARDServiceScheduler();
        }

        protected override void OnStop()
        {
            Logger.LogToFile("ICAD Service stopped at " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            this.Scheduler.Dispose();
        }

        private void SchedulerCallback(object e)
        {
            Logger.LogToFile("ICAD Service Log: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            this.ICARDServiceScheduler();
        }

        private void ICARDServiceScheduler()
        {
            try
            {
                Console.WriteLine("ICAD Windows service started...");
                Scheduler = new Timer(new TimerCallback(SchedulerCallback));
                //service mode - Daily or Interval
                var mode = Convert.ToInt32(AppConfig.ServiceMode); // (TimerEnum)Enum.Parse(typeof(TimerEnum), AppConfig.ServiceMode());

                Logger.LogToFile((Convert.ToString("ICAD Service Mode: ") + mode) + " {0}");
                //Set the Default Time.

                DateTime scheduledTime = new DateTime();

                if (mode == (int)TimerEnum.Daily)
                {
                    //Get the Scheduled Time from AppSettings.
                    scheduledTime = DateTime.Parse(AppConfig.ScheduledTime);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next day.
                        scheduledTime = scheduledTime.AddDays(1);
                    }
                }

                if (mode == (int)TimerEnum.Interval)
                {
                    //Get the Interval in Minutes from AppSettings.
                    int intervalMinutes = Convert.ToInt32(AppConfig.IntervalMinutes);

                    //Set the Scheduled Time by adding the Interval to Current Time.
                    scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
                    if (DateTime.Now > scheduledTime)
                    {
                        //If Scheduled Time is passed set Schedule for the next Interval.
                        scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
                    }
                }


                /*=================== MAIN START LOGIC HERE ===================*/
                Console.WriteLine("Begin the main task...");
                Console.WriteLine("---------------------------------------------------------------------");
                var app = new IcadService();
                var startDate = DateTime.Now.AddDays(-1); //AddYears(-2);
                var endDate = DateTime.Now;

                var institutionCode = AppConfig.BankInstitutionCode;
                var bankCode = AppConfig.BankCode;
                var username = AppConfig.ICADUsername;
                var password = AppConfig.ICADPassword;
                var serviceUrl = AppConfig.ICADServiceUrl;

                var message = app.PushService(startDate, endDate, bankCode, institutionCode, serviceUrl, username, password);
                //var message = "SampleMsg";
                Logger.LogByProcees("IcadService.PushService", message);

                //send report to email
                var subject = "ICAD Service Report";
                var emailTemplate = "EmailTemplate.html";
                Mailer.SendEmailReport(emailTemplate, subject, message);
                Console.WriteLine("Main task is completed!");
                Console.WriteLine("---------------------------------------------------------------------");

                /*=================== MAIN END LOGIC HERE ===================*/


                TimeSpan timeSpan = new TimeSpan();
                timeSpan = scheduledTime.Subtract(DateTime.Now);
                string schedule = string.Format("{0} day(s) {1} hour(s) {2} minute(s) {3} seconds(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                Logger.LogToFile((Convert.ToString("ICAD Service scheduled to run after: ") + schedule) + " {0}");

                //Get the difference in Minutes between the Scheduled and Current Time.
                Int64 dueTime = Convert.ToInt64(timeSpan.TotalMilliseconds);
                //Change the Timer's Due Time.
                Scheduler.Change(dueTime, Timeout.Infinite);

                Console.WriteLine("Service is scheduled to run next at " + schedule);
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine("Press exit to close the window ");
            }
            catch (Exception ex)
            {
                Logger.LogToFile("ICAD Service Error on: {0} " + ex.Message + ": " + ex.Source.ToString().Trim() + "; " + ex.StackTrace);

                //send error report to email
                var subject = "ICAD Service Error";
                var emailTemplate = "EmailTemplate.html";
                var messageBody = ex.Message + ": " + ex.Source.ToString().Trim() + "; " + ex.StackTrace;
                Mailer.SendEmailReport(emailTemplate, subject, messageBody);

                //Stop the Windows Service.
                using (ServiceController serviceController = new ServiceController("ICADService"))
                {
                    serviceController.Stop();
                }
            }
        }
    }
}
