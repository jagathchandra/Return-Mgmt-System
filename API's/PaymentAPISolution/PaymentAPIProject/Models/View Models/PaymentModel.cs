using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models.ViewModel
{
    public class PaymentModel
    {
        public int RequestId { get; set; }

        public long CreditCardNumber { get; set; }
        public int CreditLimit { get; set; }
    }
}
