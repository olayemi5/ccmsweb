using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nfp.ssm.core;

namespace NIBssSSMService.core.SSMManager
{
    public  class encryptwithSSM
    {
        public static string EncryptData(string Data,string ApplicationName)
        {
            string encryptmessage = "";
            try
            {
                string pathone = "E:/DotnetSSMkeys/" + ApplicationName + "/public/";
                string pathtwo = "E:/DotnetSSMkeys/" + ApplicationName + "/private";
                SSMLib ssmlib = new SSMLib(pathone + "/public.key", pathtwo + "/private.key");
                 encryptmessage = ssmlib.encryptMessage(Data);
            }
            catch(Exception oo)
            {
                return oo.Message;
            }
            return encryptmessage;
        }
    }
}
