using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Repositories
{
    public interface IPayment
    {
        string PaymentProcess(long CreditCardNumber, int CreditLimit);
    }
}
