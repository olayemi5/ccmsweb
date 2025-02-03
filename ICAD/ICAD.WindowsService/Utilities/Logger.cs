using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAD.WindowsService.Utilities
{
    public class Logger
    {
        public static void LogToFile(string text)
        {
            string path = string.Format(@"{0}\ServiceLog.txt", AppConfig.ApplicationPath);
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }

        public static void LogByProcees(string processName, string message)
        {
            LogToFile(string.Format("{0} - {1} method return: {2} ",
                DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), processName, message));
        }
    }
}
