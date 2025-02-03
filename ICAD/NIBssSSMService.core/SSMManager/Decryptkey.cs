using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nfp.ssm.core;
using System.Configuration;

namespace NIBssSSMService.core.SSMManager
{
   public class Decryptkey
    {
       public static string DecryptData(string Data, string ApplicationName)
       {
           string encryptmessage = "";
           try
           {
               string email = ConfigurationManager.AppSettings["ssmemail"].ToString();
               string password = ConfigurationManager.AppSettings["ssmpassword"].ToString();
               string pathone = "E:/DotnetSSMkeys/" + ApplicationName + "/public/";
               string pathtwo = "E:/DotnetSSMkeys/" + ApplicationName + "/private";
               SSMLib ssmlib = new SSMLib(pathone + "/public.key", pathtwo + "/private.key");
               encryptmessage = ssmlib.decryptFile(Data, password);
           }
           catch (Exception oo)
           {
               return oo.Message;
           }
           return encryptmessage;
       }
    }
}
