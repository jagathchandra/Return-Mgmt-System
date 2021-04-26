using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models.ViewModel
{
    public class ProcessRequest
    {

        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public long? CreditCardNumber { get; set; }
        public string ComponentType { get; set; }
        public string ComponentName { get; set; }
        public int? Quantity { get; set; }
        public string IsPriorityRequest { get; set; }
    }
}
