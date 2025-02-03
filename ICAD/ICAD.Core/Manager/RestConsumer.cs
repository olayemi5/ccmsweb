using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using ICAD.Core.IcardBLL;
using Newtonsoft.Json;
using System.Reflection;


namespace ICAD.Core.Manager
{
    public class RestConsumer
    {
        private static string Key;
        private static string Iv;

        static RestConsumer()
        {
            //Key = "lhX5FgsKTQzMwrc0"; //ik#*d)JFII#%3T0n
            //Iv = "YiTODHUpl818GQ0k"; //uE:Fh8jToN186*fY
            Key = "oUXhJExmwC4c96Kt"; //ik#*d)JFII#%3T0n
            Iv = "rXV34x7828GNYpw5"; //uE:Fh8jToN186*fY
        }

        public static string PostAccountMaintenance(RequestObj Rq, string url, string username, string password)
        {
            try
            {
                var client = new RestClient(url);

                var request = new RestRequest("api/accountmanager", Method.POST);
                var json = JsonConvert.SerializeObject(Rq);
                var encryptedRequest = Decryption.ICADEncrypt(json, Key, Iv);

                //request.AddParameter("text/plain", json, ParameterType.RequestBody);//, 
                request.AddParameter("text/plain", encryptedRequest, ParameterType.RequestBody);

                string odate = DateTime.Now.ToString("yyyyMMdd");
                string timestamp = DateTime.Now.ToString("yyyyMMddMMHHmmss"); //yyyyMMddMMHHmmss
                //string checkstring1 = "adewale.balogun@fcmb.com" + odate + "5NsAwu05#EKI7U83";
                string SignaturString = username + odate + password;
                string SignatureHash256Key = Encryption.GenerateSHA256String(SignaturString);
                //string checkstring = "adewale.balogun@fcmb.com" + ":" + "5NsAwu05#EKI7U83";
                string AuthString = username + ":" + password;
                string AuthEncrypteDdata = Encryption.Base64Encode(AuthString);
                string sha = "SHA256";

                request.AddHeader("Accept", "text/plain");
                request.AddHeader("Authorization", AuthEncrypteDdata);
                request.AddHeader("SIGNATURE", SignatureHash256Key);
                request.AddHeader("SIGNATURE_METH", sha);
                request.AddHeader("TIMESTAMP", timestamp);
                request.AddHeader("Content-Type", "text/plain");
                
                IRestResponse response = client.Execute(request);
                var result = response.Headers.Where(h => h.Name == "responseInfo").FirstOrDefault();

                if (result != null)
                {
                    return result.Value.ToString();
                }

                return response.StatusDescription + " " + response.ErrorMessage; 
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string PushMultipleRequest(List<RequestObj> Rq, string url, string username, string password)
        {
            try
            {
                var client = new RestClient(url);

                var request = new RestRequest("api/accountmanager/bulk", Method.POST);
                var json = JsonConvert.SerializeObject(Rq);
                var encryptedRequest = Decryption.ICADEncrypt(json, Key, Iv);

                //request.AddParameter("text/plain", json, ParameterType.RequestBody);//, 
                request.AddParameter("text/plain", encryptedRequest, ParameterType.RequestBody);

                string odate = DateTime.Now.ToString("yyyyMMdd");
                string timestamp = DateTime.Now.ToString("yyyyMMddMMHHmmss"); //yyyyMMddMMHHmmss
                //string checkstring1 = "adewale.balogun@fcmb.com" + odate + "5NsAwu05#EKI7U83";
                string SignaturString = username + odate + password;
                string SignatureHash256Key = Encryption.GenerateSHA256String(SignaturString);
                //string checkstring = "adewale.balogun@fcmb.com" + ":" + "5NsAwu05#EKI7U83";
                string AuthString = username + ":" + password;
                string AuthEncrypteDdata = Encryption.Base64Encode(AuthString);
                string sha = "SHA256";

                request.AddHeader("Accept", "text/plain");
                request.AddHeader("Authorization", AuthEncrypteDdata);
                request.AddHeader("SIGNATURE", SignatureHash256Key);
                request.AddHeader("SIGNATURE_METH", sha);
                request.AddHeader("TIMESTAMP", timestamp);
                request.AddHeader("Content-Type", "text/plain");

                IRestResponse response = client.Execute(request);
                //var header = response.Headers.ToString();
                var result = response.Headers.Where(h => h.Name == "responseInfo").FirstOrDefault();
                var headers = response.Headers.ToString();

                if (result != null)
                {
                    //return result.Value.ToString();
                }

                var sb = new StringBuilder();
                foreach (var param in response.Headers)
                {
                    sb.AppendFormat("{0}: {1}\r\n", param.Name, param.Value);
                }
                headers = sb.ToString();

                return response.StatusDescription + " " + response.ErrorMessage; 
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static string PushBranches(List<BranchObj> Rq, string url, string username, string password)
        {
            try
            {
                var client = new RestClient(url);

                var request = new RestRequest("api/accountmanager/branch", Method.POST);
                var json = JsonConvert.SerializeObject(Rq);
                var encryptedRequest = Decryption.ICADEncrypt(json, Key, Iv);

                //request.AddParameter("text/plain", json, ParameterType.RequestBody);//, 
                request.AddParameter("text/plain", encryptedRequest, ParameterType.RequestBody);

                string odate = DateTime.Now.ToString("yyyyMMdd");
                string timestamp = DateTime.Now.ToString("yyyyMMddMMHHmmss"); //yyyyMMddMMHHmmss
                //string checkstring1 = "adewale.balogun@fcmb.com" + odate + "5NsAwu05#EKI7U83";
                string SignaturString = username + odate + password;
                string SignatureHash256Key = Encryption.GenerateSHA256String(SignaturString);
                //string checkstring = "adewale.balogun@fcmb.com" + ":" + "5NsAwu05#EKI7U83";
                string AuthString = username + ":" + password;
                string AuthEncrypteDdata = Encryption.Base64Encode(AuthString);
                string sha = "SHA256";

                request.AddHeader("Accept", "text/plain");
                request.AddHeader("Authorization", AuthEncrypteDdata);
                request.AddHeader("SIGNATURE", SignatureHash256Key);
                request.AddHeader("SIGNATURE_METH", sha);
                request.AddHeader("TIMESTAMP", timestamp);
                request.AddHeader("Content-Type", "text/plain");

                IRestResponse response = client.Execute(request);
                //var header = response.Headers.ToString();
                var result = response.Headers.Where(h => h.Name == "responseInfo").FirstOrDefault();
                var headers = response.Headers.ToString();

                if (result != null)
                {
                    return result.Value.ToString();
                }

                var sb = new StringBuilder();
                foreach (var param in response.Headers)
                {
                    sb.AppendFormat("{0}: {1}\r\n", param.Name, param.Value);
                }
                headers = sb.ToString();

                return response.StatusDescription + " " + response.ErrorMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string ResetPassword(string url, string username)
        {
            var responseFromServer = string.Empty;
            try
            {
                var client = new RestClient(url);

                var request = new RestRequest("api/accountmanager/reset", Method.POST);
                request.AddParameter("text/plain", username, ParameterType.RequestBody);
               // request.AddHeader("username", username);
               // request.AddHeader("password", "");
               // request.AddHeader("Authorization", "");
               // request.AddHeader("SIGNATURE", "");
               // request.AddHeader("SIGNATURE_METHOD", "");
               // request.AddHeader("TIMESTAMP", "");
               request.AddHeader("Content-Type", "text/plain");//application/json

               

                IRestResponse response = client.Execute(request);
                var hd = response.ErrorMessage;
             
                var hd1 = response.Headers;
                responseFromServer = response.Content;
            }
            catch (Exception ex)
            {
                responseFromServer = ex.Message;
            }
            return responseFromServer;
        }
    }
}
