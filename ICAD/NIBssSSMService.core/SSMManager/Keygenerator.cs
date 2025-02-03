using System;
using System.Collections.Generic;
using System.Text;
using nfp.ssm.core;
using System.Configuration;

namespace NIBssSSMService.core.SSMManager
{
   public class Keygenerator
    {
       public static string generatenewkey(string ApplicationName)
       {
           bool flg = false;
           try
           {
              string email = ConfigurationManager.AppSettings["ssmemail"].ToString();
              string password = ConfigurationManager.AppSettings["ssmpassword"].ToString();

               string pathone = "E:/DotnetSSMkeys/" + ApplicationName + "/public/";
               string pathtwo = "E:/DotnetSSMkeys/" + ApplicationName + "/private";
             
               if ((!System.IO.File.Exists(pathone)) && (!System.IO.File.Exists(pathtwo)))
               {
                   System.IO.Directory.CreateDirectory(pathone);
                   System.IO.Directory.CreateDirectory(pathtwo);
               }
               SSMLib ssmlib = new SSMLib(pathone+"/public.key",pathtwo+"/private.key");
               flg = ssmlib.generateKeyPair(email, password);
               return flg.ToString()+ "|" +email+"|" + password;

               
           }
           catch(Exception oo)
           {
               return "Exception|"+oo.Message;
           }
       }
    }
}
