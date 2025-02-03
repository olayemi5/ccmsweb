using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using NIBssSSMService.core.SSMManager;

namespace NIBssSSMService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class SsmService : System.Web.Services.WebService
    {
        [WebMethod]
        public string generatessmkey(string applicationName)
        {
            return Keygenerator.generatenewkey(applicationName);
        }

        [WebMethod]
        public string ssmEncryption(string data, string applicationName)
        {
            return encryptwithSSM.EncryptData(data, applicationName);
        }

        [WebMethod]
        public string ssmDecryption(string Data, string applicationName)
        {
            return Decryptkey.DecryptData(Data, applicationName);
        }
    }
}