using ComponentProcessingAPIProject.Models;
using ComponentProcessingAPIProject.Test.view_model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ComponentProcessingAPIProject.Repository
{
    public class Repair : IRepo
    {
        //private readonly ReturnManagementSystemContext db;
        private readonly testContext _test;

        

        public Repair(testContext test)
        {
            _test = test;
        }

        public IEnumerable<ProcessDetail> GetAllDetails()
        {
            var ListofDetails = _test.ProcessDetails.ToList();
            return ListofDetails;
        }

        public ProcessResponse ProcessResponse(string Name, string ContactNumber, string ComponentType, string ComponentName, int Quantity, string IsPriorityRequest)
        {
            ProcessResponse pResponse = new ProcessResponse();
            ProcessRequest pRequest = new ProcessRequest();
            //Random r = new Random();
            //pResponse.RequestId = r.Next(10, 200);
            var DateofDelivery = DateTime.Now;

            ProcessDetail a = _test.ProcessDetails.FirstOrDefault(c => c.ContactNumber == ContactNumber);

            if (ComponentType == "Integral" && (IsPriorityRequest == "No" || IsPriorityRequest == null))
            {
                
                pResponse.RequestId = a.RequestId;
                using (var client = new HttpClient())
                {
                    //pRequest.ComponentType = a.ComponentType;
                    client.BaseAddress = new Uri("https://localhost:44344");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync("api/PackageDelivery/GetPackagingDeliveryCharge/" + ComponentType + "," + Quantity).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        string PandD = JsonConvert.DeserializeObject<string>(responseString);
                        //pResponse.PackagingAndDeliveryCharge = (double?)PandD;
                        pResponse.PackagingAndDeliveryCharge = Convert.ToDouble(PandD);
                    }
                }
                pResponse.ProcessingCharge = 500;                
                //pResponse.PackagingAndDeliveryCharge = 300;
                pResponse.DateofDelivery = DateofDelivery.AddDays(5);

                a.PackagingAndDeliveryCharge = pResponse.PackagingAndDeliveryCharge;
                a.ProcessingCharge = pResponse.ProcessingCharge;
                a.DateofDelivery = DateofDelivery.AddDays(5);

                _test.SaveChanges();

                //return pResponse;
            }
            if (ComponentType == "Integral" && IsPriorityRequest == "Yes")
            {
                pResponse.RequestId = a.RequestId;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44344");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    
                    var response = client.GetAsync("api/PackageDelivery/GetPackagingDeliveryCharge/" + ComponentType + "," + Quantity).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        string PandD = JsonConvert.DeserializeObject<string>(responseString);
                        //pResponse.PackagingAndDeliveryCharge = (double?)PandD;
                        pResponse.PackagingAndDeliveryCharge = Convert.ToDouble( PandD );
                        //_test.SaveChanges();
                    }
                }

                pResponse.ProcessingCharge = 700;
                //pResponse.PackagingAndDeliveryCharge = 500;                
                pResponse.DateofDelivery = DateofDelivery.AddDays(2);

                a.PackagingAndDeliveryCharge = pResponse.PackagingAndDeliveryCharge;
                a.ProcessingCharge = pResponse.ProcessingCharge;
                a.DateofDelivery = DateofDelivery.AddDays(2);
                _test.SaveChanges();
                
                //return pResponse;
            }
            //if(ComponentType == "Integral")
            if(ComponentType == "Accessory")
            {
                pResponse.RequestId = a.RequestId;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44344");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    var response = client.GetAsync("api/PackageDelivery/GetPackagingDeliveryCharge/" + ComponentType + "," + Quantity).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        string PandD = JsonConvert.DeserializeObject<string>(responseString);
                        //pResponse.PackagingAndDeliveryCharge = (double?)PandD;
                        pResponse.PackagingAndDeliveryCharge = Convert.ToDouble(PandD);
                        //_test.SaveChanges();
                    }
                }

                pResponse.ProcessingCharge = 300;
                //pResponse.PackagingAndDeliveryCharge = 500;                
                pResponse.DateofDelivery = DateofDelivery.AddDays(5);

                a.PackagingAndDeliveryCharge = pResponse.PackagingAndDeliveryCharge;
                a.ProcessingCharge = pResponse.ProcessingCharge;
                a.DateofDelivery = DateofDelivery.AddDays(2);
                _test.SaveChanges();
            }
  
            return pResponse;
        }

        public string CompleteProcess(int RequestId)
        {
            ProcessResponse pResponse = new ProcessResponse();
            ProcessRequest pRequest = new ProcessRequest();
            var a = _test.ProcessDetails.FirstOrDefault(c => c.RequestId == RequestId);
            pResponse.ProcessingCharge = a.ProcessingCharge;
            pResponse.PackagingAndDeliveryCharge = a.PackagingAndDeliveryCharge;
            
            string Total = Convert.ToString(a.ProcessingCharge + a.PackagingAndDeliveryCharge);
            return Total;
        
        }
    }
}
