using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using ICAD.Core.IcardBLL;

namespace ICAD.Core.Manager
{
   public class IcadAccountManager
    {
       public static string SubmitIcardData()
       {
           var responseFromServer = string.Empty;

          // string postData = System.Web.Helpers.Json.Encode(Rq); RequestObj Rq,string Url
           string postData = "";
           try
           {
             // postData = encryptwithSSM.EncryptData(postData,"ICARD");
               //postData = "{\"accountName\": \"" + Rq.accountName + "\",";
               //postData += "\"requestID\": \"" + Rq.requestID + "\",";
               //postData += "\"dateOfBirth\": \"" + Rq.dateOfBirth + "\",";
               //postData += "\"email\": \"" + Rq.email + "\",";
               //postData += "\"dateOpened\": \"" + Rq.dateOpened + "\",";
               //postData += "\"accountNumber\": \"" + Rq.accountNumber + "\",";
               //postData += "\"phoneNumber\": \"" + Rq.phoneNumber + "\",";
               //postData += "\"phoneNumber2\": \"" + Rq.phoneNumber2 + "\",";
               //postData += "\"currency\": \"" + Rq.currency + "\",";
               //postData += "\"firstName\": \"" + Rq.firstName + "\",";
               //postData += "\"middleName\":\"" + Rq.middleName + "\",";
               //postData += "\"lastName\": \"" + Rq.lastName + "\",";
               //postData += "\"branchCode\": \"" + Rq.branchCode + "\",";
               //postData += "\"accountStatus\": \"" + Rq.accountStatus + "\",";
               //postData += "\"institutionCode\": \"" + Rq.institutionCode + "\",";
               ////postData += "\"requestorInstitutionCode\": \"" + Rq.requestorInstitutionCode + "\",";
               //postData += "\"accountType\": \"" + Rq.accountType + "\",";
               //postData += "\"accountDesignation\": \"" + Rq.accountDesignation + "\",";
               //postData += "\"oldAccountNumber\": \"" + Rq.oldAccountNumber + "\",";
               //postData += "\"uniqueCustomerId\": \"" + Rq.uniqueCustomerId + "\",";
               //postData += "\"dateModified\": \"" + Rq.dateModified + "\",";
               //postData += "\"rcNo\": \"" + Rq.rcNo + "\",";
               //postData += "\"bvns\": ";
               //postData += "[";
               //foreach (bvnObj bvn in Rq.bvns)
               //{
               //    int i = Rq.bvns.Count % 2;
               //    if (i != 0)
               //    {
               //        postData += "{\"bvn\":\"" + bvn.bvn + "\"}";
               //    }
               //    else
               //    {
               //        postData += "{\"bvn\":\"" + bvn.bvn + "\"},";
               //    }


               //}
               //postData += "]}";


               string Url = "http://192.234.10.104:86/icadservice/api/accountmanager/reset"; ///adewale.balogun@fcmb.com
            // string  Url = "http://192.234.10.220:80/icadservice/api/accountmanager/reset";
               WebRequest request = WebRequest.Create(Url);


                //string odate = DateTime.Now.ToString("yyyyMMdd");
                //string timestamp = DateTime.Now.ToString("yyyyMMddMMHHmmss"); //yyyyMMddMMHHmmss
                //string checkstring1 = "adewale.balogun@fcmb.com" + odate + "5NsAwu05#EKI7U83";
                //string hash256Key = Encryption.GenerateSHA256String(checkstring1);
                //string checkstring = "adewale.balogun@fcmb.com" + ":" + "5NsAwu05#EKI7U83";
                //string Encrypteddata =  Encryption.Base64Encode(checkstring);
                
                
                //string sha ="SHA256";
                ////string hearder = "{\" Authorization\": \"" + Encrypteddata + "\",";
                ////hearder += "\"SIGNATURE\": \"" + Encrypteddata1 + "\",";
                ////hearder += "\"SIGNATURE_METH\": \"" + sha + "\",";
                ////hearder += "\"TIMESTAMP\": \"" + odate + "\"}";

                //request.Headers.Add("Authorization", Encrypteddata);
                //request.Headers.Add("SIGNATURE", hash256Key);
                //request.Headers.Add("SIGNATURE_METH", sha);
                //request.Headers.Add("TIMESTAMP", timestamp);
               string username = "adewale.balogun@fcmb.com"; //icad@fcmb.com
                //request.Headers.Add("username",username);
                //request.Headers.Add("password","");
                //request.Headers.Add("Authorization", "");
                //request.Headers.Add("SIGNATURE", "");
                //request.Headers.Add("SIGNATURE_METHOD", "");
                //request.Headers.Add("TIMESTAMP", "");
              //  request.Headers.Add("Authorization", Encrypteddata);
               //request.Headers.

               // postData = "adewale.balogun@fcmb.com";
               postData = username;


                
               request.Method = "POST";
               Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
               request.ContentType = "text/plain";//application/json
               request.ContentLength = byteArray.Length;

               Stream dataStream = request.GetRequestStream();

               dataStream.Write(byteArray, 0, byteArray.Length);

               dataStream.Close();

               WebResponse response = request.GetResponse();
              
              
               Console.WriteLine(((HttpWebResponse)response).StatusDescription);

              
               dataStream = response.GetResponseStream();

             
               StreamReader reader = new StreamReader(dataStream);

            
               responseFromServer = reader.ReadToEnd();

               //responseFromServer = Decryptkey.DecryptData(responseFromServer, "ICARD");
             
               reader.Close();
               dataStream.Close();
               
               var headers = response.Headers.ToString();
               var reuben = "";
               response.Close();
           }
           catch(WebException oo)
           {
               string ddd = oo.Message;

               if (oo.Status == WebExceptionStatus.ProtocolError)
               { }
               if (oo.Response != null)
               {
                   var statusCode = ((HttpWebResponse)oo.Response).StatusCode;
                   var statusDescription = ((HttpWebResponse)oo.Response).StatusDescription;

                  var ResponseStatus = statusCode.ToString();
                   var StatusDescription = statusDescription;
               }
               var ThatWasThrown = oo.InnerException + " \n " + oo.Message;
               //callResult.ErrorMessage = ex.Message;

               var headerError = ((HttpWebResponse)oo.Response).Headers;
               if (headerError != null)
               {
                   if (headerError.Count > 0)
                   {
                       //var callResult ="";
                   }
               }
           }
           return responseFromServer;
       }
    }
}
