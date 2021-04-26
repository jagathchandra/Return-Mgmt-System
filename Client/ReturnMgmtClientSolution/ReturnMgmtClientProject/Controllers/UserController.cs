using ComponentProcessingAPIProject.test;
using ComponentProcessingAPIProject.Test.view_model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReturnMgmtClientProject.Models.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ReturnMgmtClientProject.Controllers
{
    public class UserController : Controller
    {
        public async Task<IActionResult> IndexWelcom(string name)
        {
           
            List<ProcessDetail> detailsList = new List<ProcessDetail>();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync("http://localhost:34213/api/Process/GetAllDetails"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        detailsList = JsonConvert.DeserializeObject<List<ProcessDetail>>(apiResponse);
                    }
                    ProcessDetail processDetail = detailsList.FirstOrDefault(d => d.Name == name);
                    TempData["Name"] = processDetail.Name;
                    TempData["CCNumber"] = processDetail.CreditCardNumber.ToString();
                }
                catch (Exception)
                {
                    ViewBag.Message = "Component API not Loaded. Please Try Later.";
                    return View();
                }
               
            }

            
            return View("ProcessRequest");
        }

        [HttpGet]
        public IActionResult ProcessRequest()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProcessRequest(ProcessRequest processRequest)
        {  
            ProcessRequest pRequest = new ProcessRequest();
            ProcessResponse pResponse = new ProcessResponse();
            pRequest.IsPriorityRequest = "No";
            using (var httpClient = new HttpClient())
            {
                try
                {
                    //int id = verify.VerificationId;
                    StringContent content = new StringContent(JsonConvert.SerializeObject(processRequest), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("http://localhost:34213/api/Process/ProcessRequest", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //ViewBag.Result = "Success";
                        pResponse = JsonConvert.DeserializeObject<ProcessResponse>(apiResponse);
                        //ViewBag.Result = "Successfully Registered, Please Login.....THANKYOU";
                        TempData["pResponse"] = JsonConvert.SerializeObject(pResponse);
                    }
                    TempData["RequestId"] = pResponse.RequestId;
                    TempData["PandD"] = JsonConvert.SerializeObject(pResponse.PackagingAndDeliveryCharge);
                    //TempData["PandD"] = pResponse.PackagingAndDeliveryCharge;
                    TempData["Processing"] = JsonConvert.SerializeObject(pResponse.ProcessingCharge);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Component API not Loaded. Please Try Later.";
                    return View();
                }
            }
            return RedirectToAction("ProcessResponse");
        }

        [HttpGet]
        public IActionResult ProcessResponse(ProcessResponse pResponse)
        {
            string strUser = TempData.Peek("pResponse").ToString();
            pResponse = JsonConvert.DeserializeObject<ProcessResponse>(strUser);

            return View(pResponse);
        }

        [HttpGet]
        public async Task<IActionResult> CompleteProcess(int RequestId)
        {

            ProcessResponse pResponse = new ProcessResponse();
            string pay;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync("http://localhost:34213/api/Process/CompleteProcessing/" + RequestId))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        pay = Convert.ToString(apiResponse);
                    }
                    TempData["pay"] = pay;
                }
                catch(Exception e)
                {
                    ViewBag.Message = "Component API not Loaded. Please Try Later.";
                    return View();
                }

            }

            
            return View();
        }

        [HttpGet]
        public IActionResult Payment()
        {
            
            PaymentModel paymentModel = new PaymentModel();
            paymentModel.RequestId = (int)TempData.Peek("RequestId");
            paymentModel.CreditCardNumber = TempData.Peek("CCNumber").ToString();
            //paymentModel.CreditLimit = 40000;
            return View(paymentModel);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(PaymentModel paymentModel)
        {
            //PaymentModel paymentModel = new PaymentModel();
            //paymentModel.RequestId = RequestId;
            string apiResponse;
            string check;
            using (var httpClient = new HttpClient())
            {
                try
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(paymentModel), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("http://localhost:46046/api/Payment/GetPaymentDetails", content))
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                        //paymentModel = JsonConvert.DeserializeObject<PaymentModel>(apiResponse);
                    }
                    TempData["Balance"] = apiResponse;
                    check = apiResponse;
                    if (check == "Please Increase your Credit Limit!!!")
                    {
                        return RedirectToAction("Invalid", "User");
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Payment API not Loaded. Please Try Later.";
                    return View();
                }
                
            }
            return RedirectToAction("Success", "User", apiResponse);           

        }

        public IActionResult Success(string apiResponse)
        {
            //TempData["Balance"]= apiResponse;
            return View(); 
        }

        public IActionResult Invalid()
        {
            return View();
        }
    }

}
