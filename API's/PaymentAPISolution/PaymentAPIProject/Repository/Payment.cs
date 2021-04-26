using PaymentAPI.Models;
using PaymentAPI.Models.ViewModel;
using PaymentAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Repositories
{
    public class Payment : IPayment
    {
        private readonly testContext _context;

        public Payment()
        { }
        public Payment(testContext context)
        {
            _context = context;
        }
        public string PaymentProcess(long CreditCardNumber, int CreditLimit)
        {
            ProcessDetail processDetail = new ProcessDetail();
            ProcessResponse processResponse = new ProcessResponse();
            ProcessRequest processRequest = new ProcessRequest();
            string BalanceAmount;
            var p = _context.ProcessDetails.FirstOrDefault(c => c.CreditCardNumber == CreditCardNumber);
            if (CreditLimit < (p.PackagingAndDeliveryCharge + p.ProcessingCharge))
            {
                BalanceAmount = "Please Increase your Credit Limit!!!";
            }
            else
            {
                BalanceAmount = (CreditLimit - (p.PackagingAndDeliveryCharge + p.ProcessingCharge)).ToString();
            }

            return BalanceAmount;

        }




    }
}
