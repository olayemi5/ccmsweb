using Fcmb.Utilities.Logger;
using ICAD.Core.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAD.Business.Services
{
    public class IcadService
    {
        private readonly AccountInfoService _accountInfoService;

        public IcadService()
        {
            _accountInfoService = new AccountInfoService();
        }

        public string PushService(DateTime startDate, DateTime endDate, string bankCode, string institutionCode, string serviceUrl, string username, string password)
        {
            var message = string.Empty;
            try
            {
                var take = 5;
                var counter = 0;
                
                //check exist record for the specified to avoid dublicate pulling
                var alreadyPulled = _accountInfoService.GetByDate(endDate).Count() > 0;

                if (!alreadyPulled)
                {
                    //get data from infopool and load to local table
                    var loadData = _accountInfoService.GetAccountsFromInfopool(startDate, endDate);
                }
                    //get last populated data
                var datalist = _accountInfoService.GetByDate(bankCode, institutionCode, endDate);
                    if (datalist.Count() > 0)
                    {
                        var response = string.Empty;
                        while (counter <= datalist.Count())
                        {
                            var list = datalist.Skip(counter).Take(take).ToList();
                            Console.WriteLine("Calling the web service...");
                            //send multiple requests
                            var requests = _accountInfoService.ConvertListToRequest(list, institutionCode).ToList();
                            var requestTime = DateTime.Now;
                            response = RestConsumer.PushMultipleRequest(requests, serviceUrl, username, password);
                            if (!string.IsNullOrEmpty(response))
                            {
                                //TODO: update the database for status
                                var responseTime = DateTime.Now;
                                foreach (var item in list)
                                {
                                    _accountInfoService.UpdateServiceResponse(item, requestTime, responseTime, response, true);
                                }
                            }
                            counter = counter + list.Count();
                        }

                        //send single request
                        //var sn = 1;
                        //foreach (var item in datalist)
                        //{
                        //    var request = _accountInfoService.ConvertToRequest(item, bankCode);
                        //    var requestTime = DateTime.Now;
                        //    Console.WriteLine(sn + " of " + datalist.Count() + " items in progress...");
                        //    var response = RestConsumer.PostAccountMaintenance(request, serviceUrl, username, password);
                        //    if (!string.IsNullOrEmpty(response))
                        //    {
                        //        //TODO: update the database for status
                        //        var responseTime = DateTime.Now;
                        //        _accountInfoService.UpdateServiceResponse(item, requestTime, responseTime, response, true);
                        //    }
                        //    sn++;
                        //}
                        message = datalist.Count() + " record(s) was processed successfull \n Response: " + response;
                    }
                else
                {
                    message = "No record found from the database";
                }
                Console.WriteLine("---------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex.ToString());
                message = ex.Message;
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Exception occured at: {0}", ex.Source);
                Console.WriteLine("Error occured... Type exit to close the window");
                Console.WriteLine("---------------------------------------------------------------------");
            }
            Console.WriteLine(message);
            return message;
        }

        public string PushBranches(string bankCode, string institutionCode, string serviceUrl, string username, string password)
        {
            var message = string.Empty;
            try
            {
                var take = 5;
                var counter = 0;
                //get data from infopool and load to local table
                var loadBranches = _accountInfoService.GetBankBranches(bankCode, institutionCode);
                if (loadBranches.Count() > 0)
                {
                    var response = string.Empty;
                    while (counter <= loadBranches.Count())
                    {
                        var list = loadBranches.Skip(counter).Take(take).ToList();
                        Console.WriteLine("Calling the web service...");
                        response = RestConsumer.PushBranches(list, serviceUrl, username, password);
                        counter = counter + list.Count();
                    }

                    message = loadBranches.Count() + " branches(s) was processed with status below. \n Response: " + response;
                }
                else
                {
                    message = "No record found from the database";
                }
                Console.WriteLine("---------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex.ToString());
                message = ex.Message;
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Exception occured at: {0}", ex.Source);
                Console.WriteLine("Error occured... Type exit to close the window");
                Console.WriteLine("---------------------------------------------------------------------");
            }
            Console.WriteLine(message);
            return message;
        }
    }
}
